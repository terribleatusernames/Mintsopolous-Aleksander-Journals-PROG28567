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

        // Set the target target's last known position 
        if (target != null)
        {
            targetVector = target.position;
        }


        // Calculate the direction to the target and the angle to rotate towards it
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

        //move the missile forward in the direction it is facing
        transform.position += transform.up * Time.deltaTime * speed;

        missileTime += Time.deltaTime;

        //delete missile if it has been alive for too long
        if (missileTime >= missileLifespan)
        {
            Destroy(gameObject);
        }

    }

    void drawRadius()
    {
        //draws a circle around the bullet to show its hitbox radius
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
        //detects if the bullet has hit an enemy ship
        for (int i = 0; i < enemyShips.GetComponent<ShipSpawner>().spawnedEnemies.Count; i++)
        {
            if (enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i] == null) { continue; }//skip if the enemy ship has been destroyed
            Vector3 enemyPosition = enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].transform.position;

            float distance = Vector3.Magnitude(enemyPosition - transform.position);

            //check if the distance between the bullet and the enemy ship is less than the sum of their hitbox radii
            if (distance < missileHitboxRadius + enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].GetComponent<Enemy>().hitboxRadius)
            {
                Destroy(gameObject);
                //remove 5 health from the enemy ship hit
                enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].GetComponent<Enemy>().currentHealth -= 5;
            }

        }

    }

}
