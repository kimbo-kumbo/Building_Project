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
            Debug.LogError($"���� ������������� ������ � {name}");
        else Debug.Log($"������ � {name} ���������� ");
    }

    private void OnEnable()
    {
        _restart.onClick.AddListener(delegate { LoadScene(SceneExample.NewGame); });
        _exit.onClick.AddListener(delegate { LoadScene(SceneExample.Exit); });
    }
    private void OnDisable()
    {
        _restart.onClick.RemoveListener(delegate { LoadScene(SceneExample.NewGame); });
        _exit.onClick.RemoveListener(delegate { LoadScene(SceneExample.Exit); });
    }
}