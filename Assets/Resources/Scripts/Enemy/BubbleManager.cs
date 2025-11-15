using UnityEngine;

public class BubbleManager : MonoBehaviour
{


    [SerializeField]
    private GameObject bubble;
    [SerializeField]
    private float coolDown;
    [SerializeField] 
    private float spawnRangeHorisontal;


    private float timeElapsed = 0;



    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= coolDown)
        {
            float originX = Random.Range(transform.position.x - spawnRangeHorisontal / 2, transform.position.x + spawnRangeHorisontal / 2);
            Vector3 bubbleOrigin = new Vector3(originX, transform.position.y, 20);

            Instantiate(bubble,
                bubbleOrigin,
                Quaternion.identity, 
                transform);

            timeElapsed = 0;
        }

    }
}
