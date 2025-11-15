using UnityEngine;

public class FlowLevel : MonoBehaviour, IEnemy
    
{
    
    private GameObject _playerNow;

    [SerializeField]
    private int _flowSpeed = 5;


    private void Update()
    {
       
        if (_playerNow != null)
        {
            ReactToPlayer();

        }
    }


    public void ReactToPlayer()
    {
        
        _playerNow.transform.Translate(Vector2.left * _flowSpeed * Time.deltaTime);
            
    }
    public void SetPlayer(GameObject player)
    {
        
        _playerNow = player;
    }

    
}
