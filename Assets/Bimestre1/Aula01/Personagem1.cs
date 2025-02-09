using UnityEngine;

public class Personagem1 : MonoBehaviour
{
    // Variáveis para controlar a velocidade do personagem
     float VelocidadeX; // Velocidade no eixo X (horizontal)
     float VelocidadeY; // Velocidade no eixo Y (vertical)
     float VelocidadeHorizontal; // Velocidade de movimento horizontal
     float DirecaoHorizontal; // Direção do movimento (-1 para esquerda, 1 para direita, 0 parado)
     Vector2 VetorMovimentoPersonagem; // Vetor responsavel pela movimentacao do personagem

    // Referência ao componente Rigidbody2D do personagem
    private Rigidbody2D CorpoRigidoPersonagem;

    void Start()
    {
        // Inicializa o Rigidbody2D do personagem
        CorpoRigidoPersonagem = GetComponent<Rigidbody2D>();

        // Configurações iniciais do Rigidbody2D
        CorpoRigidoPersonagem.gravityScale = 1.0f; // Define a gravidade do personagem
        CorpoRigidoPersonagem.freezeRotation = true; // Impede que o personagem gire ao colidir

        //iniciar Variáveis
        VelocidadeX = 0;
        VelocidadeY = 0;
        VelocidadeHorizontal = 5;
        DirecaoHorizontal = 0;

        // inicializa o vetor de velocidade com zero em x e zero em y
        VetorMovimentoPersonagem = new Vector2(VelocidadeX, VelocidadeY);
    }

    void Update()
    {
        // Chama a função de movimento a cada frame
        MovimentoHorizontal();
    }

    void MovimentoHorizontal()
    {
        // Captura a entrada do jogador no eixo horizontal (teclas A/D ou setas esquerda/direita)
        DirecaoHorizontal = Input.GetAxis("Horizontal");

        // Calcula a velocidade no eixo X multiplicando a direção pela velocidade de andar
        VelocidadeX = VelocidadeHorizontal * DirecaoHorizontal;

        // Mantém a velocidade no eixo Y (para não interferir na gravidade)
        VelocidadeY = CorpoRigidoPersonagem.velocity.y;

        // Modifica o vetor de movimento com os novos valores de x e y
        VetorMovimentoPersonagem = new Vector2(VelocidadeX, VelocidadeY);

        // Aplica o vetor de movimento ao Rigidbody2D do personagem
        CorpoRigidoPersonagem.velocity = VetorMovimentoPersonagem;
    }
}