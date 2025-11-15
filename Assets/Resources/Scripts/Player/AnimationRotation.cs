using UnityEngine;

public class AnimationRotation : MonoBehaviour
{
    private InputManager gameInput;

    void Start()
    {
        gameInput = InputManager.Instance;
    }

  
    void Update()
    {
        Vector2 input = gameInput.GetMovementVectorNormalized();
        if (input.x != 0)
        {
           
            this.transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x) * Mathf.Sign(input.x) * (-1),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }
}
