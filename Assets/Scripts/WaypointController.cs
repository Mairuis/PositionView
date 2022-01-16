using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class WaypointController : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField] private Vector3 _linePointOffset = Vector3.zero;

    public Transform[] Waypoints { get; private set; } = { };

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = true;
        Waypoints = GetWaypointTransforms();
    }

    // Update is called once per frame
    void Update()
    {
        var waypointTransforms = GetWaypointTransforms();
        var positions = waypointTransforms.Where(t => t.gameObject.activeSelf).Select(t => t.position + _linePointOffset).ToList();
        var person = GameObject.FindWithTag("Person");
        positions.Insert(0, person.transform.position);
        _lineRenderer.positionCount = positions.Count;
        _lineRenderer.SetPositions(positions.ToArray());
        Waypoints = waypointTransforms;
    }

    private Transform[] GetWaypointTransforms()
    {
        var waypointTransforms = transform
            .GetComponentsInChildren<Transform>(true)
            .Where(go => go.transform != this.transform).ToArray();
        return waypointTransforms;
    }
}