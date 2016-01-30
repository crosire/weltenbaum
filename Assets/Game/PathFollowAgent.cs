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
	public int WaypointIndex { get; private set; }

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

		var waypoint = LaneManager.GetWaypoint(LaneIndex, WaypointIndex);

		if (_agent.remainingDistance < 1.0f)
		{
			_agent.destination = LaneManager.GetWaypoint(LaneIndex, ++WaypointIndex);
		}
	}
}
