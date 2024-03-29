using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("Boundary")]
    [SerializeField] public Vector3 pos1;
    [SerializeField] public Vector3 pos2;

    private void OnDrawGizmos()
    {
        Vector3 position1 = transform.position + pos1;
        Vector3 position2 = transform.position + pos2;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(position1, 0.3f);
        Gizmos.DrawWireSphere(position2, 0.3f);
        Gizmos.DrawLine(position1, new Vector3(position1.x, position2.y, position1.z));
        Gizmos.DrawLine(position1, new Vector3(position2.x, position1.y, position1.z));
        Gizmos.DrawLine(position2, new Vector3(position2.x, position1.y, position1.z));
        Gizmos.DrawLine(position2, new Vector3(position1.x, position2.y, position1.z));
    }
}
