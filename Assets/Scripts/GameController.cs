using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public Text[] buttonList;
	private string playerSide;

	public GameObject gameOverPanel;
	public Text gameOverText;
	/**
	 * This gets called when the game starts.
	 **/
	void Awake()
	{
		SetGameControllerReferenceOnButtons ();
		playerSide = "X";
		gameOverPanel.SetActive(false);
	}

	void SetGameControllerReferenceOnButtons ()
	{
		for (int i = 0; i < buttonList.Length; i++) 
		{
			buttonList [i].GetComponentInParent<GridSpace> ().SetGameControllerReference(this);
		}
	}

	public string GetPlayerSide() 
	{
		return playerSide;
	}

	public void EndTurn()
	{
		//Top row
		if (buttonList [0].text == playerSide && buttonList [1].text == playerSide && buttonList [2].text == playerSide)
		{
			GameOver();
		}

		//Middle row
		if (buttonList [3].text == playerSide && buttonList [4].text == playerSide && buttonList [5].text == playerSide)
		{
			GameOver();
		}

		//Bottom row
		if (buttonList [6].text == playerSide && buttonList [7].text == playerSide && buttonList [8].text == playerSide)
		{
			GameOver();
		}

		//Left column
		if (buttonList [0].text == playerSide && buttonList [3].text == playerSide && buttonList [6].text == playerSide)
		{
			GameOver();
		}

		//Middle column
		if (buttonList [1].text == playerSide && buttonList [4].text == playerSide && buttonList [7].text == playerSide)
		{
			GameOver();
		}

		//Right column
		if (buttonList [2].text == playerSide && buttonList [5].text == playerSide && buttonList [8].text == playerSide)
		{
			GameOver();
		}

		//Top left to bottom right diagonal
		if (buttonList [0].text == playerSide && buttonList [4].text == playerSide && buttonList [8].text == playerSide)
		{
			GameOver();
		}

		//Bottom left to top right diagonal
		if (buttonList [2].text == playerSide && buttonList [4].text == playerSide && buttonList [6].text == playerSide)
		{
			GameOver();
		}

		ChangeSides();
	}

	void GameOver() 
	{
		//Disable all the buttons
		for (int i = 0; i < buttonList.Length; i++)
		{
			buttonList[i].GetComponentInParent<Button>().interactable = false;
		}
		gameOverPanel.SetActive (true);
		gameOverText.text = playerSide + " Wins!";
		Debug.Log ("Game over!");
	}

	void ChangeSides()
	{
		playerSide = (playerSide == "X") ? "O" : "X";
	}
}
