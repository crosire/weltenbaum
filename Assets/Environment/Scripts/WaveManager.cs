using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Wave
{
	public Entity[] _entities;
}

public class WaveManager : MonoBehaviour
{

	[Header("Entities")]
	public GameObject[] _enemies;
	public GameObject[] _allies;
	public GameObject[] _entitiesSpawnpoint;

	public Wave[] _wave;
	int _finishedWaves = 0;
	int _aliveEnemies;

	[Header("Misc")]
	public GameObject[] _spawnPoints;


	void Start()
	{
	}

	void Update()
	{
		if (_aliveEnemies <= 0)
		{
			SpawnWave();
		}

		//TODO 

	}

	void SpawnWave()
	{
		_aliveEnemies = _wave[_finishedWaves]._entities.Length;
		for (int i = 0; i < _wave[_finishedWaves]; i++)
		{
			GameObject _temp = Instantiate(_enemies[0], _spawnPoints[0].transform.position, Quaternion.identity) as GameObject;
			_temp.transform.parent = _entitiesSpawnpoint[0].transform;
		}

		if (_wave.Length > 0)
		{
			_finishedWaves++;
		}
		else
		{
			//You won!
			GameManager.SwitchGameState(GameState.Won);
		}

	}
}
