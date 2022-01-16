using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float baseSpeed = 10;
    private int _currentWaypointIndex = 0;
    private WaypointController _wayPointController;
    private NavMeshAgent _navMeshAgent;

    // Start is called before the first frame update
    private void Start()
    {
        _wayPointController = FindObjectOfType<WaypointController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        var waypoints = _wayPointController.Waypoints;
        if (_currentWaypointIndex >= waypoints.Length)
        {
            _navMeshAgent.enabled = false;
            return;
        }

        var target = waypoints[_currentWaypointIndex];
        var position = target.transform.position;
        _navMeshAgent.speed = baseSpeed * _animationCurve.Evaluate(Vector3.Distance(transform.position, position));
        _navMeshAgent.SetDestination(position);

        if (!target.gameObject.activeSelf)
        {
            _currentWaypointIndex++;
        }
    }
}