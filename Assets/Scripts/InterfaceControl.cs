using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceControl : Move 
{
    [SerializeField] Text textTime; //сериализованное поле типа Text , выводит на Canvas информацию о времени
    [SerializeField] Text textHealt; //сериализованное поле типа Text , выводит на Canvas информацию о текущем здоровье
    [SerializeField] Text textCoins; //сериализованное поле типа Text , выводит на Canvas информацию о собранных монетках
    [SerializeField] Image imageGameOver; //сериализованное поле типа Image , содержит картинку окончания игры
    [SerializeField] int countTime = 60; //сериализованное поле типа int, таймера

    void Start()
    {
        textHealt.text = healt.ToString(); //переносим значение здоровья в текстовое поле
        StartCoroutine(TimeCoroutine()); //запускаем корутину времени       
        imageGameOver.enabled = false; //деактивируем картинку окончания игры
    }
  

    private void OnEnable()
    {
        Move.CoinsEvent += CoinsPlus; //подписываемся на событие CoinsEvent и вызываем метод CoinsPlus в случае уведомления
        Move.HealtEvent += HealtMinus; //подписываемся на событие HealtEvent и вызываем метод HealtMinus в случае уведомления
    }

    private void OnDisable()
    {
        Move.CoinsEvent -= CoinsPlus; //отписываемся от события CoinsEvent
        Move.HealtEvent -= HealtMinus; //отписываемся от события HealtEvent
    }

    private void CoinsPlus() //метод увеличивающий колличество монеток
    {
        countCoins++;
        textCoins.text = countCoins.ToString();
    }

    private void HealtMinus() //метод уменьшающий жизни
    {
        healt--;
        textHealt.text = healt.ToString();
    }


    IEnumerator TimeCoroutine() //корутина окончания игры
    {
        while (true) //бесконечный цикл
        {
            countTime--; //уменьшаем время
            textTime.text = countTime.ToString(); //обновляем текстовое поле textTime
            yield return new WaitForSeconds(1f); //ожидаем 1 секунду
            if (_transform.position.y <= -3f || healt <= 0 || countTime <= 0) //условие окончания игры
            {
                imageGameOver.enabled = true; //запускаем картинку GAME OVER
                Time.timeScale = 0; //останавливаем время
                yield return new WaitForSeconds(3f); //ожидаем 3 секунды
                EditorApplication.isPaused = true; //останавливаем игру
            }
        }

    }
}
