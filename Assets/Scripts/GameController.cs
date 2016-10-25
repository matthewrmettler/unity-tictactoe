using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Player {
	public Image panel;
	public Text text;
}

[System.Serializable]
public class PlayerColor {
	public Color panelColor;
	public Color textColor;
}

public class GameController : MonoBehaviour {
	public Player playerX;
	public Player playerO;
	public PlayerColor activePlayerColor;
	public PlayerColor inactivePlayerColor;

	public Text[] buttonList;
	private string playerSide;

	public GameObject gameOverPanel;
	public GameObject restartButton;
	public Text gameOverText;

	//Count the number of moves (to figure out when it's a tie/draw)
	private int moveCount;
	/**
	 * This gets called when the game starts.
	 **/
	void Awake()
	{
		SetGameControllerReferenceOnButtons ();
		RestartGame ();
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
		if (checkForWin ()) {
			return;
		}
		//No one won, keep going
		moveCount++;
		//Check for end of game
		if (moveCount >= 9) 
		{
			GameOver ("draw");
		}
		//Otherwise, keep going
		ChangeSides();
	}

	bool checkForWin()
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
			GameOver(playerSide);
			return true;
		}
		return false;
	}

	void GameOver(string winningPlayer) 
	{
		//Disable all the buttons
		SetBoardInteractable(false);
		gameOverPanel.SetActive(true);
		restartButton.SetActive (true);
		if (winningPlayer == "draw") {
			SetGameOverText("It's a draw!");
		} else {
			SetGameOverText(winningPlayer + " Wins!");
		}
		Debug.Log ("Game over!");
	}

	void ChangeSides()
	{

		playerSide = (playerSide == "X") ? "O" : "X";

		if (playerSide == "X") {
			SetPlayerColors (playerX, playerO);
		} else {
			SetPlayerColors (playerO, playerX);
		}
	}

	void SetGameOverText(string value)
	{
		gameOverPanel.SetActive(true);
		gameOverText.text = value;
	}

	public void RestartGame()
	{
		playerSide = "X";
		SetPlayerColors (playerX, playerO);
		moveCount = 0;
		gameOverPanel.SetActive(false);
		restartButton.SetActive (false);
		//Enable all the buttons
		SetBoardInteractable(true);
	}

	public void SetBoardInteractable(bool toggle)
	{
		for (int i = 0; i < buttonList.Length; i++)
		{
			buttonList[i].GetComponentInParent<Button>().interactable = toggle;
			if (toggle) buttonList [i].text = "";
		}
	}

	void SetPlayerColors(Player newPlayer, Player oldPlayer)
	{
		newPlayer.panel.color = activePlayerColor.panelColor;
		newPlayer.text.color = activePlayerColor.textColor;

		oldPlayer.panel.color = inactivePlayerColor.panelColor;
		oldPlayer.text.color = inactivePlayerColor.textColor;
	}
}
