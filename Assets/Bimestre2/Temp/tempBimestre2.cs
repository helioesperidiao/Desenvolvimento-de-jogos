using UnityEngine;
using UnityEngine.SceneManagement;

// Classe que controla o movimento do personagem
public class tempBimestre2 : MonoBehaviour {

	int TotalPulos;

	PhysicsMaterial2D MaterialSemAtrito;

	int ContadorPulos;

	SpriteRenderer Renderer;

	string TagObjetoTocado;

	bool PegouChave01;

	bool ApertouBotaoPular; // para saber se o botão de pular foi apertado
	float VelocidadePuloSimples; // varivael que guarda a velocidade de pulo do personagem
	Collider2D Colisor2dPersonagem;  //variável que armazena o colisor do personagem
	bool EstaTocandoAlgumColisor; //Variável que verificar se o personagem está tocando um colisor

	// Variáveis para controlar a velocidade do personagem
	float VelocidadeX; // Velocidade no eixo X (horizontal)
	float VelocidadeY; // Velocidade no eixo Y (vertical)

	float VelocidadeHorizontalMaxima; // Velocidade máxima que o personagem pode atingir no eixo X
	float DirecaoHorizontal; // Direção do movimento horizontal (-1 para esquerda, 1 para direita, 0 para parado)

	float VelocidadeVerticalMaxima; //Variável que armazena a velocidade Maxima Vertical do personagem
	float DirecaoVertical; // Variável que armazena a direção vertical do personamagem

	Vector2 VetorVelocidadePersonagem; // Vetor que armazena a velocidade do personagem em X e Y
	// Referência ao componente Rigidbody2D do personagem
	Rigidbody2D CorpoRigidoPersonagem;

	// Método chamado uma vez no início do jogo
	void Start () {

		MaterialSemAtrito = new PhysicsMaterial2D();

		TotalPulos = 1;

		ContadorPulos = 0;

		Renderer = GetComponent<SpriteRenderer> ();

		PegouChave01 = false;

		TagObjetoTocado = "";

		ApertouBotaoPular = false; // inicia a variavel com false;
		EstaTocandoAlgumColisor = false; // inicializa a variavel como falso;

		VelocidadePuloSimples = 10.0f; // determina o valor da velocidade do Pulo;

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

		MovimentoHorizontalFlip();
		MovimentoPuloMultiplo ();
		//MovimentoPuloDuplo ();
		//MovimentoHorizontal (); // Chama o método para controlar o movimento horizontal do personagem
		//MovimentoPuloUnico();
		//MovimentoVertical (); 
		//MovimentoPuloSimples();
	}

	void MovimentoPuloDuplo(){

		// Verifica se o botão de pular foi pressionado.
		// "Jump" é o nome do botão configurado no Input Manager do Unity.
		ApertouBotaoPular = Input.GetButtonDown ("Jump");

	

		// Verifica se o botão de pular foi pressionado e se o personagem está tocando algum colisor.
		if (ApertouBotaoPular == true && ContadorPulos < 2) {

			ContadorPulos = ContadorPulos + 1; // ContadorPulos++;

			// Armazena a velocidade atual no eixo X do personagem.
			VelocidadeX = CorpoRigidoPersonagem.velocity.x;

			// Define a velocidade no eixo Y para o valor de VelocidadePuloSimples.
			// VelocidadePuloSimples é provavelmente uma variável que define a força do pulo.
			VelocidadeY = VelocidadePuloSimples;

			// Cria um novo vetor de velocidade com a velocidade X atual e a nova velocidade Y.
			VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

			// Aplica o novo vetor de velocidade ao Rigidbody2D do personagem.
			// Isso faz o personagem pular.
			CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
		}

	}

	void MovimentoPuloMultiplo(){

		// Verifica se o botão de pular foi pressionado.
		// "Jump" é o nome do botão configurado no Input Manager do Unity.
		ApertouBotaoPular = Input.GetButtonDown ("Jump");



		// Verifica se o botão de pular foi pressionado e se o personagem está tocando algum colisor.
		if (ApertouBotaoPular == true && ContadorPulos < TotalPulos) {

			ContadorPulos = ContadorPulos + 1; // ContadorPulos++;

			// Armazena a velocidade atual no eixo X do personagem.
			VelocidadeX = CorpoRigidoPersonagem.velocity.x;

			// Define a velocidade no eixo Y para o valor de VelocidadePuloSimples.
			// VelocidadePuloSimples é provavelmente uma variável que define a força do pulo.
			VelocidadeY = VelocidadePuloSimples;

			// Cria um novo vetor de velocidade com a velocidade X atual e a nova velocidade Y.
			VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

			// Aplica o novo vetor de velocidade ao Rigidbody2D do personagem.
			// Isso faz o personagem pular.
			CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
		}

	}

	// Método para controlar o movimento horizontal do personagem
	void MovimentoHorizontalFlip(){

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

		if (DirecaoHorizontal < 0) {
			Renderer.flipX = true;
		}else if (DirecaoHorizontal > 0) {
			Renderer.flipX = false;
		}

	}

	void MovimentoPuloUnico(){

		// Verifica se o botão de pular foi pressionado.
		// "Jump" é o nome do botão configurado no Input Manager do Unity.
		ApertouBotaoPular = Input.GetButtonDown ("Jump");

		// Verifica se o personagem está tocando algum colisor.
		// Colisor2dPersonagem é uma referência ao Collider2D do personagem.
		// IsTouchingLayers() retorna true se o colisor estiver tocando qualquer camada.
		EstaTocandoAlgumColisor = Colisor2dPersonagem.IsTouchingLayers ();

		// Verifica se o botão de pular foi pressionado e se o personagem está tocando algum colisor.
		if (ApertouBotaoPular == true && EstaTocandoAlgumColisor == true) {

			// Armazena a velocidade atual no eixo X do personagem.
			VelocidadeX = CorpoRigidoPersonagem.velocity.x;

			// Define a velocidade no eixo Y para o valor de VelocidadePuloSimples.
			// VelocidadePuloSimples é provavelmente uma variável que define a força do pulo.
			VelocidadeY = VelocidadePuloSimples;

			// Cria um novo vetor de velocidade com a velocidade X atual e a nova velocidade Y.
			VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

			// Aplica o novo vetor de velocidade ao Rigidbody2D do personagem.
			// Isso faz o personagem pular.
			CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
		}

	}

	void MovimentoPuloSimples(){

		// Verifica se o botão de pular foi pressionado.
		// "Jump" é o nome do botão configurado no Input Manager do Unity.
		ApertouBotaoPular = Input.GetButtonDown ("Jump");

		// Verifica se o botão de pular foi pressionado.
		if (ApertouBotaoPular == true) {

			// Armazena a velocidade atual no eixo X do personagem.
			// CorpoRigidoPersonagem.velocity.x retorna a velocidade horizontal do Rigidbody2D.
			VelocidadeX = CorpoRigidoPersonagem.velocity.x;

			// Define a velocidade no eixo Y para o valor de VelocidadePuloSimples.
			// VelocidadePuloSimples é uma variável que define a força do pulo.
			VelocidadeY = VelocidadePuloSimples;

			// Cria um novo vetor de velocidade com a velocidade X atual e a nova velocidade Y.
			// Isso mantém a velocidade horizontal e aplica a força do pulo no eixo Y.
			VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

			// Aplica o novo vetor de velocidade ao Rigidbody2D do personagem.
			// Isso faz o personagem pular, mantendo sua velocidade horizontal.
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

	void OnCollisionEnter2D(Collision2D objetoTocado ){

		TagObjetoTocado = objetoTocado.gameObject.tag;

		if (TagObjetoTocado == "parede") {
			print (TagObjetoTocado);
		
			PhysicsMaterial2D MaterialSemAtrito = new PhysicsMaterial2D();
			MaterialSemAtrito.friction = 0f; // Colisor nãoo possui atrito

			objetoTocado.collider.sharedMaterial = MaterialSemAtrito;
		}

		if (TagObjetoTocado == "bonusPuloDuplo") {
			TotalPulos = 2;
			Destroy (objetoTocado.gameObject);
		}

		if (TagObjetoTocado == "Chao") {
			ContadorPulos = 0;
		}

		/*
		if (TagObjetoTocado == "Fim") {
			SceneManager.LoadScene ("fase02");
		}
		*/

		if (TagObjetoTocado == "Fim" && PegouChave01==true) {
			SceneManager.LoadScene ("fase02");
		}

		if (TagObjetoTocado == "chave01") {
			PegouChave01 = true;
			Destroy (objetoTocado.gameObject);
		}
		if (TagObjetoTocado == "Diamante01") {
			print (TagObjetoTocado);
			Destroy (objetoTocado.gameObject);
		}
		if (TagObjetoTocado == "Diamante02") {
			print (TagObjetoTocado);
			Destroy (objetoTocado.gameObject,1.5f);
		}
		if (TagObjetoTocado == "Diamante03") {
			print (TagObjetoTocado);
			objetoTocado.rigidbody.velocity = new Vector2 (10f * DirecaoHorizontal, 10f);
			Destroy (objetoTocado.gameObject, 0.6f);
		}
		if (TagObjetoTocado == "tipo1") {
			if (VelocidadeHorizontalMaxima < 10) {
				VelocidadeHorizontalMaxima = VelocidadeHorizontalMaxima + 1f;
				print ("VelocidadeHorizontalMaxima: " + VelocidadeHorizontalMaxima);
				Destroy (objetoTocado.gameObject, 0.05f);
			}
		}
		if (TagObjetoTocado == "tipo2") {
			if (VelocidadeHorizontalMaxima >= 2) {
				VelocidadeHorizontalMaxima = VelocidadeHorizontalMaxima - 1f;
				print ("VelocidadeHorizontalMaxima: " + VelocidadeHorizontalMaxima);
				Destroy (objetoTocado.gameObject, 0.05f);
			}
		}

	}
}
