using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public Transform playerStart;
    public GameObject towerPrefab;

    void Start()
    {
        Instantiate(towerPrefab, playerStart.position, playerStart.rotation);
    }
}
