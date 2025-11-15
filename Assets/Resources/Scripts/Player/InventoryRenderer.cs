using UnityEngine;

public class InventoryRenderer : MonoBehaviour
{
    private InputManager gameInput;

    [SerializeField]
    private GameObject player;
    private Interaction interaction;
   

    void Start()
    {
        interaction = player.GetComponent<Interaction>();
      
        interaction.OnGettingInventory += Interaction_OnGettingInventory;
        interaction.OnThrowingInventory += Interaction_OnThrowingInventory;
        
    }

    private void Interaction_OnThrowingInventory(object sender, System.EventArgs e)
    {
        Transform holded = interaction.interactableObject.GetTransform();
        interaction.interactableObject.SetIsHolded(false);
        holded.parent = null;

        
    }

    private void Interaction_OnGettingInventory(object sender, System.EventArgs e)
    {
        Transform holded = interaction.interactableObject.GetTransform();
       
        holded.parent = this.transform;
        holded.transform.localPosition = new Vector3(0, -1, 0);
        //зачем это?
        holded = null;
    }
    private void OnDestroy()
    {
        interaction.OnGettingInventory -= Interaction_OnGettingInventory;
        interaction.OnThrowingInventory -= Interaction_OnThrowingInventory;
    }




}
