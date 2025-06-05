using UnityEngine; // Importa o namespace UnityEngine, necessário para todas as classes Unity

// Define a classe Plataforma que herda de MonoBehaviour
public class Plataforma : MonoBehaviour
{

	Transform TransformPlataforma; // Referência ao componente Transform da plataforma
	Vector2 PosicaoInicial; // Armazena a posição inicial da plataforma
	float TempoDecorrido; // Acumula o tempo passado para controle do movimento

	// Variáveis públicas que podem ser ajustadas no Inspector
	public float Distancia; // Distância máxima do movimento
	public float VelocidadeMovimento; // Velocidade do movimento
	public Vector2 Direcao; // Direção do movimento (vetor normalizado ou não):
	// - (1, 0) → Movimento horizontal para direita
	// - (-1, 0) → Movimento horizontal para esquerda
	// - (0, 1) → Movimento vertical para cima
	// - (0, -1) → Movimento vertical para baixo
	// - (1, 1) → Movimento diagonal superior direito (45°)
	// - (-1, 1) → Movimento diagonal superior esquerdo (135°)
	// - (-1, -1) → Movimento diagonal inferior esquerdo (225°)
	// - (1, -1) → Movimento diagonal inferior direito (315°)
	// - (0.5f, 1) → Movimento em ângulo de ~63.4°
	// - (1, 0.5f) → Movimento em ângulo de ~26.6°
	// Qualquer combinação de valores x e y para direções personalizadas
	// Método chamado quando o objeto é inicializado

	void Start()
	{
		// Obtém o componente Transform do objeto atual
		TransformPlataforma = GetComponent<Transform>();
		// Armazena a posição inicial do objeto
		PosicaoInicial = TransformPlataforma.position;
		// Inicializa o tempo decorrido como zero
		TempoDecorrido = 0f;
	}

	// Método chamado a cada frame
	void Update()
	{
		mover(); // Chama a função de movimento a cada frame
	}

	// Função que controla o movimento da plataforma
	void mover()
	{
		// Incrementa o tempo decorrido multiplicado pela velocidade
		TempoDecorrido += Time.deltaTime * VelocidadeMovimento;
		// Calcula um valor que vai e volta entre 0 e Distancia (movimento de vai-e-vem)
		float movimento = Mathf.PingPong(TempoDecorrido, Distancia);

		// Atualiza a posição do objeto: posição inicial + direção * valor do movimento
		transform.position = PosicaoInicial + Direcao.normalized * movimento;
	}

	// Método chamado quando ocorre uma colisão 2D
	void OnCollisionEnter2D(Collision2D objetoTocado)
	{
		// Recupera a tag do objeto que colidiu
		string tag = objetoTocado.gameObject.tag;
		// Verifica se a tag contém "Player"
		if (tag.Contains("Player") == true)
		{
			// Faz o jogador se tornar filho da plataforma (move-se junto com ela)
			objetoTocado.transform.parent = TransformPlataforma;
		}
	}

	// Método chamado quando uma colisão 2D termina
	void OnCollisionExit2D(Collision2D objetoParouTocar)
	{
		// Recupera a tag do objeto que parou de colidir
		string tag = objetoParouTocar.gameObject.tag;
		// Verifica se a tag contém "Player"
		if (tag.Contains("Player") == true)
		{
			// Remove o parentesco, fazendo o jogador parar de seguir a plataforma
			objetoParouTocar.transform.parent = null;
		}
	}
}