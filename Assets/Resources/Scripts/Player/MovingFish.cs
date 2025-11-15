using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class MovingFish : MonoBehaviour
{
   
    private InputManager gameInput;

    [SerializeField]
    private float moveSpeed;

    private bool canMove = true;


   

    public event EventHandler OnJump;


    private void Start()
    {
        gameInput = InputManager.Instance;
        gameInput.OnJumpAction += GameInput_OnJumpAction;
        GetComponent<HealthState>().OnBanControl += MovingFish_OnBanControl;
        GetComponent<HealthState>().OnAvailControl += MovingFish_OnAvailControl;
    }

    private void MovingFish_OnAvailControl(object sender, EventArgs e)
    {
        canMove = true;
    }

    private void MovingFish_OnBanControl(object sender, EventArgs e)
    {
        canMove= false;
    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e)
    {
        if (canMove)
        {
            Move(20);
            
        }
        else
        {
            Move(1);
        }
        OnJump?.Invoke(this, EventArgs.Empty);

    }

    private void Update()
    {

        if (canMove)
        {
            Move(1);
        }
        

    }

    private void Move(int param)
    {
        Vector2 input = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(input.x, input.y, 0f);

        transform.position += moveDir * moveSpeed * param * Time.deltaTime;
    }
    private void OnDestroy()
    {
        gameInput.OnJumpAction -= GameInput_OnJumpAction;
        GetComponent<HealthState>().OnBanControl -= MovingFish_OnBanControl;
        GetComponent<HealthState>().OnAvailControl -= MovingFish_OnAvailControl;
    }
}
