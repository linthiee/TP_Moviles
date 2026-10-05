using UnityEngine;
using System.Collections;

public class Bolsa : MonoBehaviour
{
	public Pallet.Valores Monto;
	//public int IdPlayer = 0;
	public string TagPlayer = "";
	public Texture2D ImagenInventario;
	Player Pj = null;
	
	bool Desapareciendo;
	public GameObject Particulas;
	public float TiempParts = 2.5f;

	private float easyRadius = 0.2f;   
	private float normalRadius = 0.4f; 
	private float hardRadius = 0.1f; 
	
	// Use this for initialization
	void Start () 
	{
		Monto = Pallet.Valores.Valor2;
		
		if(Particulas != null)
			Particulas.SetActive(false);

		AdjustCollisionRadius();
	}

	void AdjustCollisionRadius()
	{
		SphereCollider collider = GetComponent<SphereCollider>();
        
		if (collider != null)
		{
			int dificultad = PlayerPrefs.GetInt("Difficulty", 0);
            
			if (dificultad == 0) 
				collider.radius = easyRadius;
			else if (dificultad == 1)
				collider.radius = normalRadius;
			else if (dificultad == 2) 
				collider.radius = hardRadius;
		}
		else
		{
			Debug.LogWarning("bag doesnt have a sphere collider");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if(Desapareciendo)
		{
			TiempParts -= Time.deltaTime;
			if(TiempParts <= 0)
			{
				GetComponent<Renderer>().enabled = true;
				GetComponent<Collider>().enabled = true;
				
				Particulas.GetComponent<ParticleSystem>().Stop();
				gameObject.SetActive(false);
			}
		}
		
	}
	
	void OnTriggerEnter(Collider coll)
	{
		if(coll.tag == TagPlayer)
		{
			Pj = coll.GetComponent<Player>();
			//if(IdPlayer == Pj.IdPlayer)
			//{
				if(Pj.AgregarBolsa(this))
					Desaparecer();
			//}
		}
	}
	
	public void Desaparecer()
	{
		Particulas.GetComponent<ParticleSystem>().Play();
		Desapareciendo = true;
		
		GetComponent<Renderer>().enabled = false;
		GetComponent<Collider>().enabled = false;
		
		if(Particulas != null)
		{
			Particulas.GetComponent<ParticleSystem>().Play();
		}
	
	}
}
