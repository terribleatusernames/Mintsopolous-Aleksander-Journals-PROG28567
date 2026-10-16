using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;      

public class ShipSpawner : MonoBehaviour
{

    public GameObject shipPrefab;
    public Transform playerShipTansform;
    public float spawnInterval;
    public float spawnRadius;
    public float spawnTime = 0;

    public List<GameObject> spawnedEnemies = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //remove any destroyed ships from list
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (spawnedEnemies[i] == null)
            {
                spawnedEnemies.RemoveAt(i);
            }
        }

        spawnTime += Time.deltaTime;
    
        if (spawnTime >= spawnInterval)
        {
            //generate a random angle in radians between 0 and 2π
            float angle = Random.Range(0f, Mathf.PI * 2f);
            //spawn the ship at that position relative to the player ship on a circle
            Vector3 spawnPos = new Vector3(
                Mathf.Cos(angle) * spawnRadius + playerShipTansform.position.x,
                Mathf.Sin(angle) * spawnRadius + playerShipTansform.position.y,
                0f);

            //give the spawned ship the player transform and add it to list
            GameObject spawnedShip = Instantiate(shipPrefab, spawnPos, Quaternion.identity);
            spawnedShip.GetComponent<Enemy>().playerTransform = playerShipTansform;
            spawnedEnemies.Add(spawnedShip);

            spawnTime = 0f;
        }
    }
}
