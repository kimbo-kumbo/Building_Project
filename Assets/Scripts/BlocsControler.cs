using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BlocsControler : MonoBehaviour
{
    [SerializeField] Text textProgres; //��������������� ���� ���� Text , ������� �� Canvas ���������� � ���������� ������
    [SerializeField] Transform[] blocks; //��������������� ���� ���� Transform , �������� ������ �� ������ ����������� Transform
    public int _progres = 0; //��������� ���� ���� int, ����������� ����� ���������� ������
    private float _step = 20f; //��������� ���� ���� float, �������� �������� ���� ���������
    private float _lastZ = 180f; //��������� ���� ���� float, �������� �������� ������ ����� ����
    private int _currentBlock = 0; //��������� ���� ���� , �������� index �������� �����
    public GameObject _prefabs; //��������� ���� ���� GameObject, �������� ������ �� ������


    private void OnTriggerEnter(Collider other) //�������� ������������
    {
        if (other.gameObject.tag == "TrigerLoadLevel") //���� ��������� ���� ���������� ������
        {
            UpdateLevel(); //�������� ����� ���������� ������
        }
    }

    private void UpdateLevel()
    {
        _progres++; //����������� ������� ���������
        textProgres.text = "Blocks passed: " + _progres.ToString(); //��������� �������� �������� �� Canvas
        _lastZ += _step; //����������� ��� �������� �� ��� Z ��� ��������� ���������
        var position = blocks[_currentBlock].position; // �������� ������� �������� ����� 
        position.z = _lastZ; //��������� ������� 
        blocks[_currentBlock].position = position; //��������� ��������� � ����� �������
        _currentBlock++; //����������� ������
        if (_currentBlock >= blocks.Length) //������� ������ �������
        {
            _currentBlock = 0;
        }

        CreatePrefabs(); //������ ��������� �������
    }

    private void CreatePrefabs() //����� �������� ������� �� ������ �������
    {
        var prefabs = GameObject.Instantiate(_prefabs,new Vector3(Random.Range(-5f, 5f), 1, _lastZ), Quaternion.identity);        
    }
}
