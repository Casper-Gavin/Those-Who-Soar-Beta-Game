using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable] 
    private struct Prefab
    {
        public GameObject gameObject;
        public int quantity;
    }
    [Header("Objects")]
    [SerializeField] private List<Prefab> prefabsToSpawn;

    [Header("Settings")]
    [SerializeField] private int clusters;
    [SerializeField] private float separationInClusters;
    [SerializeField] private bool clusterLikeTypes;

    [Header("Boundary")]
    [SerializeField] private Vector3 pos1;
    [SerializeField] private Vector3 pos2;

    private void Start()
    {
        SpawnPrefabs();
    }

    private void SpawnPrefabs()
    {
        // use this for now, if we have problems with this we can change it
        // algo: find biggest side of any prefab and use as separation distance so
        // that overlapping prefabs are never placed

        // an alternative way is to place and check overlap with physics until success
        // but, with current algo its more efficient than physics
        // Difference might be negligible, we can switch if it doesn't seem to work well

        float separationDistance = 0.0f;

        List<GameObject> allPrefabs = new List<GameObject>();
        for (int i = 0; i < prefabsToSpawn.Count; i++)
        {
            for (int j = 0; j < prefabsToSpawn[i].quantity; j++)
            {
                // will this use a reference? we'll see... May want to make a copy here somehow
                // actually doesn't matter, since the position is set from Instantiate, not associated with gameobj
                allPrefabs.Add(prefabsToSpawn[i].gameObject);
            }

            BoxCollider2D collider2D = prefabsToSpawn[i].gameObject.GetComponent<BoxCollider2D>();
            if (collider2D)
            {
                separationDistance = Mathf.Max(separationDistance, collider2D.size.x);
                separationDistance = Mathf.Max(separationDistance, collider2D.size.y);
            }
            else
            {
                Debug.Log("collider does not exist");
            }
        }

        separationDistance += 0.10f; // add a slight amount incase the boxes are touching

        int total = allPrefabs.Count;

        List<Vector3> positionsSoFar = new List<Vector3>();
        foreach (GameObject prefab in allPrefabs)
        {
            bool valid = false;
            Vector3 position = new Vector3(0, 0, 0);
            int failures = 0;
            do 
            {
                position.x = Random.Range(pos1.x, pos2.x);
                position.y = Random.Range(pos1.y, pos2.y);
                valid = true;
                foreach (Vector3 pos in positionsSoFar)
                {
                    if (Vector3.Distance(position, pos) < separationDistance)
                    {
                        valid = false;
                        // could cycle infinitely, need an epoch limit with fallback behavior
                        failures += 1;
                        break;
                    }
                }
                if (failures >= 10)
                {
                    break;
                }
            } while(!valid);

            if (failures < 10) // if we fail to place too many times, just don't place the entity
            {
                Instantiate(prefab, transform.position + position, Quaternion.identity);
                positionsSoFar.Add(position);
            }
        }

        // clustering
        /*int numClusters = clusters;
        if (numClusters == 0)
        {
            numClusters = total;
        }

        List<List<GameObject>(clusters)> clusteredPrefabs;*/
    }

    private void OnDrawGizmos()
    {
        Vector3 position1 = transform.position + pos1;
        Vector3 position2 = transform.position + pos2;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(position1, 0.3f);
        Gizmos.DrawWireSphere(position2, 0.3f);
        Gizmos.DrawLine(position1, new Vector3(position1.x, position2.y, position1.z));
        Gizmos.DrawLine(position1, new Vector3(position2.x, position1.y, position1.z));
        Gizmos.DrawLine(position2, new Vector3(position2.x, position1.y, position1.z));
        Gizmos.DrawLine(position2, new Vector3(position1.x, position2.y, position1.z));
    }
}
