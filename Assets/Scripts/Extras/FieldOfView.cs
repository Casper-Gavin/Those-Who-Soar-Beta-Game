using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {
    [SerializeField] private Character character;
    private GameObject characterBackup;

    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    private float fov;
    [SerializeField] private float viewDistance;
    private Vector3 origin;
    private float startingAngle;

    [SerializeField] private GameObject torch;
    private CTorch torchScript;
    private float currentPhase = 0f;
    public float pulseSpeed = 1f;
    public float pulseMagnitude = 0.5f;
    public bool shouldUpdatePulseParameters = true;


    private void Awake() {
        character = GetComponent<Character>();
        characterBackup = GameObject.Find("Player");
        torch = GameObject.Find("Torch");

        if (torch != null) {
            torchScript = torch.GetComponent<CTorch>();
        }
    }

    private void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 360f;
        
        if (viewDistance == null){
            viewDistance = 8f;
        }

        origin = Vector3.zero;
    }

    private void Update () {
        //SetOrigin(character.transform.position);

        if (character != null) {
            //SetOrigin(character.transform.position);
        } else {
            //SetOrigin(Vector3.zero);

            character = characterBackup.GetComponent<Character>();
        }

        if (torch == null) {
            torch = GameObject.Find("Torch");
        } else {
            if (torchScript == null) {
                torchScript = torch.GetComponent<CTorch>();
            } else if (torchScript.torchHasSpawned && torchScript.newViewDistance != viewDistance) {
                currentPhase = Mathf.Sin(pulseSpeed * Time.time);

                if (currentPhase > 0 && shouldUpdatePulseParameters) {
                    pulseSpeed = Random.Range(0.5f, 1.5f);
                    pulseMagnitude = Random.Range(0.25f, 1.0f);
                    shouldUpdatePulseParameters = false;
                } else if (currentPhase < 0) {
                    shouldUpdatePulseParameters = true;
                }

                float targetViewDistance = torchScript.newViewDistance + (Mathf.Sin(pulseSpeed * Time.time) * pulseMagnitude);

                viewDistance = Mathf.Lerp(viewDistance, targetViewDistance, torchScript.torchLerpTime * Time.deltaTime);

                //float pulseSpeed = Random.Range(0.5f, 1.5f);
                //float pulseMagnitude = Random.Range(0.25f, 1.0f);

                //float targetViewDistance = torchScript.newViewDistance + (Mathf.Sin(pulseSpeed * Time.time) * pulseMagnitude);

                //viewDistance = Mathf.Lerp(viewDistance, targetViewDistance, torchScript.torchLerpTime * Time.deltaTime);
            }
        }
    }

    private void LateUpdate() {
        int rayCount = 1000;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++) {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null) {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            } else {
                vertex = raycastHit2D.point;
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
        mesh.bounds = new Bounds(origin, Vector3.one * 50f);

        mesh.RecalculateNormals();
    }

    public void SetOrigin(Vector3 origin) {
        this.origin = origin;
    }

    /*
    public void SetAimDirection(Vector3 aimDirection) {
        startingAngle = GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }

    public void SetViewDistance(float viewDistance) {
        this.viewDistance = viewDistance;
    }
    */

    private Vector3 GetVectorFromAngle(float angle) {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) {
            n += 360;
        }
        return n;
    }
}
