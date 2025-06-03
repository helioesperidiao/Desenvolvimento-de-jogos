using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform alvo;
	public Vector3 offset = new Vector3(0, 5, -10);
	public float suavidade = 0.1f;

	void LateUpdate()
	{
		if (alvo == null) return;

		Vector3 posicaoDesejada = alvo.position + offset;
		Vector3 posicaoSuave = Vector3.Lerp(transform.position, posicaoDesejada, suavidade);
		transform.position = posicaoSuave;

		// Faz a câmera olhar para o personagem para garantir que ele fique no centro da visão
		transform.LookAt(alvo);
	}
}