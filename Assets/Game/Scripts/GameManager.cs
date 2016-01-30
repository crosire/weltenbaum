using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum GameState
{
	None,
	Menu,
	Running,
	Paused,
	Won,
	Lost,
}

public class GameManager : MonoBehaviour
{
	GameState _currentState = GameState.None;

	private static GameManager Singleton { get; set; }

	void Awake()
	{
		Debug.Assert(Singleton == null, "Cannot create multiple instances of the 'GameManager' singleton class.");

		Singleton = this;
	}
	void Start()
	{
		SwitchGameState(GameState.Menu);
	}

	public static void SwitchGameState(GameState state)
	{
		if (state == Singleton._currentState)
		{
			return;
		}

		// Load new scene
		if (state != GameState.None)
		{
			SceneManager.LoadSceneAsync("Game (" + state + ")", LoadSceneMode.Additive);
		}

		// Unload previous scene
		if (Singleton._currentState != GameState.None)
		{
			SceneManager.UnloadScene("Game (" + Singleton._currentState + ")");
		}

		Singleton._currentState = state;
	}
}
