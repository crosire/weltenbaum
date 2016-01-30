using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Lane
{
	public Transform Waypoints;
}

public class LaneManager : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField]
	Lane[] _lanes;
	#endregion

	public static int LaneCount { get { return Singleton._lanes.Length; } }

	private static LaneManager Singleton { get; set; }

	public static Lane GetLane(int lane)
	{
		return Singleton._lanes[lane];
	}
	public static Vector3 GetWaypoint(int lane, int index)
	{
		return Singleton._lanes[lane].Waypoints.GetChild(index).position;
	}
	public static int GetWaypointCount(int lane)
	{
		return Singleton._lanes[lane].Waypoints.childCount;
	}

	void Awake()
	{
		Debug.Assert(Singleton == null, "Cannot create multiple instances of the 'LaneManager' singleton class.");

		Singleton = this;
	}
}
