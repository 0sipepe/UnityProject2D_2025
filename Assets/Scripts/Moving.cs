using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;

public class Moving : MonoBehaviour
{
    [SerializeField]
    // Он у тебя уже давно Singleton.
    private InputManager gameInput;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpHeight;

    [SerializeField]

    // Не понятное название. Это лимит по расстоянию, или по времени?
    private float reserveDistance = 3;

    private bool isJumpingUp = false;
    private bool isJumpingDown = true;
    private bool hasReachedTheChange = false;
    private bool isGrounded;
    private float startPos;
    private bool isJumpReserved = false;

    private CapsuleCollider2D _collider;
    private Rigidbody2D _rigidbody;


    private void Start()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        gameInput.OnJumpAction += GameInput_OnJumpAction;

        //_updateJumpingState = NotJumping;


    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e)
    {


        if (!isJumpReserved && (IsAvailableToReserveAJump() && (isJumpingDown || isGrounded)))
        {
            isJumpReserved = true;

            Debug.Log("reserved");
        }
    }

    //private Func<Action> _updateJumpingState;
    private void Update()
    {
        Vector2 input = gameInput.GetMovementVectorNormalized();
        if (isJumpReserved && isGrounded)
        {
            isJumpingUp = true;
            isJumpingDown = false;
            _rigidbody.AddForceY(jumpHeight, ForceMode2D.Impulse);
            isJumpReserved = false;
        }
        if (isJumpingUp)
        {

            float distancePassed = DistanceBeforeGroundRaycast();
            float distanceBeforeChangingDirectionOfJump = jumpHeight - distancePassed;

            Debug.Log("is jumping");
            hasReachedTheChange = distanceBeforeChangingDirectionOfJump <= reserveDistance;
            if (hasReachedTheChange)
            {
                isJumpingUp = false;
                isJumpingDown = true;

            }

        }
        if (isJumpingDown)
        {
            if (isGrounded)
            {
                isJumpingDown = false;
            }

        }


        //_updateJumpingState = () => _updateJumpingState();

        _rigidbody.linearVelocityX = input.x * moveSpeed;
    }







    //возвращает, можно ли зарезервировать прижок
    private bool IsAvailableToReserveAJump()
    {
        bool isNearGround = false;
        Vector3 player = transform.position;
        player.y = transform.position.y - _collider.size.y / 2f;

        Debug.DrawRay(player, Vector2.down * reserveDistance, Color.blue);

        RaycastHit2D hit = Physics2D.Raycast(player, Vector2.down, reserveDistance, LayerMask.NameToLayer("Floor"));
        if (hit.collider != null)
        {
            if (hit.distance <= reserveDistance)
            {
                isNearGround = true;
                Debug.Log("is available");
            }
        }
        else
        {
            isNearGround = false;
            Debug.Log("is not available");
        }
        Debug.Log("inside method" + isNearGround);
        return isNearGround;
    }

    //метод возвращает расстояние перед землей
    //разделить на 2 метода - бул из граундед и флоат дистансе
    private float DistanceBeforeGroundRaycast()
    {
        float distanceBeforeGround;
        Vector3 player = transform.position;
        player.y = transform.position.y - _collider.size.y / 2f;

        Vector3 down = new Vector3(0, -3, 0);
        Ray rayToFloor = new Ray(player, down);

        Debug.DrawRay(player, down, Color.blue);

        RaycastHit2D hit = Physics2D.Raycast(player, down);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Debug.Log("grounded");
            isGrounded = true;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Debug.Log("grounded");
            isGrounded = true;
            //if (isJumpReserved)
            //    Jump()

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
