using UnityEngine;
using UnityEngine.UI;

public class GameOver_Controller : SceneLoad
{
    public GameOverMenu gameOver;
    [SerializeField] Button _restart;
    [SerializeField] Button _exit;

    private void OnValidate()
    {
        if (gameOver == null || _restart == null || _exit == null)        
            Debug.LogError($"Есть незаполненные ссылки в {name}");
        else Debug.Log($"Ссылки в {name} заполненны ");
    }

    private void OnEnable()
    {
        _restart.onClick.AddListener(()=> LoadScene(SceneExample.Restart));
        _exit.onClick.AddListener(() => LoadScene(SceneExample.Exit));
    }
    private void OnDisable()
    {
        _restart.onClick.RemoveListener(() => LoadScene(SceneExample.Restart));
        _exit.onClick.RemoveListener(() => LoadScene(SceneExample.Exit));
    }
}