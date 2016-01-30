using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum GameState
{
	Title,
	Running,
	Paused,
	Won,
	Lost,
}

public class GameManager : MonoBehaviour
{
	GameState _currentState = GameState.Title;

	private static GameManager Singleton { get; set; }

	void Awake()
	{
		Debug.Assert(Singleton == null, "Cannot create multiple instances of the 'GameManager' singleton class.");

		Singleton = this;
	}
	void Start()
	{
		// Load user interface
		SceneManager.LoadScene("UI", LoadSceneMode.Additive);
	}

	IEnumerator LoadSceneAndSetActive(string name)
	{
		yield return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

		SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
	}

	public static void SwitchGameState(GameState state)
	{
		if (state == Singleton._currentState)
		{
			return;
		}

		Singleton.StartCoroutine(Singleton.LoadSceneAndSetActive("Game (" + state + ")"));

		SceneManager.UnloadScene("Game (" + Singleton._currentState + ")");

		Singleton._currentState = state;
	}
}
