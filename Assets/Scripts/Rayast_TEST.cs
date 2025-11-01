using UnityEngine;

public class Rayast_TEST : MonoBehaviour
{

    [SerializeField]
    private InputManager gameInput;
    private CapsuleCollider2D _collider;
     
   
    void Start()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        gameInput.OnJumpAction += GameInput_OnJumpAction;
    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e)
    {
        IsNearGroundRaycast();
    }

   
    void Update()
    {
        IsNearGroundRaycast();
    }
    private void IsNearGroundRaycast()
    {
        Vector3 player = transform.position;
        player.x = transform.position.x + _collider.size.x/2f + 0.01f ;

        Vector3 right = new Vector3(1, 0, 0);
        Ray rayToFloor = new Ray(player, right);

        Debug.DrawRay(player, right, Color.blue);

        RaycastHit2D hit = Physics2D.Raycast(player, right, 1);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider);
        }


      

        //float distanceBeforeGround;
        //Vector3 rayFromCapsuleBottom = transform.position;

        //Vector3 right = new Vector3(15, 0, 0);

        //Ray rayToFloor = new Ray(rayFromCapsuleBottom, right);

        //Debug.Log(transform.position);
        //Debug.Log(right);

        //RaycastHit hit;
        //Debug.DrawRay(rayFromCapsuleBottom, right, Color.blue);


        //if (Physics.Raycast(rayToFloor, out hit, 15))
        //{
        //    distanceBeforeGround = hit.distance;
        //    Debug.Log($"Расстояние от низа объекта до пола: {distanceBeforeGround}");


        //    return true;
        //}
        //else
        //{
        //    Debug.DrawRay(rayFromCapsuleBottom, Vector3.right * hit.distance, Color.blue);
        //    Debug.Log($"не вышло");
        //    return false;
        //}
    }
}
