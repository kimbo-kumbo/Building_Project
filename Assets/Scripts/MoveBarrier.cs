using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBarrier : MonoBehaviour
{
    private float speed = 4f; //приватное поле типа float , содержит значение скорости передвижения
    private Vector3 vector = Vector3.right;
    
    void Update()
    {
        transform.position += vector * Time.deltaTime * speed; //каждый кадр двигаем объект

        if (transform.position.x <= -5f) //условие при котором инвертируем скорость передвижения
        {
            vector = Vector3.right;
        }
        if(transform.position.x >= 5f) //условие при котором инвертируем скорость передвижения
        {
            vector = Vector3.left;
        }

        
    }
}
