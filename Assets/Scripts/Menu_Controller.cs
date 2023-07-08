using UnityEngine;
using UnityEngine.UI;

public class Menu_Controller : SceneLoad
{
    [SerializeField] private Button _start;
    [SerializeField] private Button _exit;
    private void OnEnable()
    {
        _start?.onClick.AddListener(delegate { LoadScene(SceneExample.NewGame);} );
        _exit?.onClick.AddListener(delegate { LoadScene(SceneExample.Exit);} );
    }
    private void OnDisable()
    {
        _start?.onClick.RemoveListener(delegate { LoadScene(SceneExample.NewGame); });
        _exit?.onClick.RemoveListener(delegate { LoadScene(SceneExample.Exit); });
    }

    private void Start()
    {
        LoggingFile loggingFile = new LoggingFile();
        loggingFile.CreateTextFile();
    }
}