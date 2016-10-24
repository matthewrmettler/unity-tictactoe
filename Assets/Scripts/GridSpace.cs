using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GridSpace : MonoBehaviour {
	public Button button;
	public Text buttonText;
	public string playerSide;

	public void SetSpace() 
	{
		buttonText.text = playerSide;
		button.interactable = false;
	}

}