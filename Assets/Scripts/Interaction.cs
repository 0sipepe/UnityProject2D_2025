using UnityEngine;
using System;

public class Interaction : MonoBehaviour
{

   
    private InputManager gameInput;

    private bool isHolding = false;
    public event EventHandler OnChangingInventory;

    //private GameObject interactableObject;
    internal IHoldable interactableObject;
    private IHoldable interactableObjectNearPlayer;


    void Start()
    {
        gameInput = InputManager.Instance;
        gameInput.OnInteractAction += GameInput_OnInteractAction;
       
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {

        // вместо логического И сделать вложенный if
        if (isHolding && (interactableObjectNearPlayer == null))
        {
            isHolding = false;
        }
        else if (isHolding && (interactableObjectNearPlayer != null))
        {
            interactableObject = interactableObjectNearPlayer;
        }
        else if (!isHolding && (interactableObjectNearPlayer != null))
        {
            Debug.Log("get");
            interactableObject = interactableObjectNearPlayer;
            isHolding = true;
        }
        OnChangingInventory?.Invoke(this, EventArgs.Empty);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable Objects"))
        {
            Debug.Log(collision);
            Debug.Log(collision.transform.GetComponent<IHoldable>());
            interactableObjectNearPlayer = collision.transform.GetComponent<IHoldable>();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        interactableObjectNearPlayer = null;
    }
   


}
