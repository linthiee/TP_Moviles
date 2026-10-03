using UnityEngine;

public class ControlDireccion : MonoBehaviour
{
    public enum TipoInput
    {
        FloatingJoystick,
        Mouse,
        Kinect,
        AWSD,
        Arrows
    }

    public TipoInput InputAct = ControlDireccion.TipoInput.FloatingJoystick;

    public Transform ManoDer;
    public Transform ManoIzq;

    public float MaxAng = 90;
    public float DesSencibilidad = 90;

    public FloatingJoystick movement;

    float Giro = 0;

    public enum Sentido
    {
        Der,
        Izq
    }

    Sentido DirAct;

    public bool Habilitado = true;
    //float Diferencia;

    //---------------------------------------------------------//

    // Use this for initialization
    void Start()
    {
        if (movement != null)
        {
            if (InputAct != TipoInput.FloatingJoystick)
            {
                movement.gameObject.SetActive(false);
                Debug.Log("no estoy con joystick");
            }
            else
            {
                movement.gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (InputAct)
        {
            case TipoInput.Mouse:
                if (Habilitado)
                    gameObject.GetComponent<CarController>()
                        .SetGiro(MousePos.Relation(MousePos.AxisRelation.Horizontal));

                break;

            case TipoInput.Kinect:

                //print("Angulo: "+Angulo());
                /*
                if(ManoIzq.position.y > ManoDer.position.y)
                {
                    DirAct = Sentido.Der;
                    Diferencia = ManoIzq.position.y - ManoDer.position.y;
                }
                else
                {
                    DirAct = Sentido.Izq;
                    Diferencia = ManoDer.position.y - ManoIzq.position.y;
                }
                */

                if (ManoIzq.position.y > ManoDer.position.y)
                {
                    DirAct = Sentido.Der;
                }
                else
                {
                    DirAct = Sentido.Izq;
                }

                switch (DirAct)
                {
                    case Sentido.Der:
                        if (Angulo() <= MaxAng)
                            Giro = Angulo() / (MaxAng + DesSencibilidad);
                        else
                            Giro = 1;

                        if (Habilitado)
                            gameObject.GetComponent<CarController>().SetGiro(Giro);

                        break;

                    case Sentido.Izq:
                        if (Angulo() <= MaxAng)
                            Giro = (Angulo() / (MaxAng + DesSencibilidad)) * (-1);
                        else
                            Giro = (-1);

                        if (Habilitado)
                            gameObject.GetComponent<CarController>().SetGiro(Giro);

                        break;
                }

                break;
            case TipoInput.AWSD:
                if (Habilitado)
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        gameObject.GetComponent<CarController>().SetGiro(-1);
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        gameObject.GetComponent<CarController>().SetGiro(1);
                    }
                }

                break;
            case TipoInput.Arrows:
                if (Habilitado)
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        gameObject.GetComponent<CarController>().SetGiro(-1);
                    }

                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        gameObject.GetComponent<CarController>().SetGiro(1);
                    }
                }

                break;
            case TipoInput.FloatingJoystick:
                if (Habilitado && movement != null)
                {
                    gameObject.GetComponent<CarController>().SetGiro(movement.Horizontal);
                }

                break;
        }
    }

    public float GetGiro()
    {
        /*
        switch(DirAct)
            {
            case Sentido.Der:
                if(Angulo() <= MaxAng)
                    return Angulo() / MaxAng;
                else
                    return 1;
                break;

            case Sentido.Izq:
                if(Angulo() <= MaxAng)
                    return (Angulo() / MaxAng) * (-1);
                else
                    return (-1);
                break;
            }
        */

        return Giro;
    }

    float Angulo()
    {
        Vector2 diferencia = new Vector2(ManoDer.localPosition.x, ManoDer.localPosition.y)
                             - new Vector2(ManoIzq.localPosition.x, ManoIzq.localPosition.y);

        return Vector2.Angle(diferencia, new Vector2(1, 0));
    }
}