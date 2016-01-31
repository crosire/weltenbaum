using UnityEngine;

public class AllyManager : MonoBehaviour
{
	#region Inspector Variables

	[SerializeField]
	GameObject[] _allies;
	[SerializeField]
	float _spawnCooldown = 2.0f;

	#endregion

	float[] _cooldowns;
	Vector3[] _spawnpoints;
	CameraMovement _cameraMovement;

	private static AllyManager Singleton { get; set; }

	public static float GetRemainingCooldown(int type)
	{
		return Singleton._cooldowns[type];
	}

	void Awake()
	{
		Debug.Assert(Singleton == null, "Cannot create multiple instances of the 'AllyManager' singleton class.");

		Singleton = this;
	}

	void Start()
	{
		_cameraMovement = Camera.main.GetComponent<CameraMovement>();

		_cooldowns = new float[LaneManager.LaneCount];
		_spawnpoints = new Vector3[LaneManager.LaneCount];

		for (int lane = 0; lane < _spawnpoints.Length; lane++)
		{
			_spawnpoints[lane] = LaneManager.GetWaypoint(lane, LaneManager.GetWaypointCount(lane) - 1);
		}
	}

	void Update()
	{
		for (int i = 0; i < _cooldowns.Length; i++)
		{
			if (_cooldowns[i] > 0.0f)
			{
				_cooldowns[i] -= Time.deltaTime;
				if (Singleton._cooldowns[i] < 0f)
				{
					Singleton._cooldowns[i] = 0f;
				}
			}
		}
	}

	public void Spawn(int type)
	{
		if (_cooldowns[type] > 0.0f)
		{
			return;
		}

		_cooldowns[type] = _spawnCooldown;

		var instance = (GameObject)Instantiate(_allies[type], _spawnpoints[_cameraMovement.LaneIndex], Quaternion.identity);

		instance.transform.SetParent(this.transform);
		instance.GetComponent<PathFollowAgent>().LaneIndex = _cameraMovement.LaneIndex;
	}
}
