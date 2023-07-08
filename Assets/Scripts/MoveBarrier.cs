using UnityEngine;

public class MoveBarrier : MonoBehaviour
{
    private float speed = 4f; 
    private Vector3 vector = Vector3.right;
    
    void Update()
    {
        transform.position += vector * Time.deltaTime * speed; 

        if (transform.position.x <= -5f) 
        {
            vector = Vector3.right;
        }
        if(transform.position.x >= 5f) 
        {
            vector = Vector3.left;
        }        
    }
}
