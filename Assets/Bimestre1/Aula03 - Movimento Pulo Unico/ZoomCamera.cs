using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
	public Camera cameraPrincipal;
	public float velocidadeZoom = 10f;

	void Start()
	{
		if (cameraPrincipal == null)
			cameraPrincipal = GetComponent<Camera>();
	}

	void Update()
	{
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		if(scroll != 0f)
		{
			Debug.Log("Scroll detectado: " + scroll);

			if(cameraPrincipal.orthographic)
			{
				cameraPrincipal.orthographicSize -= scroll * velocidadeZoom;
				cameraPrincipal.orthographicSize = Mathf.Clamp(cameraPrincipal.orthographicSize, 1f, 20f);
			}
			else
			{
				cameraPrincipal.fieldOfView -= scroll * velocidadeZoom;
				cameraPrincipal.fieldOfView = Mathf.Clamp(cameraPrincipal.fieldOfView, 20f, 60f);
			}
		}
	}
}
