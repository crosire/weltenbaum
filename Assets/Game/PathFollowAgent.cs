using UnityEngine;
using System.Collections;

public class PathFollowAgent : MonoBehaviour
{
	#region Inspector Variables
	#endregion

	Entity _entity;
	NavMeshAgent _agent;
	bool _isStopped = false;

	public int LaneIndex { get; set; }
	public int NextWaypointIndex { get; private set; }

	void Awake()
	{
		_entity = GetComponent<Entity>();
		_agent = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		if (_entity._entityState != EntityState.Walk)
		{
			if (!_isStopped)
			{
				_agent.Stop();
				_isStopped = true;
			}
			return;
		}
		else if (_isStopped)
		{
			_agent.Resume();
			_isStopped = false;
		}

		if (_agent.remainingDistance < 1.0f)
		{
			if (NextWaypointIndex >= LaneManager.GetWaypointCount(LaneIndex))
			{
				_entity.OnReachedLaneEnd();
				return;
			}

			_agent.destination = LaneManager.GetWaypoint(LaneIndex, NextWaypointIndex++);
		}
	}
}
