using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// Classe que controla o movimento do personagem 
public class Personagem02x07 : MonoBehaviour
{

	PhysicsMaterial2D MaterialSemAtrito;


	int QtdPontos;
	int QtdVidas;

	string TagTriggerEnter;
	string TagTriggerStay;
	string TagTriggerExit;

	string TagObjetoStay;
	string TagObjetoExit;



	int TotalPulos; //determina o limite de pulos do personagem
	int ContadorPulos; // Conta quantos pulos o personagem pode efetuar

	SpriteRenderer Rerender; //Contem a imagem do asset, utilizado para fazer o flip

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
	void Start()
	{
		// Cria um novo material e armazena na váriavel  MaterialSemAtrito
		MaterialSemAtrito = new PhysicsMaterial2D();
		MaterialSemAtrito.friction = 0f; // esse material possui atrito zero;

		TagTriggerEnter = "";
		TagTriggerStay = "";
		TagTriggerExit = "";

		TagObjetoStay = "";
		TagObjetoExit = "";

	

		//Inicalmente o personagme não pode pular pq o total de pulos é igual a zero.
		TotalPulos = 2; //determina o limite de pulos do personagem
		ContadorPulos = 0; // Conta quantos pulos o personagem pode efetuar

		Rerender = GetComponent<SpriteRenderer>(); // incializa a variável

		PegouChave01 = false;
		TagObjetoTocado = "";

		ApertouBotaoPular = false; // inicia a variavel com false;
		EstaTocandoAlgumColisor = false; // inicializa a variavel como falso;

		VelocidadePuloSimples = 10.0f; // determina o valor da velocidade do Pulo;

		// Obtém o componente Rigidbody2D do objeto ao qual este script está anexado
		CorpoRigidoPersonagem = GetComponent<Rigidbody2D>();

		Colisor2dPersonagem = GetComponent<Collider2D>();

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
		DirecaoVertical = 0f;            // Define a direção do movimento na vertical

		// Cria um vetor de velocidade inicial para o personagem
		VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);

		// Aplica o vetor de velocidade ao Rigidbody2D para mover o personagem
		CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
	}

	// Método chamado uma vez por frame (quadro)
	void Update()
	{

		MovimentoHorizontalFlipComReducaoAoPular();
		MovimentoPuloMultiploComReducaoVelocidade();
		//MovimentoHorizontalFlip ();
		//MovimentoPuloDuplo ();
		//MovimentoHorizontal ();
		//MovimentoPuloUnico();
		//MovimentoVertical (); 
		//MovimentoPuloSimples();
	}

	/// <summary>
	/// Controla o movimento horizontal do personagem com inversão de sprite (flip) e redução progressiva
	/// da velocidade conforme a quantidade de pulos realizados.
	/// </summary>
	/// <remarks>
	/// Este método realiza as seguintes ações:
	/// 1. Captura o input horizontal do jogador
	/// 2. Calcula a velocidade considerando reduções progressivas durante pulos
	/// 3. Mantém a velocidade vertical existente (para não interferir com física de pulo/gravidade)
	/// 4. Aplica o movimento ao Rigidbody2D
	/// 5. Inverte o sprite quando o movimento é para a esquerda
	/// </remarks>
	void MovimentoHorizontalFlipComReducaoAoPular()
	{
		// Obtém a direção horizontal do input (-1 = esquerda, 1 = direta, 0 = neutro)
		DirecaoHorizontal = Input.GetAxis("Horizontal");

		// Calcula a velocidade base multiplicando a direção pela velocidade máxima
		VelocidadeX = VelocidadeHorizontalMaxima * DirecaoHorizontal;

		// Aplica redução progressiva da velocidade horizontal conforme quantidade de pulos:
		// - 1 pulo: 80% da velocidade normal
		// - 2 pulos: 60% da velocidade normal
		// - 3 pulos: 40% da velocidade normal
		if (ContadorPulos == 1)
		{
			VelocidadeX = (VelocidadeHorizontalMaxima * DirecaoHorizontal) * 0.8f;
		}
		if (ContadorPulos == 2)
		{
			VelocidadeX = (VelocidadeHorizontalMaxima * DirecaoHorizontal) * 0.6f;
		}
		if (ContadorPulos == 3)
		{
			VelocidadeX = (VelocidadeHorizontalMaxima * DirecaoHorizontal) * 0.4f;
		}

		// Preserva a velocidade atual no eixo Y para manter a física de pulo/queda
		VelocidadeY = CorpoRigidoPersonagem.velocity.y;

		// Cria um novo vetor de velocidade combinando os componentes X (calculado) e Y (preservado)
		VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);

		// Aplica o vetor de velocidade ao Rigidbody2D para efetivar o movimento
		CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;

		// Controla a inversão do sprite (flip) baseado na direção do movimento:
		// - Para esquerda (valores negativos): ativa flipX
		// - Para direita (valores positivos): desativa flipX
		if (DirecaoHorizontal < 0)
		{
			Rerender.flipX = true;
		}
		else if (DirecaoHorizontal > 0)
		{
			Rerender.flipX = false;
		}
	}

	/// <summary>
	/// Controla o sistema de pulos múltiplos com redução progressiva da força do pulo
	/// </summary>
	/// <remarks>
	/// Este método implementa um sistema de pulo que permite:
	/// 1. Múltiplos pulos consecutivos no ar (até o limite definido por TotalPulos)
	/// 2. Redução progressiva da força vertical em pulos consecutivos
	/// 3. Manutenção da velocidade horizontal durante os pulos
	/// 
	/// Requer configuração prévia:
	/// - Input "Jump" configurado no Input Manager
	/// - Variáveis VelocidadePuloSimples e TotalPulos definidas
	/// - Referência ao Rigidbody2D do personagem
	/// </remarks>
	void MovimentoPuloMultiploComReducaoVelocidade()
	{
		// Verifica se o botão de pulo (configurado como "Jump") foi pressionado neste frame
		ApertouBotaoPular = Input.GetButtonDown("Jump");

		// Condições para executar o pulo:
		// 1. Botão de pulo pressionado
		// 2. Número de pulos atual menor que o total permitido
		if (ApertouBotaoPular == true && ContadorPulos < TotalPulos)
		{
			// Incrementa o contador de pulos realizados
			ContadorPulos = ContadorPulos + 1;

			// Preserva a velocidade horizontal atual do personagem
			VelocidadeX = CorpoRigidoPersonagem.velocity.x;

			// Define a força vertical do pulo com redução progressiva:
			// - 1º pulo: 100% da velocidade normal
			// - 2º pulo: 80% da velocidade normal
			// - 3º pulo: 70% da velocidade normal
			if (ContadorPulos == 1)
			{
				VelocidadeY = VelocidadePuloSimples;
			}
			if (ContadorPulos == 2)
			{
				VelocidadeY = VelocidadePuloSimples * 0.8f;
			}
			if (ContadorPulos == 3)
			{
				VelocidadeY = VelocidadePuloSimples * 0.7f;
			}

			// Cria vetor de velocidade combinando:
			// - X: velocidade horizontal preservada
			// - Y: nova velocidade vertical calculada
			VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);

			// Aplica o vetor de velocidade ao Rigidbody2D para executar o movimento
			CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
		}
	}


	void MovimentoPuloDuplo()
	{

		// Verifica se o botão de pular foi pressionado.
		// "Jump" é o nome do botão configurado no Input Manager do Unity.
		ApertouBotaoPular = Input.GetButtonDown("Jump");


		// Verifica se o botão de pular foi pressionado e se o personagem está tocando algum colisor.
		if (ApertouBotaoPular == true && ContadorPulos < 2)
		{

			ContadorPulos = ContadorPulos + 1;  //ContadorPulos++;

			// Armazena a velocidade atual no eixo X do personagem.
			VelocidadeX = CorpoRigidoPersonagem.velocity.x;

			// Define a velocidade no eixo Y para o valor de VelocidadePuloSimples.
			// VelocidadePuloSimples é provavelmente uma variável que define a força do pulo.
			VelocidadeY = VelocidadePuloSimples;

			// Cria um novo vetor de velocidade com a velocidade X atual e a nova velocidade Y.
			VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);

			// Aplica o novo vetor de velocidade ao Rigidbody2D do personagem.
			// Isso faz o personagem pular.
			CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
		}

	}

	void MovimentoHorizontalFlip()
	{

		// Obtém a direção horizontal a partir do input do jogador (teclado ou controle)
		// Input.GetAxis("Horizontal") retorna um valor entre -1 e 1:
		// -1 para esquerda, 1 para direita, 0 se nenhuma tecla do eixo horizontal estiver sendo pressionada
		DirecaoHorizontal = Input.GetAxis("Horizontal");

		// Calcula a velocidade no eixo X multiplicando a direção pela velocidade máxima
		VelocidadeX = VelocidadeHorizontalMaxima * DirecaoHorizontal;

		// Mantém a velocidade atual no eixo Y (para não interferir na gravidade ou pulo)
		VelocidadeY = CorpoRigidoPersonagem.velocity.y;

		// Cria um novo vetor de velocidade com os valores calculados
		VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);

		// Aplica o vetor de velocidade ao Rigidbody2D para mover o personagem
		CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
		if (DirecaoHorizontal < 0)
		{
			Rerender.flipX = true;
		}
		else if (DirecaoHorizontal > 0)
		{
			Rerender.flipX = false;
		}

	}

	void MovimentoPuloUnico()
	{

		// Verifica se o botão de pular foi pressionado.
		// "Jump" é o nome do botão configurado no Input Manager do Unity.
		ApertouBotaoPular = Input.GetButtonDown("Jump");

		// Verifica se o personagem está tocando algum colisor.
		// Colisor2dPersonagem é uma referência ao Collider2D do personagem.
		// IsTouchingLayers() retorna true se o colisor estiver tocando qualquer camada.
		EstaTocandoAlgumColisor = Colisor2dPersonagem.IsTouchingLayers();

		// Verifica se o botão de pular foi pressionado e se o personagem está tocando algum colisor.
		if (ApertouBotaoPular == true && EstaTocandoAlgumColisor == true)
		{

			// Armazena a velocidade atual no eixo X do personagem.
			VelocidadeX = CorpoRigidoPersonagem.velocity.x;

			// Define a velocidade no eixo Y para o valor de VelocidadePuloSimples.
			// VelocidadePuloSimples é provavelmente uma variável que define a força do pulo.
			VelocidadeY = VelocidadePuloSimples;

			// Cria um novo vetor de velocidade com a velocidade X atual e a nova velocidade Y.
			VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);

			// Aplica o novo vetor de velocidade ao Rigidbody2D do personagem.
			// Isso faz o personagem pular.
			CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
		}

	}

	void MovimentoPuloSimples()
	{

		// Verifica se o botão de pular foi pressionado.
		// "Jump" é o nome do botão configurado no Input Manager do Unity.
		ApertouBotaoPular = Input.GetButtonDown("Jump");

		// Verifica se o botão de pular foi pressionado.
		if (ApertouBotaoPular == true)
		{

			// Armazena a velocidade atual no eixo X do personagem.
			// CorpoRigidoPersonagem.velocity.x retorna a velocidade horizontal do Rigidbody2D.
			VelocidadeX = CorpoRigidoPersonagem.velocity.x;

			// Define a velocidade no eixo Y para o valor de VelocidadePuloSimples.
			// VelocidadePuloSimples é uma variável que define a força do pulo.
			VelocidadeY = VelocidadePuloSimples;

			// Cria um novo vetor de velocidade com a velocidade X atual e a nova velocidade Y.
			// Isso mantém a velocidade horizontal e aplica a força do pulo no eixo Y.
			VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);

			// Aplica o novo vetor de velocidade ao Rigidbody2D do personagem.
			// Isso faz o personagem pular, mantendo sua velocidade horizontal.
			CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
		}
	}

	//Método para controlar o movimento Vertical do personagem.
	void MovimentoVertical()
	{

		// Obtém a direção Vertical a partir do input do jogador (teclado ou controle)
		// Input.GetAxis("Vertical") retorna um valor entre (-1 e 1):
		// -1 para baixo, 1 para cima, 0 se nenhuma tecla do eixo vertical estiver sendo pressionada
		DirecaoVertical = Input.GetAxis("Vertical");

		VelocidadeX = CorpoRigidoPersonagem.velocity.x; // recupera a velocidade em x do corpo ridido do personagem.
		VelocidadeY = DirecaoVertical * VelocidadeVerticalMaxima; // calculo a velocidade em y do personagem.

		// cria um novo vetor de velocidade com VelocidadeX e VelocidadeY
		VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);

		//adiciona o novo vetor de velocidade a velocidade(velocity) do corpo rididp do personagem.
		CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;

	}

	// Método para controlar o movimento horizontal do personagem
	void MovimentoHorizontal()
	{

		// Obtém a direção horizontal a partir do input do jogador (teclado ou controle)
		// Input.GetAxis("Horizontal") retorna um valor entre -1 e 1:
		// -1 para esquerda, 1 para direita, 0 se nenhuma tecla do eixo horizontal estiver sendo pressionada
		DirecaoHorizontal = Input.GetAxis("Horizontal");

		// Calcula a velocidade no eixo X multiplicando a direção pela velocidade máxima
		VelocidadeX = VelocidadeHorizontalMaxima * DirecaoHorizontal;

		// Mantém a velocidade atual no eixo Y (para não interferir na gravidade ou pulo)
		VelocidadeY = CorpoRigidoPersonagem.velocity.y;

		// Cria um novo vetor de velocidade com os valores calculados
		VetorVelocidadePersonagem = new Vector2(VelocidadeX, VelocidadeY);

		// Aplica o vetor de velocidade ao Rigidbody2D para mover o personagem
		CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
	}

	void OnTriggerEnter2D(Collider2D objetoTriggerEnter)
	{
		TagTriggerEnter = objetoTriggerEnter.gameObject.tag;

		if (TagTriggerEnter.Contains("tipo1"))
		{
			print("OnTriggerEnter2D():" + TagTriggerEnter);
			Destroy(objetoTriggerEnter.gameObject);

		}
	



	

	}

	void OnTriggerStay2D(Collider2D objetoTriggerStay)
	{
		TagTriggerStay = objetoTriggerStay.gameObject.tag;
		//print ("OnTriggerStay2D():" + TagTriggerStay);
	}


	void OnTriggerExit2D(Collider2D objetoTriggerExit)
	{
		TagTriggerExit = objetoTriggerExit.gameObject.tag;
		//print ("OnTriggerExit2D():" + TagTriggerExit);
	}

	void OnCollisionExit2D(Collision2D objetoExit)
	{
		TagObjetoExit = objetoExit.gameObject.tag;

		if (TagObjetoExit.Contains("lama"))
		{
			print("onCollisionExit():" + TagObjetoExit);
			VelocidadeHorizontalMaxima = 5.0f;
		}
	}

	void OnCollisionStay2D(Collision2D objetoStay)
	{
		TagObjetoStay = objetoStay.gameObject.tag;
		if (TagObjetoStay.Contains("lama"))
		{
			print("OnCollisionStay():" + TagObjetoExit);
			VelocidadeHorizontalMaxima = 1.0f;
		}
	}

	void OnCollisionEnter2D(Collision2D objetoTocado)
	{

		TagObjetoTocado = objetoTocado.gameObject.tag;

		if (TagObjetoTocado.Contains("semAtrito"))
		{
			print(" OnCollisionEnter2D():" + TagObjetoTocado);
			objetoTocado.collider.sharedMaterial = MaterialSemAtrito;
		}


		if (TagObjetoTocado.Contains("reiniciar"))
		{
			SceneManager.LoadScene("fase01");

		}

		if (TagObjetoTocado.Contains("fechar"))
		{
			Debug.Log("Saindo do jogo...");
			Application.Quit();

		}

	

		if (TagObjetoTocado.Contains("trampolim"))
		{
			CorpoRigidoPersonagem.velocity = new Vector2(0, 15);
		}

		/*
		if (TagObjetoTocado == "Fim") {
			SceneManager.LoadScene ("fase02");
		}
		*/
		if (TagObjetoTocado.Contains("semAtrito"))
		{
			print("Tocou TAG: semAtrito");
			objetoTocado.collider.sharedMaterial = MaterialSemAtrito;
		}
		if (TagObjetoTocado.Contains("chao"))
		{
			print("OnCollisionEnter2D():" + TagObjetoTocado);
			ContadorPulos = 0;
		}

		if (TagObjetoTocado == "bonusPuloUnico")
		{
			TotalPulos = 1;
			Destroy(objetoTocado.gameObject);
		}
		if (TagObjetoTocado == "bonusPuloDuplo")
		{
			TotalPulos = 2;
			Destroy(objetoTocado.gameObject);
		}


		if (TagObjetoTocado == "Fim" && PegouChave01 == true)
		{
			SceneManager.LoadScene("fase02");
		}

		if (TagObjetoTocado == "chave01")
		{
			PegouChave01 = true;
			Destroy(objetoTocado.gameObject);
		}
		if (TagObjetoTocado == "Diamante01")
		{
			print(TagObjetoTocado);
			Destroy(objetoTocado.gameObject);
		}
		if (TagObjetoTocado == "Diamante02")
		{
			print(TagObjetoTocado);
			Destroy(objetoTocado.gameObject, 1.5f);
		}
		if (TagObjetoTocado == "Diamante03")
		{
			print(TagObjetoTocado);
			objetoTocado.rigidbody.velocity = new Vector2(10f * DirecaoHorizontal, 10f);
			Destroy(objetoTocado.gameObject, 0.6f);
		}
		if (TagObjetoTocado == "tipo1")
		{
			if (VelocidadeHorizontalMaxima < 10)
			{
				VelocidadeHorizontalMaxima = VelocidadeHorizontalMaxima + 1f;
				print("VelocidadeHorizontalMaxima: " + VelocidadeHorizontalMaxima);
				Destroy(objetoTocado.gameObject, 0.05f);
			}
		}
		if (TagObjetoTocado == "tipo2")
		{
			if (VelocidadeHorizontalMaxima >= 2)
			{
				VelocidadeHorizontalMaxima = VelocidadeHorizontalMaxima - 1f;
				print("VelocidadeHorizontalMaxima: " + VelocidadeHorizontalMaxima);
				Destroy(objetoTocado.gameObject, 0.05f);
			}
		}

	}
}
