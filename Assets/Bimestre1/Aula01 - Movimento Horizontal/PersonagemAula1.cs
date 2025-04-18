﻿using UnityEngine;

// Classe que controla o movimento do personagem
public class PersonagemAula1 : MonoBehaviour {

	// Variáveis para controlar a velocidade do personagem
	float VelocidadeX; // Velocidade no eixo X (horizontal)
	float VelocidadeY; // Velocidade no eixo Y (vertical)
	float VelocidadeHorizontalMaxima; // Velocidade máxima que o personagem pode atingir no eixo X
	float DirecaoHorizontal; // Direção do movimento horizontal (-1 para esquerda, 1 para direita, 0 para parado)
	Vector2 VetorVelocidadePersonagem; // Vetor que armazena a velocidade do personagem em X e Y

	// Referência ao componente Rigidbody2D do personagem
	Rigidbody2D CorpoRigidoPersonagem;

	// Método chamado uma vez no início do jogo
	void Start () {

		// Obtém o componente Rigidbody2D do objeto ao qual este script está anexado
		CorpoRigidoPersonagem = GetComponent<Rigidbody2D> ();

		// Define a escala da gravidade para o Rigidbody2D (1 é o valor padrão)
		CorpoRigidoPersonagem.gravityScale = 1f;

		// Congela a rotação do Rigidbody2D para evitar que o personagem gire ao colidir com algo
		CorpoRigidoPersonagem.freezeRotation = true;

		// Inicializa as variáveis de velocidade
		VelocidadeX = 0f; // Começa parado no eixo X
		VelocidadeY = 0f; // Começa parado no eixo Y
		VelocidadeHorizontalMaxima = 2.5f; // Define a velocidade máxima no eixo X
		DirecaoHorizontal = 0f; // Começa sem direção definida

		// Cria um vetor de velocidade inicial para o personagem
		VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

		// Aplica o vetor de velocidade ao Rigidbody2D para mover o personagem
		CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
	}

	// Método chamado uma vez por frame (quadro)
	void Update () {

		// Chama o método para controlar o movimento horizontal do personagem
		MovimentoHorizontal ();
	}

	// Método para controlar o movimento horizontal do personagem
	void MovimentoHorizontal(){

		// Obtém a direção horizontal a partir do input do jogador (teclado ou controle)
		// Input.GetAxis("Horizontal") retorna um valor entre -1 e 1:
		// -1 para esquerda, 1 para direita, 0 se nenhuma tecla estiver sendo pressionada
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


/*
Explicação Detalhada:
Variáveis:

	VelocidadeX e VelocidadeY: Controlam a velocidade do personagem nos eixos X e Y.

	VelocidadeHorizontalMaxima: Define a velocidade máxima que o personagem pode atingir no eixo X.

	DirecaoHorizontal: Armazena a direção do movimento horizontal (-1 para esquerda, 1 para direita, 0 para parado).

	VetorVelocidadePersonagem: Um vetor 2D que combina as velocidades X e Y para mover o personagem.

	Rigidbody2D:

	CorpoRigidoPersonagem: Referência ao componente Rigidbody2D, que é responsável por simular a física do personagem.

	gravityScale: Controla a intensidade da gravidade aplicada ao personagem.

	freezeRotation: Impede que o personagem gire ao colidir com outros objetos.

Método Start:

	Inicializa as variáveis e configura o Rigidbody2D no início do jogo.

Método Update:

	Chamado uma vez por frame. Aqui, o método MovimentoHorizontal é chamado para atualizar o movimento do personagem a cada quadro.

Método MovimentoHorizontal:

	Obtém a direção horizontal do jogador usando Input.GetAxis("Horizontal").

	Calcula a velocidade no eixo X multiplicando a direção pela velocidade máxima.

	Mantém a velocidade atual no eixo Y para não interferir na gravidade ou em possíveis pulos.

	Cria um novo vetor de velocidade e aplica ao Rigidbody2D para mover o personagem.

	Dicas para Iniciantes:
	Input.GetAxis("Horizontal"): Retorna um valor entre -1 e 1 dependendo da tecla pressionada. Isso permite um movimento suave, especialmente útil para controles analógicos.

	Rigidbody2D.velocity: Define a velocidade do objeto diretamente. Isso é útil para movimentos baseados em física.

	GravityScale: Ajuste este valor para mudar como a gravidade afeta o personagem. Um valor maior faz o personagem cair mais rápido.

*/