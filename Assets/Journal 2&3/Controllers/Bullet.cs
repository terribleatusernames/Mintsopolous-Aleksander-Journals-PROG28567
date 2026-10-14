using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject enemyShips;

    private float circlePoints = 10;
    public float bulletHitboxRadius;

    public float bulletLifespan;

    public float bulletTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bulletDetector();   
        drawRadius();
        transform.position += transform.up * Time.deltaTime * bulletSpeed;

        bulletTimer += Time.deltaTime;

        if (bulletTimer >= bulletLifespan)
        {
            Destroy(gameObject);
        }
    }

    void drawRadius()
    {

        for (int j = 0; j <= circlePoints - 1; j++)
        {
            float degreeFirst = Mathf.PI * 2 / circlePoints * j;
            float degreeSecond = Mathf.PI * 2 / circlePoints * (j + 1);



            Debug.DrawLine(
                new Vector3(Mathf.Cos(degreeFirst) * bulletHitboxRadius, Mathf.Sin(degreeFirst) * bulletHitboxRadius, 0) + transform.position, //POS 1
                new Vector3(Mathf.Cos(degreeSecond) * bulletHitboxRadius, Mathf.Sin(degreeSecond) * bulletHitboxRadius, 0) + transform.position, //POS 2
                Color.purple); // color

        }

    }

    void bulletDetector()
    {
        
            for (int i = 0; i < enemyShips.GetComponent<ShipSpawner>().spawnedEnemies.Count; i++)
            {
                if (enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i] == null) { continue; }
                Vector3 enemyPosition = enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].transform.position;

                float distance = Vector3.Magnitude(enemyPosition - transform.position);

                if (distance < bulletHitboxRadius + enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].GetComponent<Enemy>().hitboxRadius)
                {
                    Destroy(gameObject);
                    enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].GetComponent<Enemy>().currentHealth -= 1;
                }

            }
        
    }
}