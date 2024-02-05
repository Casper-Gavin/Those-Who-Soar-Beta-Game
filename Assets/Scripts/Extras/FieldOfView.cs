using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {
    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    float fov;
    Vector3 origin;
    private float startingAngle;

    private void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 360f;
        origin = Vector3.zero;
    }

    private void LateUpdate() {
        int rayCount = 200;
        float angle = 0f;
        // float angle = startingAngle; // Use this if we lessen FOV
        float angleIncrease = fov / rayCount;
        float viewDistance = 7f;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++) {
            Vector3 vertex = Vector3.zero;
            RaycastHit2D hit = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if (CheckIfInFieldOfView(hit.point)) {
                if (hit.collider == null) {
                    vertex = origin + GetVectorFromAngle(angle) * viewDistance;
                } else {
                    vertex = hit.point;
                }
            }

            vertices[vertexIndex] = vertex;

            if (i > 0) {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex++;
            
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    private Vector3 GetVectorFromAngle(float angle) {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    public void SetOrigin(Vector3 origin) {
        this.origin = origin;
    }

    /* Use this if we lessen FOV
    public void SetAimDirection(Vector3 aimDirection) {
        startingAngle = GetAngleFromVectorFloat(aimDirection) - fov / 2f;
    }
    */

    // Check if a point is inside the field of view
    private bool CheckIfInFieldOfView(Vector3 point) {
        Vector3 dirToTarget = (point - origin).normalized;
        if (Vector3.Angle(transform.up, dirToTarget) < fov / 2f) {
            return true;
        }
        return false;
    }
}
