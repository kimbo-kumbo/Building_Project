using UnityEngine;

public class RotateBarrier : MonoBehaviour
{
    private float rotateSpeed = 30f; 
    private Vector3 vector = Vector3.forward;
    private float timeRotate = 5f; 


    void Update()
    {
        timeRotate -= Time.deltaTime; 
        if(timeRotate <= 5 && timeRotate > 2.5f) 
        {
            transform.Rotate(vector * rotateSpeed * Time.deltaTime);            
        }
        if(timeRotate <= 2.5 && timeRotate > 0) 
        {
            transform.Rotate(-vector * rotateSpeed * Time.deltaTime);
        }
        if( timeRotate <= 0) 
        {
            timeRotate = 5;
        }      
    }
}
