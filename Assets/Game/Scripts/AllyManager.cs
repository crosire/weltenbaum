using UnityEngine;

public class AllyManager : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField]
	GameObject[] _allies;
	#endregion

	Vector3[] _spawnpoints;
	CameraMovement _cameraMovement;

	void Start()
	{
		_cameraMovement = Camera.main.GetComponent<CameraMovement>();

		_spawnpoints = new Vector3[LaneManager.LaneCount];

		for (int lane = 0; lane < _spawnpoints.Length; lane++)
		{
			_spawnpoints[lane] = LaneManager.GetWaypoint(lane, LaneManager.GetWaypointCount(lane) - 1);
		}
	}

	public void Spawn(int type)
	{
		var instance = (GameObject)Instantiate(_allies[type], _spawnpoints[_cameraMovement.LaneIndex], Quaternion.identity);

		instance.transform.SetParent(this.transform);
		instance.GetComponent<PathFollowAgent>().LaneIndex = _cameraMovement.LaneIndex;
	}
}
