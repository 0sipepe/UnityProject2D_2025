using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    HealthState hp;

    private void Start()
    {
        if(player != null)
        {
            hp = player.GetComponent<HealthState>();

            hp.OnDeath += Hp_OnDeath;
            hp.OnWin += Hp_OnWin;
            hp.OnLoose += Hp_OnLoose;

        }
    }

    private void Hp_OnLoose(object sender, System.EventArgs e)
    {
        //TODO: экран смерти и перевозрождения
        SceneManager.LoadScene(1);
    }

    private void Hp_OnWin(object sender, System.EventArgs e)
    {
        SceneManager.LoadScene(3);
    }

    private void Hp_OnDeath(object sender, System.EventArgs e)
    {
        Debug.Log("i need to load scene 1");
        SceneManager.LoadScene(2);
    }
    private void OnDestroy()
    {
        hp.OnDeath -= Hp_OnDeath;
        hp.OnWin -= Hp_OnWin;
        hp.OnLoose -= Hp_OnLoose;
    }
}

 
