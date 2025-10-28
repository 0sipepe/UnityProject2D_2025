using UnityEngine;
using System;

public class Interaction : MonoBehaviour
{

    [SerializeField]
    private InputManager gameInput;
    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;

    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
