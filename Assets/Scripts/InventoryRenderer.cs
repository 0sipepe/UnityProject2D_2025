using UnityEngine;

public class InventoryRenderer : MonoBehaviour
{
    private InputManager gameInput;

    [SerializeField]
    GameObject player;
    private Interaction interaction;

    void Start()
    {
        interaction = player.GetComponent<Interaction>();
        interaction.OnChangingInventory += InventoryRenderer_OnChangingInventory;
       
    }

    private void InventoryRenderer_OnChangingInventory(object sender, System.EventArgs e)
    { 
        //Debug.Log(player);
        //Debug.Log(transform.GetComponent<SpriteRenderer>().sprite);
        //Debug.Log(player.GetComponent<Interaction>());
        Debug.Log(interaction.interactableObject);
        //Debug.Log(player.GetComponent<Interaction>().interactableObject.GetSprite());
        try
        {
            transform.GetComponent<SpriteRenderer>().sprite = interaction.interactableObject.GetSprite();
        }
        catch 
        {
                
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
       


        //убрать отслеживание события нажатия на кнопку интеракт и добавить слушание события, которое будет выбрасывать поднятие и опускание чего-то. 
        

        
    }

 
}
