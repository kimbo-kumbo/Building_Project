using UnityEngine;
using UnityEngine.UI;

public class Menu_Controller : SceneLoad
{
    [SerializeField] private Button _start;
    [SerializeField] private Button _training;
    [SerializeField] private Button _exit;
    
    private void OnEnable()
    {
        _start?.onClick.AddListener(() => LoadScene(SceneExample.NewGame));
        _training?.onClick.AddListener(() => LoadScene(SceneExample.Training));
        _exit?.onClick.AddListener(() => LoadScene(SceneExample.Exit));
    }
    private void OnDisable()
    {
        _start?.onClick.RemoveListener(() => LoadScene(SceneExample.NewGame));
        _training?.onClick.RemoveListener(() => LoadScene(SceneExample.Training));
        _exit?.onClick.RemoveListener(() => LoadScene(SceneExample.Exit));
    }

    private void Start()
    {
        LoggingFile loggingFile = new LoggingFile();
        loggingFile.CreateTextFile();
    }
}