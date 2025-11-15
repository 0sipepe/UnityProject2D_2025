using UnityEngine;
using System;

public class Interaction : MonoBehaviour
{

   
    private InputManager gameInput;

    
    [SerializeField]
    private LayerMask layerFinal;
    

    private bool isHolding = false;
    public event EventHandler OnGettingInventory;
    public event EventHandler OnThrowingInventory;
    private HealthState _healthState;
   



    //private GameObject interactableObject;
    internal IHoldable interactableObject;
    private IHoldable interactableObjectNearPlayer;
    private IEnemy enemyRightNow;
   
    
    int layerFinalNumber;
    // Ёто точно помен€ть смотри строку 96


    private void Start()
    {
        _healthState = this.transform.GetComponent<HealthState>();
        layerFinalNumber = (int)Mathf.Log(layerFinal.value, 2);
        gameInput = InputManager.Instance;
        gameInput.OnInteractAction += GameInput_OnInteractAction;
       
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {

      
        //логика дл€ €годы
        if ((interactableObjectNearPlayer == null))
        {
            if (isHolding)
            {
                Debug.Log("throw");
                isHolding = false;
                OnThrowingInventory?.Invoke(this, EventArgs.Empty);
                interactableObject.SetIsHolded(false);
                interactableObject = null;
                return;
            }

        }
        else
        {
            if (!isHolding)
            {
                //удалили предыдущий
                interactableObject?.SetIsHolded(false);
                interactableObject = interactableObjectNearPlayer;
                isHolding = true;
            
                //€года знает что ее держат
                interactableObject.SetIsHolded(true);
                OnGettingInventory?.Invoke(this, EventArgs.Empty);

                if (interactableObject.GetTransform().CompareTag("winner"))
                {
                    _healthState.CheckState();
                }

            }
            return;

        }


        //логика дл€ лос€
        if (enemyRightNow != null)
        {
            enemyRightNow.ReactToPlayer();
            if (_healthState.IsAlive == false)
            {
                _healthState.BecomeFish();
            }

        }
    
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        //дл€ €год и прочего
        if (collision.transform.GetComponent<IHoldable>() != null)
        {
            interactableObjectNearPlayer = collision.transform.GetComponent<IHoldable>();
            
        }
       

        if (collision.transform.GetComponent<IEnemy>() != null)
        {
            enemyRightNow = collision.transform.GetComponent<IEnemy>();
            enemyRightNow.SetPlayer(this.gameObject);

         
        }


        //дл€ финального триггера. <<< тут написано говно какое-то. “ут сравнивать по тегу будет лучше.  то-то в таких случа€х, чтобы не сравнивать по тэгу (работа со строками), вешает пустой монобех на объект, чтобы было как в строке 90. >>>
        if (collision.gameObject.layer == layerFinalNumber)
        {
            
            enemyRightNow.ReactToPlayer();
            _healthState.CheckState();
           
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
        //€года
        if (collision.transform.GetComponent<IHoldable>() != null)
        {
           
            interactableObjectNearPlayer = null;
        }

        //течение дл€ рыбы
        if (collision.transform.GetComponent<IEnemy>() != null)
        {
            if (collision.transform.GetComponent<FlowLevel>() != null)
            {
                _healthState.LetControl();
                enemyRightNow.SetPlayer(null);
                enemyRightNow = null;
            }
           
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.collider.CompareTag("Bubble"))
        {

            _healthState.BanControl();
        }
        if (collision.transform.GetComponent<IEnemy>() != null)
        {
            if(collision.transform.GetComponent<BirdMoving>() != null)
            {
                enemyRightNow = collision.transform.GetComponent<IEnemy>();
                Debug.Log(this.gameObject);
                enemyRightNow.SetPlayer(this.gameObject);
                enemyRightNow.ReactToPlayer();
            }
        }
    }

    private void OnDestroy()
    {
        gameInput.OnInteractAction -= GameInput_OnInteractAction;
    }



}
