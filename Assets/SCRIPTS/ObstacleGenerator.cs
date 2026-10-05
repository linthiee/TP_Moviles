using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public Transform Player; 
    public float cdTime = 2f; 
    public float fwdDistance = 80f;
    public float roadVariation = 5f; 

    public float easyTime = 3f;
    public float normalTime = 2f;
    public float hardTime = 1f;
    
    private float timer = 0f;
    private int quantPerWave = 1;
    
    public float distanceFromGround = 0f;
    
    void Start()
    {
        int dificultad = PlayerPrefs.GetInt("Difficulty", 0);

        switch (dificultad)
        {
            case 0: 
                cdTime = easyTime;
                break;
            case 1: 
                cdTime = normalTime;
                break;
            case 2: 
                cdTime = hardTime;
                break;
            default:
                cdTime = normalTime;
                break;
        }
    }
    
    void Update()
    {
        if (GameManager.Instancia != null && GameManager.Instancia.EstAct == GameManager.EstadoJuego.Jugando)
        {
            timer += Time.deltaTime;

            if (timer >= cdTime)
            {
                for (int i = 0; i < quantPerWave; i++)
                {
                    GenerateObstacle();
                }

                timer = 0f;
            }
        }
    }

    void GenerateObstacle()
    {
        GameObject newObstacle = ObjectPool.instance.AskForObstacle();

        if (newObstacle != null)
        {
            Vector3 futurePos = Player.position + (Player.forward * fwdDistance);
            futurePos += Player.right * Random.Range(-roadVariation, roadVariation);

            futurePos.y = Player.position.y + 20f;

            RaycastHit impact;
            if (Physics.Raycast(futurePos, Vector3.down, out impact, 50f))
            {
                newObstacle.transform.position = impact.point;
                newObstacle.transform.rotation = Player.rotation;
                
                Rigidbody rb = newObstacle.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
            else
            {
                newObstacle.SetActive(false);
            }
        }
    }
}
