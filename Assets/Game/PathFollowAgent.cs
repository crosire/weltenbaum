using UnityEngine;
using System.Collections;

public class PathFollowAgent : MonoBehaviour
{
	#region Inspector Variables
	#endregion

	NavMeshAgent _agent;

	public int Lane { get; set; }
	public int Waypoint { get; private set; }

	void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
	}
}
