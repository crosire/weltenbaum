using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Lane
{
	public Transform[] Waypoints;
}

public class LaneManager : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField]
	Lane[] _lanes;
	#endregion

	private static LaneManager Singleton { get; set; }

	public static Vector3 GetWaypoint(int lane, int index)
	{
		return Singleton._lanes[lane].Waypoints[index].position;
	}
	public static int GetWaypointCount(int lane)
	{
		return Singleton._lanes[lane].Waypoints.Length;
	}

	void Awake()
	{
		Debug.Assert(Singleton == null, "Cannot create multiple instances of the 'LaneManager' singleton class.");

		Singleton = this;
	}
}
