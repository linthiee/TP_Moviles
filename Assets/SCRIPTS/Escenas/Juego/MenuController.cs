using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuController : MonoBehaviour
{
    [FormerlySerializedAs("nombreEscenaJuego")] public string sceneName = "conduccion9";
    public GameObject menuPanel;
    public GameObject creditsPanel;
    public GameObject difficultyPanel;
    
    public void PlaySingleplayer()
    {
        PlayerPrefs.SetInt("ModoSingleplayer", 1);
        PlayerPrefs.Save();
        
        menuPanel.SetActive(false);
        difficultyPanel.SetActive(true);
    }

    public void PlayMultiplayer()
    {
        PlayerPrefs.SetInt("ModoSingleplayer", 0);
        PlayerPrefs.Save();
        
        menuPanel.SetActive(false);
        difficultyPanel.SetActive(true);
    }
    
    public void SetEasyDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 0); 
        PlayerPrefs.Save();
        
        menuPanel.SetActive(false);
        difficultyPanel.SetActive(false);
        
        LoadingScreen.instance.LoadScene(sceneName);
    }

    public void SetNormalDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
        PlayerPrefs.Save();
        
        menuPanel.SetActive(false);
        difficultyPanel.SetActive(false);

        LoadingScreen.instance.LoadScene(sceneName);
    }

    public void SetHardDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 2); 
        PlayerPrefs.Save();
        
        menuPanel.SetActive(false);
        difficultyPanel.SetActive(false);

        LoadingScreen.instance.LoadScene(sceneName);
    }
    
    public void GoToCredits()
    {
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void GoToMenu()
    {
        menuPanel.SetActive(true);
        creditsPanel.SetActive(false);
        difficultyPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exiting"); 
    }
}
