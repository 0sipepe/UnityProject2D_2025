using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BirdMoving : MonoBehaviour, IEnemy
{

    [SerializeField]
    private float distanceToFlyHorizontal;
    [SerializeField]
    private float distanceToFlyVertical;
    [SerializeField]
    private BoxCollider2D upperCollider;
    [SerializeField]
    private float distanceToPassEveryStep = 10;

    [SerializeField]
    private GameObject placeToPutPlayer;
    private bool playerFalling = false;

    private float distancePassedHorizontal = 0;
    private float distancePassedVertical = 0;
    
  

    private int stateOfReacting = 0;



    private GameObject _player;
    private float periodQuickTimeEvent = 5;
    private float timeElapsed;
    private int playerJumpsCount;
    private int requiredJumpsCount = 10;

    Vector2 startPoint;


  
    void Start()
    {
        startPoint = transform.position;
    }

    private void BirdMoving_OnJump(object sender, System.EventArgs e)
    {
        playerJumpsCount++;
        HandlePlayerActions();
    }

    void Update()
    {
        if (playerFalling)
        {
            PlayerFall();
        }
        Debug.Log(startPoint);
        timeElapsed += Time.deltaTime;
        switch (stateOfReacting)
        {
            case 1:
                FlyUp();
                break;
            case 2:
                FlyLeft();
                break;
            case 3:
                FlyDown();
                break;
            case 4:
                FlyUp();
                break;
            case 5:
                FlyBack();
                break;
            case 6:
                FlyDown();
                break;
        }
      
    }
    private void FlyUp()
    {
       
        if (distancePassedVertical < distanceToFlyVertical)
        { 
           
            transform.Translate(Vector2.up * distanceToPassEveryStep * Time.deltaTime);
            distancePassedVertical += distanceToPassEveryStep * Time.deltaTime;       
        }
        else
        {
            if (_player != null)
            {
                
                stateOfReacting = 2;
                distancePassedVertical = 0;
            }
            else
            {
                stateOfReacting = 5;
                distancePassedVertical = 0;
            }
        }
    }
    private void FlyDown()
    {
       
        if (distancePassedVertical < distanceToFlyVertical)
        {
            transform.Translate(Vector2.down * distanceToPassEveryStep * Time.deltaTime);
            distancePassedVertical += distanceToPassEveryStep * Time.deltaTime;
        }
        else
        {
            if(_player != null)
            {
                _player.transform.position = placeToPutPlayer.transform.position;
                SetPlayer(null);
                
                stateOfReacting = 4;
                distancePassedVertical = 0;
            }
            else
            {
                if (playerFalling)
                {
                    stateOfReacting = 4;
                   
                }
                else 
                { 
                     stateOfReacting = 0;
                }
                   
                distancePassedVertical = 0;
            }

            if(stateOfReacting == 6)
            {
                transform.position = startPoint;
            }               
        }
    }

    private void FlyLeft() 
    {
        
        if (distancePassedHorizontal < distanceToFlyHorizontal)
        {
            
            transform.Translate(Vector2.left * distanceToPassEveryStep * Time.deltaTime);
            distancePassedHorizontal += distanceToPassEveryStep * Time.deltaTime;
        }
        else
        {
            stateOfReacting = 3;
            distancePassedHorizontal = 0;
        }
    
    }
    private void FlyBack()
    {
      
        if (distancePassedHorizontal < distanceToFlyHorizontal)
        {
            transform.Translate(Vector2.right * distanceToPassEveryStep * Time.deltaTime);
            distancePassedHorizontal += distanceToPassEveryStep * Time.deltaTime;
            
        }
        else
        {
            stateOfReacting = 6;
            distancePassedHorizontal = 0;
            distancePassedVertical = 0;
        }
        
        
    }
    public void ReactToPlayer()
    {
        timeElapsed = 0;

        stateOfReacting = 1;
       
       

    }

    private void ManageSound()
    {
        if(_player != null)
        {
            this.transform.GetComponent<AudioSource>().enabled = true;
        }
        else
        {

            this.transform.GetComponent<AudioSource>().enabled = false;
        }
    }
    public void SetPlayer(GameObject player)
    {
        
        if (player == null)
        {

            _player.transform.GetComponent<HealthState>().LetControl();
            _player.GetComponent<MovingFish>().OnJump -= BirdMoving_OnJump;
            _player.GetComponent<HealthState>().LetControl();
            _player.transform.parent = null;
            _player = null;
            ManageSound();

        }
        else
        {

            _player = player;
           
            _player.transform.parent = this.transform;
            _player.transform.localPosition = new Vector2(0, 0.05f);
            _player.GetComponent<MovingFish>().OnJump += BirdMoving_OnJump;
            _player.GetComponent<HealthState>().BanControl();
            ManageSound() ;
          
        } 
    }
    private void PlayerFall()
    {
        if (_player.transform.position.y > startPoint.y)
        {
            _player.transform.Translate(Vector2.down * distanceToPassEveryStep * Time.deltaTime);
        }
        else
        {
            _player.transform.position = new Vector2(_player.transform.position.x, placeToPutPlayer.transform.position.y);
            playerFalling = false;
            SetPlayer(null);

            upperCollider.isTrigger = false;
        }
    }

    private void HandlePlayerActions()
    {
        if (timeElapsed < periodQuickTimeEvent)
        {
            if (playerJumpsCount >= requiredJumpsCount)
            {

                playerFalling = true;
                upperCollider.isTrigger = true;
                FlyBack();
            }
        }
    }


}
