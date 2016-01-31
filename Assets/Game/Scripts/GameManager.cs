using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum GameState
{
	None,
	Menu,
	Running,
	Won,
	Lost,
}

public class GameManager : MonoBehaviour
{
	bool _paused = false;
	GameState _currentState = GameState.None;

	public static GameState CurrentState { get { return Singleton._currentState; } }

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

	void Update()
	{
		if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && _currentState == GameState.Running)
		{
			_paused = !_paused;
			Time.timeScale = _paused ? 0.0f : 1.0f;
		}
	}

	public static void SwitchGameState(GameState state)
	{
		if (state == Singleton._currentState)
		{
			return;
		}

		Singleton.StartCoroutine(Singleton.SwitchGameStateCoroutine(state));
	}
	IEnumerator SwitchGameStateCoroutine(GameState state)
	{
		// Load new scene
		if (state != GameState.None)
		{
			yield return SceneManager.LoadSceneAsync("Game (" + state + ")", LoadSceneMode.Additive);
		}

		switch (_currentState)
		{
			case GameState.Menu:
				if (state == GameState.Running)
				{
					SceneManager.UnloadScene("Game (Menu)");
				}
				break;
			case GameState.Won:
			case GameState.Lost:
				if (state == GameState.Menu)
				{
					SceneManager.UnloadScene("Game (Running)");
					SceneManager.UnloadScene("Game (" + _currentState + ")");
				}
				break;
		}

		_currentState = state;
	}
}
