using UnityEngine;

public class InventoryRenderer : MonoBehaviour
{
    private InputManager gameInput;

    [SerializeField]
    GameObject player;
    private Interaction interaction;
    Vector2 hand = new Vector2(0.4f, 0);

    void Start()
    {
        interaction = player.GetComponent<Interaction>();
      
        interaction.OnGettingInventory += Interaction_OnGettingInventory;
        interaction.OnThrowingInventory += Interaction_OnThrowingInventory;
        
    }

    private void Interaction_OnThrowingInventory(object sender, System.EventArgs e)
    {
        Transform holded = interaction.interactableObject.GetTransform();
        holded.parent = null;
        
    }

    private void Interaction_OnGettingInventory(object sender, System.EventArgs e)
    {
        Transform holded = interaction.interactableObject.GetTransform();
       
        holded.parent = this.transform;
        holded.transform.localPosition = Vector3.zero;
        //зачем это?
        holded = null;
    }

   

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
       


        //убрать отслеживание события нажатия на кнопку интеракт и добавить слушание события, которое будет выбрасывать поднятие и опускание чего-то. 
        

        
    }

 
}
