using UnityEngine;
using UnityEngine.UI;

public class PauseMenu_Controller : SceneLoad
{
    [SerializeField] PauseMenu _pausemenu;
    [SerializeField] Button _resume;
    [SerializeField] Button _restart;
    [SerializeField] Button _mainMenu;
    [SerializeField] Button _exit;
    private NewInput _newInput;
    private void Awake()
    {        
        _newInput = new NewInput();
    }
    private void OnValidate()
    {
        if (_pausemenu == null || _resume == null|| _restart == null || _mainMenu == null || _exit == null)        
            Debug.LogError($"Есть незаполненные ссылки в {name}");
        else Debug.Log($"Ссылки в {name} заполненны ");
    }
    private void OnEnable()
    {
        _resume.onClick.AddListener(delegate { ResumeGame(); });
        _restart.onClick.AddListener(delegate { LoadScene(SceneExample.NewGame); });
        _mainMenu.onClick.AddListener(delegate { LoadScene(SceneExample.MainMenu); });
        _exit.onClick.AddListener(delegate { LoadScene(SceneExample.Exit); });
        _newInput.GameControl.Enable();
        _newInput.GameControl.PauseMenu.performed += context => OpenPauseMenu();        
    }
    private void OnDisable()
    {
        _resume.onClick.RemoveListener(delegate { ResumeGame(); });
        _restart.onClick.RemoveListener(delegate { LoadScene(SceneExample.NewGame); });
        _mainMenu.onClick.RemoveListener(delegate { LoadScene(SceneExample.MainMenu); });
        _exit.onClick.RemoveListener(delegate { LoadScene(SceneExample.Exit); });

        _newInput.Disable();
    }    
    private void ResumeGame()
    {
        Time.timeScale = 1f;
        _pausemenu.gameObject.SetActive(false);
    }

    private void OpenPauseMenu()
    {
        _pausemenu.gameObject.SetActive(true);
        Time.timeScale = 0f;        
    }
}