using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceControl : Move 
{
    [SerializeField] Text textTime; //��������������� ���� ���� Text , ������� �� Canvas ���������� � �������
    [SerializeField] Text textHealt; //��������������� ���� ���� Text , ������� �� Canvas ���������� � ������� ��������
    [SerializeField] Text textCoins; //��������������� ���� ���� Text , ������� �� Canvas ���������� � ��������� ��������
    [SerializeField] Image imageGameOver; //��������������� ���� ���� Image , �������� �������� ��������� ����
    [SerializeField] int countTime = 60; //��������������� ���� ���� int, �������

    void Start()
    {
        textHealt.text = healt.ToString(); //��������� �������� �������� � ��������� ����
        StartCoroutine(TimeCoroutine()); //��������� �������� �������       
        imageGameOver.enabled = false; //������������ �������� ��������� ����
    }
  

    private void OnEnable()
    {
        Move.CoinsEvent += CoinsPlus; //������������� �� ������� CoinsEvent � �������� ����� CoinsPlus � ������ �����������
        Move.HealtEvent += HealtMinus; //������������� �� ������� HealtEvent � �������� ����� HealtMinus � ������ �����������
    }

    private void OnDisable()
    {
        Move.CoinsEvent -= CoinsPlus; //������������ �� ������� CoinsEvent
        Move.HealtEvent -= HealtMinus; //������������ �� ������� HealtEvent
    }

    private void CoinsPlus() //����� ������������� ����������� �������
    {
        countCoins++;
        textCoins.text = countCoins.ToString();
    }

    private void HealtMinus() //����� ����������� �����
    {
        healt--;
        textHealt.text = healt.ToString();
    }


    IEnumerator TimeCoroutine() //�������� ��������� ����
    {
        while (true) //����������� ����
        {
            countTime--; //��������� �����
            textTime.text = countTime.ToString(); //��������� ��������� ���� textTime
            yield return new WaitForSeconds(1f); //������� 1 �������
            if (_transform.position.y <= -3f || healt <= 0 || countTime <= 0) //������� ��������� ����
            {
                imageGameOver.enabled = true; //��������� �������� GAME OVER
                Time.timeScale = 0; //������������� �����
                yield return new WaitForSeconds(3f); //������� 3 �������
                EditorApplication.isPaused = true; //������������� ����
            }
        }

    }
}
