using UnityEngine;

public class InteractableObjectsGravity : MonoBehaviour
{
 

    private float gravity = 9.8f;
    private void Update()
    {

        bool isGrounded = this.GetComponent<Berry>().IsGrounded();

        if (!isGrounded)
        {
            transform.Translate(Vector2.down * gravity * Time.deltaTime);
        }


    }
}
