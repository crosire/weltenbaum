using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Wave
{
	public int _entityType0, _entityType1, _entityType2;
}

public class WaveManager : MonoBehaviour
{

	[Header("Entities")]
	public GameObject[] _enemies;
	public GameObject[] _entitiesSpawnpoint;

	public Wave[] _waves;
	int _finishedWaves = 0;
	int _aliveEnemies = 0;

	public static int AliveEnemies { get; set; }

	public static WaveManager Singleton { get; set; }

	void Awake()
	{
		Debug.Assert(Singleton == null, "Cannot create multiple instances of the 'GameManager' singleton class.");

		Singleton = this;
	}

	void Update()
	{
		if (_aliveEnemies <= 0)
		{
			SpawnWave();
		}
	}

	void SpawnWave()
	{
		_aliveEnemies = _waves[_finishedWaves]._entityType0 + _waves[_finishedWaves]._entityType1 + _waves[_finishedWaves]._entityType2;
		for (int i = 0; i < _aliveEnemies; i++)
		{
			Instantiate(_enemies[0], _entitiesSpawnpoint[0].transform.position, Quaternion.identity);
		}

		if (_finishedWaves < _waves.Length)
		{
			_finishedWaves++;
		}
		else
		{
			GameManager.SwitchGameState(GameState.Won);
		}

	}
}
