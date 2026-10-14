using UnityEngine;

public class Missile : MonoBehaviour
{
    public Transform target;

    private Vector3 targetVector;
    public GameObject enemyShips;
    public float speed;

    public float turnSpeed;

    public float missileLifespan;

    private float missileTime;

    private float circlePoints = 10;

    public float missileHitboxRadius;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        drawRadius();
        missileDetector();

        if(target != null)
        {
            targetVector = target.position;
        }

     

        Vector3 directionToTarget = (targetVector - transform.position).normalized;
            
        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;

        float deltaAngle = Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle);

        float rotateDirection = Mathf.Sign(deltaAngle);

        float angleStep = turnSpeed * rotateDirection * Time.deltaTime;

        if (Mathf.Abs(deltaAngle) <= angleStep)
        {
            transform.Rotate(0f, 0f, deltaAngle);
        }
        else
        {
            transform.Rotate(0f, 0f, angleStep);
        }

        transform.position += transform.up * Time.deltaTime * speed;

        missileTime += Time.deltaTime;

        if (missileTime >= missileLifespan)
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
                new Vector3(Mathf.Cos(degreeFirst) * missileHitboxRadius, Mathf.Sin(degreeFirst) * missileHitboxRadius, 0) + transform.position, //POS 1
                new Vector3(Mathf.Cos(degreeSecond) * missileHitboxRadius, Mathf.Sin(degreeSecond) * missileHitboxRadius, 0) + transform.position, //POS 2
                Color.purple); // color

        }

    }

    void missileDetector()
    {

        for (int i = 0; i < enemyShips.GetComponent<ShipSpawner>().spawnedEnemies.Count; i++)
        {
            if (enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i] == null) { continue; }
            Vector3 enemyPosition = enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].transform.position;

            float distance = Vector3.Magnitude(enemyPosition - transform.position);

            if (distance < missileHitboxRadius + enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].GetComponent<Enemy>().hitboxRadius)
            {
                Destroy(gameObject);
                enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].GetComponent<Enemy>().currentHealth -= 5;
            }

        }

    }

}
