using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum GameState
{
	Splash,
	Menu,
	Ingame,
	Won,
	Lost,
}

public class GameManager : MonoBehaviour
{
	bool _paused = false;
	GameState _currentState = GameState.Splash;
	int _currentLevel = 0;
	int _currentMap = 0;

	public static GameState CurrentState { get { return Singleton._currentState; } }

	private static GameManager Singleton { get; set; }

	void Awake()
	{
		Debug.Assert(Singleton == null, "Cannot create multiple instances of the 'GameManager' singleton class.");
		Singleton = this;
	}

	void Start()
	{
		SceneManager.LoadSceneAsync("Game/Splash", LoadSceneMode.Additive);
		_currentState = GameState.Splash;
	}

	void Update()
	{
		if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && _currentState == GameState.Ingame)
		{
			_paused = !_paused;
			Time.timeScale = _paused ? 0.0f : 1.0f;
		}
	}

	public static void SwitchGameState(GameState state)
	{
		Singleton.StartCoroutine(Singleton.SwitchGameStateCoroutine(Singleton._currentState, state));
		Singleton._currentState = state;
	}

	IEnumerator SwitchGameStateCoroutine(GameState oldState, GameState newState)
	{
		// From Splash to Menu
		if (oldState == GameState.Splash && newState == GameState.Menu)
		{
			SceneManager.UnloadScene("Game/Splash");
			SceneManager.LoadSceneAsync("Game/Menu", LoadSceneMode.Additive);
			yield return SceneManager.LoadSceneAsync("Game/Map", LoadSceneMode.Additive);
		}

		if (oldState == GameState.Menu && newState == GameState.Ingame)
		{
			SceneManager.UnloadScene("Game/Menu");
			yield return SceneManager.LoadSceneAsync("Game/Levels/0 - Tutorial", LoadSceneMode.Additive);
		}
	}
}
