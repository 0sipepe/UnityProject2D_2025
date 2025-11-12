using UnityEngine;

public class Berry : MonoBehaviour, IHoldable
{

    [SerializeField]
    private InteractableObjectsGravity gravity;



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
        gravity.enabled = !state;
    }

}
