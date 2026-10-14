using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float spawnInterval;

    float spawnTime;
    public GameObject bulletPrefab;
    public List<GameObject> bullets = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            if (bullets[i] == null)
            {
                bullets.RemoveAt(i);
            }
        }

        spawnTime += Time.deltaTime;

        if (spawnTime >= spawnInterval)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullets.Add(bullet);
            spawnTime = 0f;
        }
    }
}
