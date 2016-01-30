using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public GameObject[] _allies;
	public GameObject[] _alliesSpawnpoint;
	public CameraMovement _cameraMovement;

	void Start()
	{

	}

	void Update()
	{

	}

	public void OnGesture1()
	{
		var obj = (GameObject)Instantiate(_allies[0], _alliesSpawnpoint[_cameraMovement.LaneIndex].transform.position, Quaternion.identity);

		obj.GetComponent<PathFollowAgent>().LaneIndex = _cameraMovement.LaneIndex;
	}
}
