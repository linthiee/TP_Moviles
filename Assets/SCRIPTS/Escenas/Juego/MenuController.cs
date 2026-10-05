using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuController : MonoBehaviour
{
    [FormerlySerializedAs("nombreEscenaJuego")] public string sceneName = "conduccion9";
    public GameObject menuPanel;
    public GameObject creditsPanel;
    
    public void PlaySingleplayer()
    {
        PlayerPrefs.SetInt("ModoSingleplayer", 1);
        PlayerPrefs.Save();
        
        SceneManager.LoadScene(sceneName);
    }

    public void PlayMultiplayer()
    {
        PlayerPrefs.SetInt("ModoSingleplayer", 0);
        PlayerPrefs.Save();
        
        SceneManager.LoadScene(sceneName);
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
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exiting"); 
    }
}
