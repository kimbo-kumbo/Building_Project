using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceControl : Move 
{
    [SerializeField] Text textTime; 
    [SerializeField] Text textHealt; 
    [SerializeField] Text textCoins;
    [SerializeField] GameOver_Controller _gameOver_Controller;
    [SerializeField] int countTime = 60;

    void Start()
    {
#if UNITY_EDITOR
        if (textTime == null || textHealt == null || textCoins == null || _gameOver_Controller == null)
            Debug.LogError($"Есть незаполненные ссылки в {name}");
        else Debug.Log("Все ссылки заполненны");
#endif
        textHealt.text = healt.ToString(); 
        StartCoroutine(TimeCoroutine());        
    }
    private void OnEnable()
    {
        Move.CoinsEvent += CoinsPlus; 
        Move.HealtEvent += HealtMinus;
    }
    private void OnDisable()
    {
        Move.CoinsEvent -= CoinsPlus; 
        Move.HealtEvent -= HealtMinus; 
    }
    private void CoinsPlus() 
    {
        countCoins++;
        textCoins.text = countCoins.ToString();
    }
    private void HealtMinus()
    {
        healt--;
        textHealt.text = healt.ToString();
    }   
    IEnumerator TimeCoroutine()
    {
        while (Time.timeScale == 1) 
        {
            countTime--;
            textTime.text = countTime.ToString();
            yield return new WaitForSeconds(1f);
            if (_transform.position.y <= -3f || healt <= 0 || countTime <= 0)
            {
                _gameOver_Controller.gameOver.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
