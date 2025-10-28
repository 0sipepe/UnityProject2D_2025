using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    [SerializeField]
    private InputManager gameInput;
    [SerializeField]


    public static GameObject player;

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadSceneAsync(0);  
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        SceneManager.UnloadSceneAsync(0);
        SceneManager.LoadSceneAsync(1);
    }
}
