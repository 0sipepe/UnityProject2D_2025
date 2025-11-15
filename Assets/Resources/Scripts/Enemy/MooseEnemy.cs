using UnityEngine;

public class MooseEnemy : MonoBehaviour, IEnemy
{

    private GameObject _player;
    
    private IHoldable foodThatMakesMooseHappy;



    private void Start()
    {
       

    }
    public void SetPlayer(GameObject player)
    {
        _player = player;
    }
   
    private bool IsHappy()
    {
        bool happy = false;
        
        if (foodThatMakesMooseHappy != null)
        {
            Debug.Log("Is grounded berry" + foodThatMakesMooseHappy.IsUnholded());
            if (foodThatMakesMooseHappy.IsUnholded())
            {
                happy =  true;
            }
        }
        return happy;
       
    }

    public void ReactToPlayer() 
    {
        this.transform.GetComponent<AudioSource>().enabled = true;
        if (!IsHappy())
        {
            Debug.Log("moose is happy " + IsHappy());
            _player.GetComponent<HealthState>().IsAlive = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<IHoldable>() != null)
        {
            Debug.Log("berry in moose");
            foodThatMakesMooseHappy = collision.transform.GetComponent<IHoldable>();
        
        }
     
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<IHoldable>() != null)
        {
                       
            foodThatMakesMooseHappy = null;

        }



    }


}
