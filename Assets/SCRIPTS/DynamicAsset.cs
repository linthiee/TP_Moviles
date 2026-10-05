using System.Collections;
using UnityEngine;

public class DynamicAset : MonoBehaviour
{
    public string prefabName = "ParedCajas"; 
    
    public float lifetime = 25f; 

    private GameObject memoryPrefab;
    private GameObject sceneInstance;
    
    void Start()
    {
        StartCoroutine(LoadAndUnload());
    }

    IEnumerator LoadAndUnload()
    {
        memoryPrefab = Resources.Load<GameObject>(prefabName);

        if (memoryPrefab != null)
        {
            sceneInstance = Instantiate(memoryPrefab, transform.position, transform.rotation);
            Debug.Log($"asset '{prefabName}' loaded and instantiated");
        }
        else
        {
            Debug.LogError($"couldnt find '{prefabName}' in Resources");
            yield break;
        }

        yield return new WaitForSeconds(lifetime);

        if (sceneInstance != null)
        {
            Destroy(sceneInstance);
        }

        memoryPrefab = null;

        Resources.UnloadUnusedAssets();
        
        Debug.Log($"asset '{prefabName}' destroyed");
    }
}
