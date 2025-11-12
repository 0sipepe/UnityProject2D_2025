using UnityEngine;

[CreateAssetMenu(fileName = "InteractableObject", menuName = "Scriptable Objects/InteractableObject")]
public class InteractableObjectSO : ScriptableObject
{
    [SerializeField]
    private GameObject prefab;


    [SerializeField]
    private Sprite sprite;

    [SerializeField] 
    private string type;

   
}
