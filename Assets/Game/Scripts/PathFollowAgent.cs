using UnityEngine;
using System.Collections;

public class PathFollowAgent : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField]
	bool _reversed = false;
	#endregion

	Entity _entity;
	NavMeshAgent _navagent;
	int _laneIndex = 0;
	bool _stopped = false;

	public int LaneIndex
	{
		get
		{
			return _laneIndex;
		}
		set
		{
			_laneIndex = value;
			NextWaypointIndex = _reversed ? LaneManager.GetWaypointCount(LaneIndex) - 1 : 0;
		}
	}
	public int NextWaypointIndex { get; private set; }

	void Awake()
	{
		_entity = GetComponent<Entity>();
		_navagent = GetComponent<NavMeshAgent>();

		LaneIndex = 0;
	}

	void Update()
	{
		if (_entity.State != EntityState.Walk)
		{
			if (!_stopped)
			{
				_stopped = true;
				_navagent.Stop();
			}
			return;
		}
		else if (_stopped)
		{
			_stopped = false;
			_navagent.Resume();
		}

		if (_navagent.remainingDistance < 1.0f)
		{
			if (NextWaypointIndex < 0)
			{
				_entity.OnReachedLaneBegin();
				return;
			}
			if (NextWaypointIndex >= LaneManager.GetWaypointCount(LaneIndex))
			{
				_entity.OnReachedLaneEnd();
				return;
			}

			_navagent.destination = LaneManager.GetWaypoint(LaneIndex, _reversed ? NextWaypointIndex-- : NextWaypointIndex++);
		}
	}
}
