using UnityEngine;

public class BubbleDeleter : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.collider.CompareTag("Bubble"))
        {
            Destroy(collision.gameObject);
        }
    }
}
