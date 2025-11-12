using UnityEngine;

public class Berry : MonoBehaviour, IHoldable
{

    public Sprite GetSprite()
    {
        return transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }
}
