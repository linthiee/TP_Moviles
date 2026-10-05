using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MngPts : MonoBehaviour
{
    Rect R = new Rect();
    public Vector2[] DineroPos;
    public Vector2 DineroEsc;
    public GUISkin GS_Dinero;
    public Vector2 GanadorPos;
    public Vector2 GanadorEsc;
    public Texture2D[] Ganadores;
    public GUISkin GS_Ganador;
    public GameObject Fondo;
    
    public float TiempEmpAnims = 2.5f;
    float Tempo = 0;
    public float TiempEspReiniciar = 10;

    public GameObject resultsPanel;
    public GameObject creditsPanel;
    public float timeForCredits = 6f;
    private bool showingCredits = false;
    private float timerCredits = 0f;

    public float TiempParpadeo = 0.7f;
    float TempoParpadeo = 0;
    bool PrimerImaParp = true;
    public bool ActivadoAnims = false;

    public float tiempoDeConteo = 2.0f; 
    private float timerConteo = 0f;
    private float puntajeAnimadoGanador = 0f;
    private float puntajeAnimadoPerdedor = 0f;

    public TextMeshProUGUI textMoneyLeft;
    public TextMeshProUGUI textMoneyRight;
    public TextMeshProUGUI textWinnerTitle;
    public Image winningImg;
    public Sprite[] winningSprites;

    Visualizacion Viz = new Visualizacion();

    //---------------------------------//

    void Start()
    {
        SetVisualWinner();

        if (creditsPanel != null)
            creditsPanel.SetActive(false);
        if (resultsPanel != null)
            resultsPanel.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
            SceneManager.LoadScene(0);

        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Keypad0))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.Backspace))
            SceneManager.LoadScene(3);

        TiempEspReiniciar -= Time.deltaTime;
        if (TiempEspReiniciar <= 0)
            SceneManager.LoadScene(0);

        if (ActivadoAnims && !showingCredits)
        {
            timerCredits += Time.deltaTime;
            if (timerCredits >= timeForCredits)
            {
                ActivateCredits();
            }
        }

        if (timerConteo < tiempoDeConteo)
        {
            timerConteo += Time.deltaTime;
            puntajeAnimadoGanador = Mathf.Lerp(0, DatosPartida.PtsGanador, timerConteo / tiempoDeConteo);
            puntajeAnimadoPerdedor = Mathf.Lerp(0, DatosPartida.PtsPerdedor, timerConteo / tiempoDeConteo);
        }
        else
        {
            puntajeAnimadoGanador = DatosPartida.PtsGanador;
            puntajeAnimadoPerdedor = DatosPartida.PtsPerdedor;
        }

        if (ActivadoAnims)
        {
            TempoParpadeo += Time.deltaTime;

            if (TempoParpadeo >= TiempParpadeo)
            {
                TempoParpadeo = 0;

                if (PrimerImaParp)
                    PrimerImaParp = false;
                else
                {
                    TempoParpadeo += 0.1f;
                    PrimerImaParp = true;
                }
            }
        }
        else
        {
            Tempo += Time.deltaTime;
            if (Tempo >= TiempEmpAnims)
            {
                Tempo = 0;
                ActivadoAnims = true;
            }
        }

        UpdateUI();
    }

    void SetVisualWinner()
    {
        if (winningImg != null && winningSprites.Length >= 2)
        {
            switch (DatosPartida.LadoGanadaor)
            {
                case DatosPartida.Lados.Der:
                    winningImg.sprite = winningSprites[1];
                    break;
                case DatosPartida.Lados.Izq:
                    winningImg.sprite = winningSprites[0];
                    break;
            }
        }

        if (textWinnerTitle != null)
        {
            if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq)
                textWinnerTitle.text = "PLAYER 1 WINS";
            else
                textWinnerTitle.text = "PLAYER 2 WINS";
        }
    }

    private void UpdateUI()
    {
        int ptsGanadorActual = Mathf.RoundToInt(puntajeAnimadoGanador);
        int ptsPerdedorActual = Mathf.RoundToInt(puntajeAnimadoPerdedor);

        bool mostrarGanador = !ActivadoAnims || PrimerImaParp;

        if (textMoneyLeft != null)
        {
            if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq)
            {
                textMoneyLeft.text = "$" + Viz.PrepararNumeros(ptsGanadorActual);
                textMoneyLeft.gameObject.SetActive(mostrarGanador);
            }
            else
            {
                textMoneyLeft.text = "$" + Viz.PrepararNumeros(ptsPerdedorActual);
                textMoneyLeft.gameObject.SetActive(true);
            }
        }

        if (textMoneyRight != null)
        {
            if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Der)
            {
                textMoneyRight.text = "$" + Viz.PrepararNumeros(ptsGanadorActual);
                textMoneyRight.gameObject.SetActive(mostrarGanador);
            }
            else
            {
                textMoneyRight.text = "$" + Viz.PrepararNumeros(ptsPerdedorActual);
                textMoneyRight.gameObject.SetActive(true);
            }
        }
    }

    private void ActivateCredits()
    {
        showingCredits = true;
        if (resultsPanel != null)
            resultsPanel.SetActive(false);
        if (creditsPanel != null)
            creditsPanel.SetActive(true);
    }

    public void DesaparecerGUI()
    {
        ActivadoAnims = false;
        Tempo = -100;
    }
}