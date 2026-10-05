using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;
    
    private static string sceneDestiny;
    
    public GameObject container;
    public Slider progressBar;
    public TextMeshProUGUI progressText;

    public CanvasGroup canvasGroup;
    
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
            return;
        }
        
        if (container != null) 
            container.SetActive(false);
    }
    
    IEnumerator LoadSceneAsync(string sceneName)
    {
        container.SetActive(true);
        yield return StartCoroutine(Fade(0f, 1f));

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            
            if (progressBar != null) progressBar.value = progress;
            if (progressText != null) progressText.text = (progress * 100f).ToString("0") + "%";

            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f); 
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

        yield return StartCoroutine(Fade(1f, 0f));
        container.SetActive(false);
    }

    private IEnumerator Fade(float start, float end)
    {
        float duration = 0.5f; 
        float time = 0f;

        if (canvasGroup != null)
        {
            while (time < duration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(start, end, time / duration);
                yield return null;
            }
            canvasGroup.alpha = end;
        }
    }
}
