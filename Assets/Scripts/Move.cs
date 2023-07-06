using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Move : MonoBehaviour
{
    [SerializeField] float forceJamp = 7f; //��������������� ���� ���� float , �������� �������� ���� ������
    [SerializeField] float speedmove = 8f; //��������������� ���� ���� float , �������� �������� �������� ������������
    [SerializeField] bool onGround = false; //��������������� ���� ���� bool , ���������� ��������� �� ������ �� �����������
    [SerializeField] Rigidbody rb;
    public Transform _transform; //��������� ���� , �������� ������ �� ��������� Transform 
    protected int countCoins = 0; //���������� ���� ���� ��� , ����������� ��������� �������
    protected int healt = 3; //���������� ���� ���� ��� , �������� ���� �������� ��������
    public bool changeNewInput; //��������� ���� ���� bool , ��� ��������� ������ ������� �����
    private NewInput _newInput; //��������� ���� , �������� ������ �� ����� NewInput

    ////////////////////////

    public static Action CoinsEvent; //������ ������� CoinsEvent
    public static Action HealtEvent; //������ ������� HealtEvent

    ////////////////////////

    private void Awake()
    {        
        _newInput = new NewInput(); //������ ��������� ������ NewInput  
    }

    private void OnEnable()
    {
        _newInput.Enable(); // ���������� NewInput
    }

    private void OnDisable()
    {
        _newInput.Disable(); //������������ NewInput
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>(); //�������� ������ �� ��������� Rigidbody
        StartCoroutine(SpeedCoroutine()); // ��������� ��������     
    }

    private void OnTriggerEnter(Collider other) //�������� ������������
    {
        if (other.gameObject.tag == "Enemy") //���� ����������� � ������
        {            
            HealtEvent?.Invoke(); // �������� ������� HealtEvent
            speedmove = 8f; //����� �������� ������������
        }

        if (other.gameObject.tag == "Money") //���� ����������� � ��������
        {
            other.gameObject.SetActive(false); //��������� ������           
            CoinsEvent?.Invoke(); // �������� ������� CoinsEvent
        }
    } 

    void Update()
    {
        transform.Translate(Vector3.forward * speedmove * Time.deltaTime); //��������� �������� �����

        if (changeNewInput == true) //��������� ������� ��� ������ ,��������� ����� ������� �����
        {
            forceJamp = 7f; //����� ���� ������
            _newInput.Move.Jamp.performed += context => JampMetod(); // ������������� �� ������� Jamp � �������� ����� JampMetod()        
        }
        if((changeNewInput == false) && Input.GetKeyDown(KeyCode.Space) && onGround) //��������� ������� ��� ������ ��������� ������ ������� �����
        {
            forceJamp = 3f; //����� ���� ������
            JampMetod(); //�������� ����� ������
        }

        MoveMetod(); //������� ����� ��������� ���������
    }
    

  
    private void OnCollisionStay(Collision collision) => onGround = true; //��� ��������������� � ������������ �������� ����������� �������

    private void OnCollisionExit(Collision collision) => onGround = false; //��� ������ �� ����������� �������� ����������� �������

    IEnumerator SpeedCoroutine() //�������� ������������� �������� ������� ������ �������
    {
        while (true) //����������� ����
        {
            speedmove += 0.5f;
            yield return new WaitForSeconds(1f);
        }
    }

    private void JampMetod() // ����� ����������� ������ �� ���� ������
    {
        if (onGround) // ���� ������ �� �����
        {
            rb.AddForce(Vector3.up * forceJamp, ForceMode.VelocityChange); //������ ������� �� ��� Y
        }
    }

    private void MoveMetod() // ����� ����������� ��������� ��������� ������/����� ,��������� ��� ������� �����
    {
        Vector3 move = Vector3.right * speedmove * Time.deltaTime;

        if (changeNewInput == true) //���� NewInputSystem ��������
        {
            var direction = _newInput.Move.SlideMove.ReadValue<Vector2>(); //��������� �������� Vector2 ��� ������������ Actions "SlideMove"
            transform.Translate(move * direction);
        }
        if (changeNewInput == false) //���� NewInputSystem ���������
        {            
           if (Input.GetKey(KeyCode.D)) transform.Translate(move);
           if (Input.GetKey(KeyCode.A)) transform.Translate(-move);
        }      
    }
}

