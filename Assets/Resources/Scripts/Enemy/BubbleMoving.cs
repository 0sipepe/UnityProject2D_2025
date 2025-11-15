using UnityEngine;

public class bub : MonoBehaviour
{
    [SerializeField] 
    private float moveSpeed = 5;


    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
