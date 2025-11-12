using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    
    private InputManager gameInput;
  
    private void Start()
    {
        gameInput = InputManager.Instance;
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        //SceneManager.LoadScene(1);
    }
}
