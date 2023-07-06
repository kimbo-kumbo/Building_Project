using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBarrier : MonoBehaviour
{
    private float speedScale = 6f; //��������� ���� ���� float , �������� �������� �������� ��������� ��������

    void Update()
    {
        transform.localScale += new Vector3(0, 0, 1 * Time.deltaTime * speedScale); //������ ���� ������ ������ �������
        if(transform.localScale.z >= 20) //������� ��� ������� ����������� �������� ��������� ��������
        {
            speedScale = -6f; 
        }
        if(transform.localScale.z <= 5) //������� ��� ������� ����������� �������� ��������� ��������
        {
            speedScale = 6f; 
        }
    }

}
