using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerUp : MonoBehaviour
{
    public GameObject prefab;

    private int powerUpCount = 5;

    private float spawnRadius = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            for (int j = 0; j < powerUpCount; j++)
            {
                float degreeFirst = 2 * Mathf.PI / powerUpCount * j;

                Instantiate(prefab, new Vector3(Mathf.Cos(degreeFirst) * spawnRadius, Mathf.Sin(degreeFirst) * spawnRadius, 0) + transform.position, Quaternion.identity);
            }
        }
    }
}
