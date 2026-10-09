using UnityEngine;

public class PlanetOrbiter : MonoBehaviour
{

    public int orbitRadius = 5;

    public int orbitTime = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float degreeFirst = (2 * Mathf.PI / orbitTime * Time.time) % (2 * Mathf.PI);
        transform.position = new Vector3(Mathf.Cos(degreeFirst) * orbitRadius, Mathf.Sin(degreeFirst) * orbitRadius, 0) + transform.parent.position;
    }
}
