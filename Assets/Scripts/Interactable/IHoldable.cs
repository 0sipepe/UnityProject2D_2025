using UnityEngine;

public interface IHoldable
{
    public Sprite GetSprite();
    public Transform GetTransform();
    public void SetIsHolded(bool state);
}
