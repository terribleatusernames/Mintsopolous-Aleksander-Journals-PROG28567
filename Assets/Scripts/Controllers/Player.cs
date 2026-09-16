using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    public Vector3 result;

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            result = AddNumbers(Vector3.up, Vector3.up );

            StartCoroutine(spawnWithDelay(3f));

        }

        Debug.Log(transform.position);

    }

    private IEnumerator spawnWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SpawnBombAtOffset(result);
    }


    Vector3 AddNumbers(Vector3 one, Vector3 two)
    {
        return one - two;
    }

    void SpawnBombAtOffset(Vector3 inOffset)
    {
        Instantiate(bombPrefab, transform.position + inOffset, Quaternion.identity);
    }
}
