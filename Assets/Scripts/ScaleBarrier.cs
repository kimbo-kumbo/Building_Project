using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBarrier : MonoBehaviour
{
    private float speedScale = 6f; //приватное поле типа float , содержит значение скорости изменения размеров

    void Update()
    {
        transform.localScale += new Vector3(0, 0, 1 * Time.deltaTime * speedScale); //каждый кадр меняем размер объекта
        if(transform.localScale.z >= 20) //условие при котором инвертируем скорость изменения размеров
        {
            speedScale = -6f; 
        }
        if(transform.localScale.z <= 5) //условие при котором инвертируем скорость изменения размеров
        {
            speedScale = 6f; 
        }
    }

}
