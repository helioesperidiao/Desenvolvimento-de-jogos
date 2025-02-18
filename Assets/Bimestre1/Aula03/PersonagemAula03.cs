﻿using UnityEngine;

// Classe que controla o movimento do personagem
public class PersonagemAula03 : MonoBehaviour {

	bool ApertouBotaoPular; // para saber se o botão de pular foi apertado
	float VelocidadePuloSimples; // varivael que guarda a velocidade de pulo do personagem
	Collider2D Colisor2dPersonagem; 
	bool EstaTocandoAlgumColisor;

	// Variáveis para controlar a velocidade do personagem
	float VelocidadeX; // Velocidade no eixo X (horizontal)
	float VelocidadeY; // Velocidade no eixo Y (vertical)

	float VelocidadeHorizontalMaxima; // Velocidade máxima que o personagem pode atingir no eixo X
	float DirecaoHorizontal; // Direção do movimento horizontal (-1 para esquerda, 1 para direita, 0 para parado)

	float VelocidadeVerticalMaxima;
	float DirecaoVertical;

	Vector2 VetorVelocidadePersonagem; // Vetor que armazena a velocidade do personagem em X e Y
	// Referência ao componente Rigidbody2D do personagem
	Rigidbody2D CorpoRigidoPersonagem;

	// Método chamado uma vez no início do jogo
	void Start () {

		ApertouBotaoPular = false; // inicia a variavel com false;
		EstaTocandoAlgumColisor = false; // inicializa a variavel como falso;

		VelocidadePuloSimples = 10; // determina o valor da velocidade do Pulo;

		// Obtém o componente Rigidbody2D do objeto ao qual este script está anexado
		CorpoRigidoPersonagem = GetComponent<Rigidbody2D> ();

		Colisor2dPersonagem = GetComponent<Collider2D> ();

		// Define a escala da gravidade para o Rigidbody2D (1 é o valor padrão)
		CorpoRigidoPersonagem.gravityScale = 3.0f;

		// Congela a rotação do Rigidbody2D para evitar que o personagem gire ao colidir com algo
		CorpoRigidoPersonagem.freezeRotation = true;

		// Inicializa as variáveis de velocidade
		VelocidadeX = 0f; // Começa parado no eixo X
		VelocidadeY = 0f; // Começa parado no eixo Y
		VelocidadeHorizontalMaxima = 5.0f; // Define a velocidade máxima no eixo X
		DirecaoHorizontal = 0f; // Começa sem direção definida

		VelocidadeVerticalMaxima = 5.0f; // Define a velocidade maxima no eixo y
		DirecaoVertical = 0f;			 // Define a direção do movimento na vertical

		// Cria um vetor de velocidade inicial para o personagem
		VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

		// Aplica o vetor de velocidade ao Rigidbody2D para mover o personagem
		CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
	}

	// Método chamado uma vez por frame (quadro)
	void Update () {


		MovimentoHorizontal (); // Chama o método para controlar o movimento horizontal do personagem
		MovimentoPuloUnico();
		//MovimentoVertical (); 
		//MovimentoPuloSimples();
	}

	void MovimentoPuloUnico(){

		ApertouBotaoPular = Input.GetButtonDown ("Jump");
		EstaTocandoAlgumColisor = Colisor2dPersonagem.IsTouchingLayers ();

		if (ApertouBotaoPular == true && EstaTocandoAlgumColisor == true) {
			VelocidadeX = CorpoRigidoPersonagem.velocity.x;
			VelocidadeY = VelocidadePuloSimples;
			VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);
			CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
		}
	
	}

	void MovimentoPuloSimples(){

		ApertouBotaoPular = Input.GetButtonDown ("Jump");

		if (ApertouBotaoPular == true) {
			VelocidadeX = CorpoRigidoPersonagem.velocity.x;
			VelocidadeY = VelocidadePuloSimples;
			VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);
			CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
		}
	}

	//Método para controlar o movimento Vertical do personagem.
	void MovimentoVertical(){

		// Obtém a direção Vertical a partir do input do jogador (teclado ou controle)
		// Input.GetAxis("Vertical") retorna um valor entre (-1 e 1):
		// -1 para baixo, 1 para cima, 0 se nenhuma tecla do eixo vertical estiver sendo pressionada
		DirecaoVertical = Input.GetAxis ("Vertical");

		VelocidadeX = CorpoRigidoPersonagem.velocity.x; // recupera a velocidade em x do corpo ridido do personagem.
		VelocidadeY = DirecaoVertical * VelocidadeVerticalMaxima; // calculo a velocidade em y do personagem.

		// cria um novo vetor de velocidade com VelocidadeX e VelocidadeY
		VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY); 

		//adiciona o novo vetor de velocidade a velocidade(velocity) do corpo rididp do personagem.
		CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;

	}

	// Método para controlar o movimento horizontal do personagem
	void MovimentoHorizontal(){

		// Obtém a direção horizontal a partir do input do jogador (teclado ou controle)
		// Input.GetAxis("Horizontal") retorna um valor entre -1 e 1:
		// -1 para esquerda, 1 para direita, 0 se nenhuma tecla do eixo horizontal estiver sendo pressionada
		DirecaoHorizontal = Input.GetAxis ("Horizontal");

		// Calcula a velocidade no eixo X multiplicando a direção pela velocidade máxima
		VelocidadeX = VelocidadeHorizontalMaxima * DirecaoHorizontal;

		// Mantém a velocidade atual no eixo Y (para não interferir na gravidade ou pulo)
		VelocidadeY = CorpoRigidoPersonagem.velocity.y;

		// Cria um novo vetor de velocidade com os valores calculados
		VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

		// Aplica o vetor de velocidade ao Rigidbody2D para mover o personagem
		CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
	}
}
