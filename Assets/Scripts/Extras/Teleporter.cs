using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool finished = false;

    [Header("Boundary")]
    [SerializeField] public Vector3 pos1;
    [SerializeField] public Vector3 pos2;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos1, 0.3f);
        Gizmos.DrawWireSphere(pos2, 0.3f);
        Gizmos.DrawLine(pos1, new Vector3(pos1.x, pos2.y, pos1.z));
        Gizmos.DrawLine(pos1, new Vector3(pos2.x, pos1.y, pos1.z));
        Gizmos.DrawLine(pos2, new Vector3(pos2.x, pos1.y, pos1.z));
        Gizmos.DrawLine(pos2, new Vector3(pos1.x, pos2.y, pos1.z));
    }
}
