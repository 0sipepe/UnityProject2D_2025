using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private InteractableObjectSO interactableObjectSO;

    public InteractableObjectSO getInteractableSO()
    {
        return interactableObjectSO;
    }
}
