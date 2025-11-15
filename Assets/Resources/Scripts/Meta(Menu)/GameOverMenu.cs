using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour 
{
    [SerializeField] private Button btnRestartGame;
    [SerializeField] private Button btnGoToLobby;
    [SerializeField] private Button btnExit;


    private void Start()
    {

        btnRestartGame.onClick.AddListener(RestartGame);
        btnGoToLobby.onClick.AddListener(GoToLobby);
        btnExit.onClick.AddListener(Exit);

        LoadText();


    }
    private void LoadText()
    {
      //TODO: менять надпись в зависимости от исхода
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void GoToLobby()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();

        // Для отладки в редакторе
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
