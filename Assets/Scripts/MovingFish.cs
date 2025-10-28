using UnityEngine;

public class MovingFish : MonoBehaviour
{
    [SerializeField]
    private InputManager gameInput;
    [SerializeField]
    private float moveSpeed;

    private bool isMoving;
    private bool isJumping;

    private void Start()
    {
        gameInput.OnJumpAction += GameInput_OnJumpAction;
    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e)
    {
        isJumping = true;
        Debug.Log("jump");
    }

    private void Update()
    {
        Vector2 input = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(input.x, input.y, 0f);

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        isMoving = moveDir != Vector3.zero;

    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
