using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    [SerializeField] TutorialManager _tutorialManager;
    [SerializeField] Animator animator;
    [SerializeField] float forceJamp = 7f;
    [SerializeField] float speedmove = 8f;
    [SerializeField] bool onGround = false;
    private Rigidbody rb;
    [HideInInspector] public Transform _transform;
    [HideInInspector] public int countCoins = 0;
    [HideInInspector] public int healt = 3;
    public bool changeNewInput;
    private NewInput _newInput;

    ////////////////////////

    public static Action CoinsEvent;
    public static Action HealtEvent;

    ////////////////////////

#if UNITY_STANDALONE
    private void Awake()
    {
        _newInput = new NewInput();
    }
#endif

    private void OnEnable()
    {
        _newInput.Enable();

#if UNITY_STANDALONE
        forceJamp = 7f;
        _newInput.Move.Jamp.performed += context => JampMetod();
#endif
#if UNITY_EDITOR
        forceJamp = 3f;
#endif

    }

    private void OnDisable()
    {
        _newInput.Disable();
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(SpeedCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            HealtEvent?.Invoke();
            speedmove = 8f;
        }

        if (other.gameObject.tag == "Money")
        {
            //_tutorialManager.OnEvent(TutorialEvent.CointTake);
            other.gameObject.SetActive(false);
            CoinsEvent?.Invoke();
            if (SceneManager.GetActiveScene().name == "Training"  && _tutorialManager.IsEnoughCoins)
            {
                _tutorialManager.OnEvent(TutorialEvent.ItemsTakeFinish);
            }
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speedmove * Time.deltaTime);
        MoveMetod();

#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            JampMetod();
        }
#endif
    }

    private void OnCollisionStay(Collision collision)
    {
        
        onGround = true;
    }
        

    private void OnCollisionExit(Collision collision)
    {
        onGround = false;        
    }

    IEnumerator SpeedCoroutine()
    {
        while (true) 
        {
            speedmove += 0.5f;
            yield return new WaitForSeconds(1f);
        }
    }

    private void JampMetod() 
    {
        if (onGround)
        {
            if (SceneManager.GetActiveScene().name == "Training")
                _tutorialManager.OnEvent(TutorialEvent.PlayerJamp);
            animator.SetTrigger("Jamp");
            rb?.AddForce(Vector3.up * forceJamp, ForceMode.VelocityChange);
            animator.SetTrigger("Run");
        }
    }

    private void MoveMetod() 
    {
        Vector3 move = Vector3.right * speedmove * Time.deltaTime;

#if UNITY_STANDALONE

        var direction = _newInput.Move.SlideMove.ReadValue<Vector2>();
        transform.Translate(move * direction);
        if (SceneManager.GetActiveScene().name == "Training")
        {
            if (direction.x < 0) _tutorialManager.OnEvent(TutorialEvent.PlayerMoveLeft);
            if (direction.x > 0) _tutorialManager.OnEvent(TutorialEvent.PlayerMoveRight);
        }       

#endif

#if UNITY_EDITOR

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (SceneManager.GetActiveScene().name == "Training")
                _tutorialManager.OnEvent(TutorialEvent.PlayerMoveRight);
            transform.Translate(move);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (SceneManager.GetActiveScene().name == "Training")
                _tutorialManager.OnEvent(TutorialEvent.PlayerMoveLeft);
            transform.Translate(-move);
        } 
#endif

    }
}