using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClickAsset : MonoBehaviour{
	//executado quando acontece um click no asset.
	void OnMouseDown()   
	{
		string tagObjeto = gameObject.tag;
		print("GameObject clicado: " + tagObjeto);
		if (tagObjeto == "botao_start") {
			SceneManager.LoadScene("fase01");
		}
	}
	//executado quando click é solto no asset.
	void OnMouseUp()  {
		string tagObjeto = gameObject.tag;
		print("Mouse solto sobre o GameObject: " + tagObjeto);
	}
	//executado quando mouse entra no asset.
	void OnMouseEnter()     
	{
		string tagObjeto = gameObject.tag;
		print("Mouse entrou no GameObject: " + tagObjeto);
	}
	//executado quando mouse sai no asset.
	void OnMouseExit() {
		string tagObjeto = gameObject.tag;
		print("Mouse saiu do GameObject: " + tagObjeto);
	}
	//executado quando mouse está sobre o asset.
	void OnMouseOver() {
		string tagObjeto = gameObject.tag;
		print("Mouse está sobre o GameObject: " + tagObjeto);
	}
}