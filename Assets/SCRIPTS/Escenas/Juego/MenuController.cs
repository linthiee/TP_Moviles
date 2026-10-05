using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuController : MonoBehaviour
{
    [FormerlySerializedAs("nombreEscenaJuego")] public string sceneName = "conduccion9";
    public GameObject menuPanel;
    public GameObject creditsPanel;
    public GameObject difficultyPanel;
    
    private IState _state;

    private void Start()
    {
        menuPanel.SetActive(false);
        difficultyPanel.SetActive(false);
        creditsPanel.SetActive(false);

        ChangeState(new MenuState());
    }
    
    public void ChangeState(IState newState)
    {
        if (_state != null)
        {
            _state.Exit(this);
        }

        _state = newState;
        _state.Enter(this);
    }
    
    public void PlaySingleplayer()
    {
        PlayerPrefs.SetInt("ModoSingleplayer", 1);
        PlayerPrefs.Save();
        
        ChangeState(new DifficultyState());
    }

    public void PlayMultiplayer()
    {
        PlayerPrefs.SetInt("ModoSingleplayer", 0);
        PlayerPrefs.Save();
        
        ChangeState(new DifficultyState());
    }
    
    public void SetEasyDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 0); 
        PlayerPrefs.Save();
        
        if (_state != null) 
            _state.Exit(this);
        
        LoadingScreen.instance.LoadScene(sceneName);
    }

    public void SetNormalDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
        PlayerPrefs.Save();

        if (_state != null)
            _state.Exit(this);

        LoadingScreen.instance.LoadScene(sceneName);
    }

    public void SetHardDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 2); 
        PlayerPrefs.Save();

        if (_state != null) 
            _state.Exit(this);

        LoadingScreen.instance.LoadScene(sceneName);
    }
    
    public void GoToCredits()
    {
        ChangeState(new CreditsState());
    }

    public void GoToMenu()
    {
        ChangeState(new MenuState());
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exiting"); 
    }
}
