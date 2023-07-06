using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Move : MonoBehaviour
{
    [SerializeField] float forceJamp = 7f; //сериализованное поле типа float , содержит значение силы прыжка
    [SerializeField] float speedmove = 8f; //сериализованное поле типа float , содержит значение скорости передвижения
    [SerializeField] bool onGround = false; //сериализованное поле типа bool , определяет находится ли объект на поверхности
    [SerializeField] Rigidbody rb;
    public Transform _transform; //публичное поле , содержит ссылку на компонент Transform 
    protected int countCoins = 0; //защищенное поле типа инт , накапливает собранные монетки
    protected int healt = 3; //защищенное поле типа инт , содержит макс значение здоровья
    public bool changeNewInput; //публичное поле типа bool , при включении меняет систему ввода
    private NewInput _newInput; //приватное поле , содержит ссылку на класс NewInput

    ////////////////////////

    public static Action CoinsEvent; //создаём событие CoinsEvent
    public static Action HealtEvent; //создаём событие HealtEvent

    ////////////////////////

    private void Awake()
    {        
        _newInput = new NewInput(); //создаём экземпляр класса NewInput  
    }

    private void OnEnable()
    {
        _newInput.Enable(); // активируем NewInput
    }

    private void OnDisable()
    {
        _newInput.Disable(); //деактивируем NewInput
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>(); //получаем ссылку на компонент Rigidbody
        StartCoroutine(SpeedCoroutine()); // запускаем корутину     
    }

    private void OnTriggerEnter(Collider other) //проверка столкновений
    {
        if (other.gameObject.tag == "Enemy") //если столкнулись с врагом
        {            
            HealtEvent?.Invoke(); // вызываем событие HealtEvent
            speedmove = 8f; //задаём скорость передвижения
        }

        if (other.gameObject.tag == "Money") //если столкнулись с монеткой
        {
            other.gameObject.SetActive(false); //отключаем объект           
            CoinsEvent?.Invoke(); // вызываем событие CoinsEvent
        }
    } 

    void Update()
    {
        transform.Translate(Vector3.forward * speedmove * Time.deltaTime); //постоянно движемся вперёд

        if (changeNewInput == true) //проверяем условия для прыжка ,используя новую систему ввода
        {
            forceJamp = 7f; //задаём силу прыжка
            _newInput.Move.Jamp.performed += context => JampMetod(); // подписываемся на событие Jamp и вызываем метод JampMetod()        
        }
        if((changeNewInput == false) && Input.GetKeyDown(KeyCode.Space) && onGround) //проверяем условия для прыжка используя старую систему ввода
        {
            forceJamp = 3f; //задаём силу прыжка
            JampMetod(); //вызываем метод прыжка
        }

        MoveMetod(); //вызывем метод изменения положения
    }
    

  
    private void OnCollisionStay(Collision collision) => onGround = true; //при соприкосновении с поверхностью получаем возможность прыгать

    private void OnCollisionExit(Collision collision) => onGround = false; //при отрыве от поверхности лишаемся возможности прыгать

    IEnumerator SpeedCoroutine() //корутина увеличивающая скорость объекта каждую секунду
    {
        while (true) //бесконечный цикл
        {
            speedmove += 0.5f;
            yield return new WaitForSeconds(1f);
        }
    }

    private void JampMetod() // метод реализующий прыжок за счет физики
    {
        if (onGround) // если объект на земле
        {
            rb.AddForce(Vector3.up * forceJamp, ForceMode.VelocityChange); //придаём импульс по оси Y
        }
    }

    private void MoveMetod() // метод реализующий изменения положения вправо/влево ,используя две системы ввода
    {
        Vector3 move = Vector3.right * speedmove * Time.deltaTime;

        if (changeNewInput == true) //если NewInputSystem включена
        {
            var direction = _newInput.Move.SlideMove.ReadValue<Vector2>(); //считываем значение Vector2 при срабатывании Actions "SlideMove"
            transform.Translate(move * direction);
        }
        if (changeNewInput == false) //если NewInputSystem выключена
        {            
           if (Input.GetKey(KeyCode.D)) transform.Translate(move);
           if (Input.GetKey(KeyCode.A)) transform.Translate(-move);
        }      
    }
}

