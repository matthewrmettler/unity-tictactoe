using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public Text[] buttonList;
	private string playerSide;

	public GameObject gameOverPanel;
	public Text gameOverText;

	//Count the number of moves (to figure out when it's a tie/draw)
	private int moveCount;
	/**
	 * This gets called when the game starts.
	 **/
	void Awake()
	{
		SetGameControllerReferenceOnButtons ();
		playerSide = "X";
		gameOverPanel.SetActive(false);
		moveCount = 0;
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
		checkForWin();
		moveCount++;
		//Check for end of game
		if (moveCount >= 9) 
		{
			SetGameOverText ("It's a draw!");
		}
		//Otherwise, keep going
		ChangeSides();
	}

	void checkForWin()
	{
		//Top row
		if ((buttonList [0].text == playerSide && buttonList [1].text == playerSide && buttonList [2].text == playerSide)
		//Middle row
		|| (buttonList [3].text == playerSide && buttonList [4].text == playerSide && buttonList [5].text == playerSide)
		//Bottom row
		|| (buttonList [6].text == playerSide && buttonList [7].text == playerSide && buttonList [8].text == playerSide)
		//Left column
		|| (buttonList [0].text == playerSide && buttonList [3].text == playerSide && buttonList [6].text == playerSide)
		//Middle column
		|| (buttonList [1].text == playerSide && buttonList [4].text == playerSide && buttonList [7].text == playerSide)
		//Right column
		|| (buttonList [2].text == playerSide && buttonList [5].text == playerSide && buttonList [8].text == playerSide)
		//Top left to bottom right diagonal
		|| (buttonList [0].text == playerSide && buttonList [4].text == playerSide && buttonList [8].text == playerSide)
		//Bottom left to top right diagonal
		|| (buttonList [2].text == playerSide && buttonList [4].text == playerSide && buttonList [6].text == playerSide))
		{
			GameOver();
		}
	}

	void GameOver() 
	{
		//Disable all the buttons
		for (int i = 0; i < buttonList.Length; i++)
		{
			buttonList[i].GetComponentInParent<Button>().interactable = false;
		}
		gameOverPanel.SetActive(true);
		SetGameOverText(playerSide + " Wins!");
		Debug.Log ("Game over!");
	}

	void ChangeSides()
	{
		playerSide = (playerSide == "X") ? "O" : "X";
	}

	void SetGameOverText(string value)
	{
		gameOverPanel.SetActive(true);
		gameOverText.text = value;
	}
}
