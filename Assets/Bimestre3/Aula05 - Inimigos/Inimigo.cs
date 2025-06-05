using UnityEngine;

public class Inimigo : MonoBehaviour
{
	SpriteRenderer PersonagemSpriteRenderer; 
	Vector2 PosicaoInicial;
	float TempoDecorrido;
	bool MovendoParaDireita = true;

	public float Distancia;
	public float VelocidadeMovimento;
	public Vector2 Direcao;

	void Start()
	{
		PersonagemSpriteRenderer = GetComponent<SpriteRenderer>();
		PosicaoInicial = transform.position;
		TempoDecorrido = 0f;


	}

	void Update()
	{
		mover();
	}

	void mover()
	{
		float tempoAnterior = TempoDecorrido;
		TempoDecorrido += Time.deltaTime * VelocidadeMovimento;
		float movimento = Mathf.PingPong(TempoDecorrido, Distancia);

		// Detecta mudança de direção no movimento PingPong
		if (Mathf.PingPong (tempoAnterior, Distancia) > Mathf.PingPong (TempoDecorrido, Distancia)) {
			
			PersonagemSpriteRenderer.flipX = true;
		} else {
			PersonagemSpriteRenderer.flipX = false;
		}

		transform.position = PosicaoInicial + Direcao.normalized * movimento;
	}



	void OnCollisionEnter2D(Collision2D objetoTocado)
	{
		if (objetoTocado.gameObject.CompareTag("Player"))
		{
			objetoTocado.transform.SetParent(transform);
		}
	}

	void OnCollisionExit2D(Collision2D objetoParouTocar)
	{
		if (objetoParouTocar.gameObject.CompareTag("Player"))
		{
			objetoParouTocar.transform.SetParent(null);
			objetoParouTocar.transform.localScale = Vector3.one; 
		}
	}
}