using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;

public class Moving : MonoBehaviour
{
    [SerializeField]
    // ќн у теб€ уже давно Singleton.
    private InputManager gameInput;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpHeight;

    [SerializeField]
    // Ќе пон€тное название. Ёто лимит по рассто€нию, или по времени?
    private float limitToStartNewJump;

    private bool isJumpingUp = false;
    private bool isJumpingDown = false;
    private bool hasReachedTheChange = false;
    private float startPos;
    private bool isJumpReserved = false;

    private CapsuleCollider2D _collider;
    private Rigidbody2D _rigidbody;


    private void Start()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        gameInput.OnJumpAction += GameInput_OnJumpAction;

        _updateJumpingState = NotJumping;
    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e)
    {
        //Debug.Log("distance before ground = " + IsNearGroundRaycast());
        if (!isJumpReserved && DistanceBeforeGroundRaycast() <= 0.4)
        {
            isJumpReserved = true;
            
           //Debug.Log("button pushed && reserved a jump");
        }
    }

    private Func<Action> _updateJumpingState;  
    private void Update()
    {
        Vector2 input = gameInput.GetMovementVectorNormalized();

        _updateJumpingState = () => _updateJumpingState();

        _rigidbody.linearVelocityX = input.x * moveSpeed;
    }

   
    
    // ѕлохое назнавние: суд€ по нему, метод должен возвращать bool
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
        }
        else
        {
            distanceBeforeGround = 5;
        }
        return distanceBeforeGround;
    }
    


    private Action NotJumping()
    {
        if (!(isJumpReserved && !isJumpingDown))
        {
            return NotJumping();
        }
        
        isJumpingUp = true;
        _rigidbody.AddForceY(jumpHeight);
        isJumpReserved = false;

        return JumpingUp();
    }

    private Action JumpingUp()
    {
        if (!hasReachedTheChange)
        {
            float distancePassed = transform.position.y - startPos;
            float distanceBeforeChangingDirectionOfJump = jumpHeight - distancePassed;

            hasReachedTheChange = distanceBeforeChangingDirectionOfJump <= limitToStartNewJump;
            return JumpingUp();
        }

        //Debug.Log("Has reached the Change");
        isJumpingUp = false;
        isJumpingDown = true;
        return JumpingDown();
    }

    private Action JumpingDown()
    {
        // Debug.Log("distance before ground jumping down = " + IsNearGroundRaycast());
        if (DistanceBeforeGroundRaycast() > 0.001f)
        {
            return JumpingDown();
        }

        isJumpingDown = false;
        return NotJumping();
    }

}
