using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    [SerializeField] private WaypointPath _waypointPath;
    [SerializeField] private float _speed = 4.0f;

    private int _targetWaypointIndex;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private float _elapsedTime;
    private float _platformRevalations;

    [SerializeField] private bool isParent = false;

    // Start is called before the first frame update
    void Start()
    {
        TargetNextWaypoint();
        _platformRevalations = 2.0f;
    }


    void FixedUpdate()
    {
        //Platform functionality
        float elapsedPercentage = _elapsedTime / _timeToWaypoint;

        if (isParent == true)
        {
            _elapsedTime += Time.deltaTime;
        }
        else
        {
            _elapsedTime = 0;
        }

        //Platform interpolation between waypoints
        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);

        if (elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
            _platformRevalations++;
        }

        //Checks if platform has moved back to the starting position
        if (_platformRevalations == 2.0f)
        {
            _platformRevalations = 0.0f;
            isParent = false;
        }
    }

    private void TargetNextWaypoint()
    {
        _previousWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);

        _timeToWaypoint = distanceToWaypoint / _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
        isParent = true;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}