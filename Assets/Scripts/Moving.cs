using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField]
    private InputManager gameInput;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float limitToStartNewJump;


    private bool isWalking;
    private bool isJumpButtonPushed = false;
    private bool isJumpingUp = false;
    private bool isJumpingDown = false;
    private bool hasReachedTheChange = false;
    

    //Надо поменять логику того, что здесь все зависит от абсолютной позиции игрока. Либо сделать емпти 
    private void Start()
    {
        gameInput.OnJumpAction += GameInput_OnJumpAction;
        
    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e)
    {
        bool hasReachedLimitToStartNewJump = transform.position.y <= limitToStartNewJump;
        if (!(isJumpingUp || isJumpingDown) || (isJumpingDown && hasReachedLimitToStartNewJump))
        {
            isJumpButtonPushed = true;
            hasReachedTheChange = false;
        }
    }

   
    private void Update()
    {
       
        Vector2 input = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir;

        
        if (isJumpButtonPushed)
        {
            isJumpingUp = true;
            moveDir = new Vector3(input.x, jumpHeight, 0f);
            isJumpButtonPushed = false;

        }

        //when palyerHuman is going up
        else if (isJumpingUp)
        {
            float distanceBeforeChangingDirectionOfJump = jumpHeight - transform.position.y;
            hasReachedTheChange = distanceBeforeChangingDirectionOfJump <= 0.4;
            moveDir = new Vector3(input.x, distanceBeforeChangingDirectionOfJump, 0f);

            if (hasReachedTheChange)
            {
                isJumpingUp = false;
                isJumpingDown = true;
                moveDir = new Vector3(input.x, jumpHeight, 0f);
            }
            
        }

        //when palyerHuman is going down
        else if (isJumpingDown)
        {
            hasReachedTheChange = transform.position.y <= 0.5; 
            float distanceBeforeChangingDirectionOfJump = -(jumpHeight - (jumpHeight - transform.position.y));
            moveDir = new Vector3(input.x, distanceBeforeChangingDirectionOfJump, 0f);

            if (hasReachedTheChange)
            {
                moveDir = new Vector3(input.x, 0, 0f);
                isJumpingDown = false;
            }
        }

        //without jumping
        else
        {
            moveDir = new Vector3(input.x, 0f, 0f);
        }
        
        
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;

    }
    public bool IsWalking()
    {
        return isWalking;
    }
    
}
