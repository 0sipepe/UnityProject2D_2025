using UnityEngine;
using System;

public class Interaction : MonoBehaviour
{

   
    private InputManager gameInput;

    private bool isHolding = false;
    public event EventHandler OnGettingInventory;
    public event EventHandler OnThrowingInventory;


    //private GameObject interactableObject;
    internal IHoldable interactableObject;
    private IHoldable interactableObjectNearPlayer;
    


    void Start()
    {
        gameInput = InputManager.Instance;
        gameInput.OnInteractAction += GameInput_OnInteractAction;
       
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {

        // вместо логического И сделать вложенный if
        if (isHolding && (interactableObjectNearPlayer == null))
        {
            Debug.Log("throw");
            isHolding = false;
            OnThrowingInventory?.Invoke(this, EventArgs.Empty);
            interactableObject.SetIsHolded(false);
            interactableObject = null;

        }
        else if (isHolding && (interactableObjectNearPlayer != null))
        {
            interactableObject = interactableObjectNearPlayer;
            //ягода знает что ее держат
            interactableObject.SetIsHolded(true);
            OnGettingInventory?.Invoke(this, EventArgs.Empty);

        }
        else if (!isHolding && (interactableObjectNearPlayer != null))
        {
            Debug.Log("get");
            interactableObject = interactableObjectNearPlayer;
            isHolding = true;
            interactableObject.SetIsHolded(true);
            OnGettingInventory?.Invoke(this, EventArgs.Empty);
        }
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable Objects"))
        {
            
            Debug.Log("trigger enter");
            interactableObjectNearPlayer = collision.transform.GetComponent<IHoldable>();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("trigger exit");
        interactableObjectNearPlayer = null;
    }
   


}
