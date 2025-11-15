using UnityEngine;

public class Berry : MonoBehaviour, IHoldable
{

    [SerializeField]
    private InteractableObjectsGravity gravity;
    [SerializeField]
    private LayerMask layerMask;
    private bool isHolded;


    private void Start()
    {
       
    }
    public Sprite GetSprite()
    {
        return transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }
    public Transform GetTransform()
    {
        return this.transform;
    }
    public void SetIsHolded(bool state)
    {
        if (gravity != null)
        {
            gravity.enabled = !state;
        }
        isHolded = state;
    }

    public bool IsGrounded()
    {
        bool hit = Physics2D.Raycast(transform.position, Vector2.down ,  (this.GetComponent<CircleCollider2D>().bounds.extents.y + 0.03f), layerMask);

        Debug.DrawRay(transform.position, Vector3.down * (this.GetComponent<CircleCollider2D>().bounds.extents.y + 0.03f), Color.red);

        if (!hit)
        {
          
            return false;
        }
        return true;
    }
    public bool IsUnholded()
    {
        return !isHolded;
    }

   





}
