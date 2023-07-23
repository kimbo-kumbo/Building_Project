using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    protected void LoadScene(SceneExample sceneExample)
    {
        Time.timeScale = 1.0f;
        switch (sceneExample)
        {
            case SceneExample.NewGame:
                SceneManager.LoadScene(1);
                break;
            case SceneExample.MainMenu:
                SceneManager.LoadScene(0);
                break;
            case SceneExample.Training:
                SceneManager.LoadScene(2);
                break;
            case SceneExample.Exit:
                Application.Quit();
                LoggingFile loggingFile = new LoggingFile();
                loggingFile.SaveTime(TimeGame.Finish);
                //Debug.Log("Выход из игры");
                break;
            case SceneExample.Restart:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }
}