using UnityEngine;
using UnityEngine.InputSystem;

public class SquareSpawner : MonoBehaviour
{
    public Camera gameCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldMousePosition = gameCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector2 mousePosition = worldMousePosition;

        //Debug.Log(mousePosition);


    }
}
