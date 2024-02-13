using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Transform _waypointsHolder;

    private Transform[] _waypoints;
    private int _indexOfCurrentWaypoint;

    private void Start()
    {
        UpdateWaypoints();
    }

    private void Update()
    {
        Move();
    }

    private void UpdateWaypoints()
    {
        _waypoints = new Transform[_waypointsHolder.childCount];

        for (int i = 0; i < _waypointsHolder.childCount; i++)
        {
            _waypoints[i] = _waypointsHolder.GetChild(i);
        }
    }

    private void Move()
    {
        var waypoint = _waypoints[_indexOfCurrentWaypoint];
        transform.position = Vector3.MoveTowards(transform.position, waypoint.position, _movementSpeed * Time.deltaTime);

        if (transform.position == waypoint.position)
        {
            SwitchDirectionToNextWaypoint();
        }
    }

    private void SwitchDirectionToNextWaypoint()
    {
        _indexOfCurrentWaypoint++;

        if (_indexOfCurrentWaypoint >= _waypoints.Length)
            _indexOfCurrentWaypoint = 0;

        var waypointPosition = _waypoints[_indexOfCurrentWaypoint].position;
        transform.forward = waypointPosition - transform.position;
    }
}