using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyMenu : MonoBehaviour
{
    [SerializeField] private Button btnStartGame;
    [SerializeField] private Button btnLoadSave;
    [SerializeField] private Button btnExit;

    private void Start()
    {

        btnStartGame.onClick.AddListener(StartGame);
        btnLoadSave.onClick.AddListener(LoadPreviousGame);
        btnExit.onClick.AddListener(Exit);

    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadPreviousGame()
    {
        //TODO: сохранять из файла какие-то штуки
        btnLoadSave.GetComponentInChildren<TextMeshProUGUI>().text = "not implemented yet";
    }
    public void Exit()
    {
         Application.Quit();

                // Для отладки в редакторе
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
        //TODO: exit
    }
}
