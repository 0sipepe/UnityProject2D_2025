using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;

public class Moving : MonoBehaviour
{
    private InputManager gameInput;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpHeight;

    [SerializeField]
    private float reserveDistance = 3;

   
    private bool isJumpingDown = true;
    private bool isGrounded;
    private bool isJumpReserved = false;

    private CapsuleCollider2D _collider;
    private Rigidbody2D _rigidbody;


    private void Start()
    {
        gameInput = InputManager.Instance;
        gameInput.OnJumpAction += GameInput_OnJumpAction;

        _collider = transform.GetComponent<CapsuleCollider2D>();
        _rigidbody = transform.GetComponent<Rigidbody2D>();

        //_updateJumpingState = NotJumping;


    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e)
    {

        //вместо jumpUp и jumpDown написать измерение по скорости 
        if (!isJumpReserved && (IsAvailableToReserveAJump()))
        {
            isJumpReserved = true;
            Debug.Log("reserved");
           
        }
        if (isGrounded)
        {
            Jump();
        }
    }

    //private Func<Action> _updateJumpingState;
    private void Update()
    {

        isJumpingDown = _rigidbody.linearVelocityY <=  0;

        Vector2 input = gameInput.GetMovementVectorNormalized();
      
        _rigidbody.linearVelocityX = input.x * moveSpeed;
    }

    private void Jump()
    {

        _rigidbody.AddForceY(jumpHeight, ForceMode2D.Impulse);
        isJumpReserved = false;
        isGrounded = false;

    }





    //возвращает, можно ли зарезервировать прижок
    private bool IsAvailableToReserveAJump()
    {
        bool isAvailable = false;
        Vector3 player = transform.position;
        player.y = transform.position.y - _collider.size.y / 2f;

        

        Debug.DrawRay(player, Vector2.down * reserveDistance , Color.red);

        RaycastHit2D hit = Physics2D.Raycast(player, Vector2.down * reserveDistance, LayerMask.NameToLayer("Floor"));
        

        if (hit.collider != null)
        {
            if (isJumpingDown)
            {
                isAvailable = true;
                Debug.Log("is available");
            }
        }
        else
        {
            isAvailable = false;
            Debug.Log("is not available");
        }
        
        return isAvailable;
    }

    //метод возвращает расстояние перед землей
    private float DistanceBeforeGroundRaycast()
    {
        float distanceBeforeGround;
        Vector3 player = transform.position;
        player.y = transform.position.y - _collider.size.y / 2f;

        Debug.DrawRay(player, Vector2.down * reserveDistance, Color.blue);

        RaycastHit2D hit = Physics2D.Raycast(player, Vector2.down * reserveDistance, reserveDistance, LayerMask.NameToLayer("Floor"));
        if (hit.collider != null)
        {
            distanceBeforeGround = hit.distance;
            Debug.Log(distanceBeforeGround);
        }
        else
        {
            distanceBeforeGround = 5;
        }
        return distanceBeforeGround;
    }


    //возвращает, столкнулись ли мы с полом
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Debug.Log("grounded");
            isGrounded = true;
            if (isJumpReserved)
            {
                Jump();
            }

        }
    }



    //недоделанная стейт-машина

    //    private Action NotJumping()
    //    {
    //        if (!(isJumpReserved && !isJumpingDown))
    //        {
    //            return NotJumping();
    //        }

    //        isJumpingUp = true;
    //        _rigidbody.AddForceY(jumpHeight);
    //        isJumpReserved = false;

    //        return JumpingUp();
    //    }

    //    private Action JumpingUp()
    //    {
    //        if (!hasReachedTheChange)
    //        {
    //            float distancePassed = transform.position.y - startPos;
    //            float distanceBeforeChangingDirectionOfJump = jumpHeight - distancePassed;

    //            hasReachedTheChange = distanceBeforeChangingDirectionOfJump <= limitToStartNewJump;
    //            return JumpingUp();
    //        }

    //        //Debug.Log("Has reached the Change");
    //        isJumpingUp = false;
    //        isJumpingDown = true;
    //        return JumpingDown();
    //    }

    //    private Action JumpingDown()
    //    {
    //        // Debug.Log("distance before ground jumping down = " + IsNearGroundRaycast());
    //        if (DistanceBeforeGroundRaycast() > 0.001f)
    //        {
    //            return JumpingDown();
    //        }

    //        isJumpingDown = false;
    //        return NotJumping();
    //    }

}
