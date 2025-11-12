using UnityEngine;

public class InteractableObjectsGravity : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    private float gravity = 9.8f;
    private void Update()
    {

        bool hit = Physics2D.Raycast(transform.position, Vector2.down, this.GetComponent<CircleCollider2D>().bounds.extents.y + 0.1f, layerMask);

        Debug.DrawRay(transform.position, Vector3.down * (this.GetComponent<CircleCollider2D>().bounds.extents.y +0.01f), Color.red);
       
        if (!hit)
        {
            transform.Translate(Vector2.down *  gravity * Time.deltaTime);
        }
       
    }
}
