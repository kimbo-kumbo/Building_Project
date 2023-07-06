using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBarrier : MonoBehaviour
{
    private float rotateSpeed = 30f; //приватное поле типа float , содержит значение скорости вращения
    private Vector3 vector = Vector3.forward;
    private float timeRotate = 5f; //приватное поле типа float , содержит значение времени вращения в одном направлении


    void Update()
    {
        timeRotate -= Time.deltaTime; //каждый кадр изменяем значение таймера
        if(timeRotate <= 5 && timeRotate > 2.5f) //условие при котором инвертируем ось вращения
        {
            transform.Rotate(vector * rotateSpeed * Time.deltaTime);            
        }
        if(timeRotate <= 2.5 && timeRotate > 0) //условие при котором инвертируем ось вращения
        {
            transform.Rotate(-vector * rotateSpeed * Time.deltaTime);
        }
        if( timeRotate <= 0) //условие при котором обновляем таймер
        {
            timeRotate = 5;
        }        
      
    }
}
