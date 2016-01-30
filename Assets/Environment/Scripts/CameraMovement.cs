using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	public Transform _cameraVocus;
	public Transform[] _lanePositions;
	int _laneIndex = 0;

	public int LaneIndex{ get; private set; }

	void Start()
	{
	}

	void Update()
	{
		this.transform.DOLookAt(_cameraVocus.transform.position, 0);
		UpdateLaneIndex();

	}

	void UpdateLaneIndex()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			if (_laneIndex > 0)
			{
				_laneIndex--;
			}
			else
			{
				_laneIndex = _lanePositions.Length - 1;
			}
			this.transform.DOMove(_lanePositions[_laneIndex].transform.position, 2.5f);
		}
		else if (Input.GetKeyDown(KeyCode.D))
		{
			if (_laneIndex < _lanePositions.Length - 1)
			{
				_laneIndex++;
			}
			else
			{
				_laneIndex = 0;
			}
			this.transform.DOMove(_lanePositions[_laneIndex].transform.position, 2.5f);
		}

	}
}
