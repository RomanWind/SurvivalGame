using UnityEngine;

public class DungeonMapWaypoints : MonoBehaviour
{
    private Transform[] _waypoints;
    private string[] _waypointsNames;
    private int _targetWaypointIndex = 0;

    private void Start()
    {
        _waypoints = new Transform[gameObject.transform.childCount];
        _waypointsNames = new string[gameObject.transform.childCount];

        for (int i = 0; i < _waypoints.Length; i++)
        {
            _waypoints[i] = gameObject.transform.GetChild(i);
            _waypointsNames[i] = gameObject.transform.GetChild(i).name;
        }
    }

    public Transform[] GetWaypointPosition()
    {
        return _waypoints;
    }

    public string[] GetWaypointName()
    {
        return _waypointsNames;
    }

    public void IncrementTargetWaypoint()
    {
        _targetWaypointIndex += 1;
    }

    public int GetTargetWaypoint()
    {
        return _targetWaypointIndex;
    }
}
