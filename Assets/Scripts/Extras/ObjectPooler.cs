using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private bool poolCanExpand = true;

    private List<GameObject> poolObjects;
    private GameObject parentObject;
    
    private void Start() {
        parentObject = new GameObject("Pool");
        Refill();
    }

    public void Refill() {
        poolObjects = new List<GameObject>();

        for (int i = 0; i < poolSize; i++) {
            AddObjectToPool();
        }
    }

    public GameObject GetObjectFromPool() {
        for (int i = 0; i < poolObjects.Count; i++) {
            if (!poolObjects[i].activeInHierarchy) {
                return poolObjects[i];
            }
        }

        if (poolCanExpand) {
            return AddObjectToPool();
        }

        return null;
    }

    public GameObject AddObjectToPool() {
        GameObject newObject = Instantiate(objectPrefab);
        newObject.SetActive(false);
        newObject.transform.parent = parentObject.transform;

        poolObjects.Add(newObject);

        return newObject;
    }
}
