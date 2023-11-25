using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private List<Vector3> path;
    [SerializeField] private float minDistanceToPoint = 0.1f;

    public Vector3 CurrentPoint => startPosition + currentPoint.Current;

    private Vector3 currentPosition;
    private Vector3 startPosition;
    private IEnumerator<Vector3> currentPoint;
    private float distanceToPoint;
    private bool gameStarted;

    private void Start()
    {
        startPosition = transform.position;
        currentPoint = GetPoint();
        currentPoint.MoveNext();

        currentPosition = transform.position;
        transform.position = currentPosition + currentPoint.Current;
        gameStarted = true;
    }

    private void Update()
    {
        if (path != null || path.Count > 0)
        {
            ComputePath();
        }
    }

    private void ComputePath()
    {
        distanceToPoint = (transform.position - (currentPosition + currentPoint.Current)).magnitude; // recalc dist to next point
        if (distanceToPoint < minDistanceToPoint) // reached next point
        {
            currentPoint.MoveNext();
        }
    }

    public IEnumerator<Vector3> GetPoint()
    {
        int index = 0;
        while(true)
        {
            yield return path[index];

            if (path.Count <= 1)
            {
                continue;
            }

            index++;
            if (index < 0)
            {
                index = path.Count - 1;
            }
            else if (index > path.Count - 1)
            {
                index = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!gameStarted && transform.hasChanged)
        {
            currentPosition = transform.position;
        }

        for (int index = 0; index < path.Count; index++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(currentPosition + path[index], 0.3f);
            Gizmos.color = Color.cyan;

            Gizmos.DrawLine(currentPosition + path[index], currentPosition + path[(index + 1) % path.Count]);

        }
    }
}
