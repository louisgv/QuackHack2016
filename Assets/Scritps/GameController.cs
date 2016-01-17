using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public enum GameState
	{
		GOING,
		GAMEOVER
	}
	
	public static GameState gameState = GameState.GOING;
	
	public GameObject GameOverScene;
	
	public GameObject Canvas;
	
	private void DoGameOverThings ()
	{
		GameOverScene.SetActive (true);
		Canvas.SetActive (false);
		Time.timeScale = 0;
	}
	
	public void Update ()
	{
		switch (gameState) {
		case GameState.GAMEOVER: 
			DoGameOverThings ();
			break;
		}
	}
}
