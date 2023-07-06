using UnityEngine;
public class RotateMoney : MonoBehaviour
{       
    void Update()
    {
        transform.Rotate(0, 0, 90f * Time.deltaTime); //каждый кадр вращаем монетку   
    }   

}
