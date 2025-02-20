using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe que controla o movimento do personagem
public class Temp : MonoBehaviour {

	bool ApertouBotaoPular; //variavel para saber se foi pressionado ou não o botão
	float VelocidadePulo; // armazena a velocidade do pulo
	Collider2D Colisor2dPersonagem; // referencia para o colisor do personagem
	bool EstaTocandoColisor; // verificar se esta tocando um colisor

	// Variáveis para controlar a velocidade do personagem
	float VelocidadeX; // Velocidade no eixo X (horizontal)
	float VelocidadeY; // Velocidade no eixo Y (vertical)
	float VelocidadeHorizontalMaxima; // Velocidade máxima que o personagem pode atingir no eixo X
	float DirecaoHorizontal; // Direção do movimento horizontal (-1 para esquerda, 1 para direita, 0 para parado)

	float VelocidadeVerticalMaxima; // Velocidade máxima que o personagem pode atingir no eixo Y
	float DirecaoVertical; // Direção do movimento vertical (-1 para baixo, 1 para cima, 0 para parado)

	Vector2 VetorVelocidadePersonagem; // Vetor que armazena a velocidade do personagem em X e Y

	Rigidbody2D CorpoRigidoPersonagem; 	// Referência ao componente Rigidbody2D do personagem
	 
	// Método chamado uma vez no início do jogo
	void Start () {

		ApertouBotaoPular = false;
		VelocidadePulo = 5f;

		Colisor2dPersonagem = GetComponent<Collider2D> ();

		EstaTocandoColisor = false;

		CorpoRigidoPersonagem = GetComponent<Rigidbody2D> (); // Obtém o componente Rigidbody2D do objeto ao qual este script está anexado

		CorpoRigidoPersonagem.gravityScale = 1f; // Define a escala da gravidade para o Rigidbody2D (0 significa que não há gravidade)

		CorpoRigidoPersonagem.freezeRotation = true; // Congela a rotação do Rigidbody2D para evitar que o personagem gire ao colidir com algo

		// Inicializa as variáveis de velocidade
		VelocidadeX = 0f; // Começa parado no eixo X
		VelocidadeY = 0f; // Começa parado no eixo Y

		VelocidadeHorizontalMaxima = 5f; // Define a velocidade máxima no eixo X
		DirecaoHorizontal = 0f; // Começa sem direção definida

		VelocidadeVerticalMaxima = 5f; // Define a velocidade máxima no eixo Y
		DirecaoVertical = 0f; // Começa sem direção definida

		// Cria um vetor de velocidade inicial para o personagem
		VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);
	}

	// Método chamado uma vez por frame (quadro)
	void Update () {

		// Chama o método para controlar o movimento horizontal do personagem
		MovimentoHorizontal ();

		MovimentoPuloUnico ();
		//MovimentoPulo ();


		// Chama o método para controlar o movimento vertical do personagem
		//MovimentoVertical ();
	}


	void MovimentoPulo()
	{
		// Verifica se o botão de pular (Jump) foi pressionado
		ApertouBotaoPular = Input.GetButtonDown("Jump");

		// Se o botão de pular foi pressionado
		if (ApertouBotaoPular == true)
		{
			// Mantém a velocidade atual no eixo X
			VelocidadeX = CorpoRigidoPersonagem.velocity.x;
			// Define a velocidade no eixo Y para o valor do pulo
			VelocidadeY = VelocidadePulo;
			// Cria um novo vetor de velocidade com os valores de X e Y
			VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);
			// Aplica o vetor de velocidade ao Rigidbody2D do personagem
			CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
		}
	}

	void MovimentoPuloUnico()
	{
		// Verifica se o botão de pular (Jump) foi pressionado
		ApertouBotaoPular = Input.GetButtonDown("Jump");
		// Verifica se o personagem está tocando o chão (ou outra camada configurada)
		EstaTocandoColisor = Colisor2dPersonagem.IsTouchingLayers();

		// Se o botão de pular foi pressionado
		if (ApertouBotaoPular == true)
		{
			// Se o personagem estiver tocando o chão
			if (EstaTocandoColisor == true)
			{
				// Mantém a velocidade atual no eixo X
				VelocidadeX = CorpoRigidoPersonagem.velocity.x;
				// Define a velocidade no eixo Y para o valor do pulo
				VelocidadeY = VelocidadePulo;
				// Cria um novo vetor de velocidade com os valores de X e Y
				VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);
				// Aplica o vetor de velocidade ao Rigidbody2D do personagem
				//CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;

				CorpoRigidoPersonagem.AddForce(new Vector2(VelocidadeX, VelocidadePulo), ForceMode2D.Impulse);

			}
		}
	}




	// Método para controlar o movimento vertical do personagem
	void MovimentoVertical(){

		// Obtém a direção vertical a partir do input do jogador (teclado ou controle)
		// Input.GetAxis("Vertical") retorna um valor entre -1 e 1:
		// -1 para baixo, 1 para cima, 0 se nenhuma tecla estiver sendo pressionada
		DirecaoVertical = Input.GetAxis ("Vertical");

		// Mantém a velocidade atual no eixo X (para não interferir no movimento horizontal)
		VelocidadeX = CorpoRigidoPersonagem.velocity.x;

		// Calcula a velocidade no eixo Y multiplicando a direção pela velocidade máxima
		VelocidadeY = VelocidadeVerticalMaxima * DirecaoVertical;

		// Cria um novo vetor de velocidade com os valores calculados
		VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

		// Aplica o vetor de velocidade ao Rigidbody2D para mover o personagem
		CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
	}

	// Método para controlar o movimento horizontal do personagem
	void MovimentoHorizontal(){

		// Obtém a direção horizontal a partir do input do jogador (teclado ou controle)
		// Input.GetAxis("Horizontal") retorna um valor entre -1 e 1:
		// -1 para esquerda, 1 para direita, 0 se nenhuma tecla estiver sendo pressionada
		DirecaoHorizontal = Input.GetAxis ("Horizontal");

		// Calcula a velocidade no eixo X multiplicando a direção pela velocidade máxima
		VelocidadeX = VelocidadeHorizontalMaxima * DirecaoHorizontal;

		// Mantém a velocidade atual no eixo Y (para não interferir no movimento vertical)
		VelocidadeY = CorpoRigidoPersonagem.velocity.y;

		// Cria um novo vetor de velocidade com os valores calculados
		VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

		// Aplica o vetor de velocidade ao Rigidbody2D para mover o personagem
		CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
	}

	void OnCollisionEnter2D(Collision2D objetoTocado){
		//é executado sempre que um colisor toca em outro colisor
		//a variável objetoTocado possui dados do objeto tocado.

		string tagObjetoTocado = objetoTocado.gameObject.tag;
		if (tagObjetoTocado == "Diamante01") {

		}
			
	}

	void OnCollisionStay2D(Collision2D objetoTocando){
		//é executado sempre que um colisor toca em outro colisor
		//a variável objetoTocado possui dados do objeto tocado.

		string tagObjetoTocado = objetoTocando.gameObject.tag;
		if (tagObjetoTocado == "Diamante01") {

		}

	}

	void OnCollisionExit2D(Collision2D objetoParouTocar){
		//é executado sempre que um colisor toca em outro colisor
		//a variável objetoTocado possui dados do objeto tocado.

		string tagObjetoTocado = objetoParouTocar.gameObject.tag;
		if (tagObjetoTocado == "Diamante01") {

		}

	}
}