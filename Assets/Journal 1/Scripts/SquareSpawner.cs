using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class SquareSpawner : MonoBehaviour
{
    public Camera gameCamera;
    public Vector3 worldMousePosition = new Vector3(); 
    public Vector2 mousePosition = new Vector2();

    public float boxSize= 1f;

    public Color white = new Color(1f, 1f, 1f, .4f);


    public List<Vector2> squares = new List<Vector2>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        worldMousePosition = gameCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        mousePosition = worldMousePosition;

        //Debug.Log(mousePosition);

        float scrollValue = Mouse.current.scroll.ReadValue().y; 

        boxSize = boxSize + scrollValue;

        drawCursorBox();

        for (int i = 0; i < squares.Count; i++)
        {
            Debug.DrawLine((squares[i] + ((Vector2.left + Vector2.up) * boxSize)), (squares[i] + ((Vector2.right + Vector2.up) * boxSize)), Color.white);
            Debug.DrawLine((squares[i] + ((Vector2.left + Vector2.down) * boxSize)), (squares[i] + ((Vector2.right + Vector2.down) * boxSize)), Color.white);
            Debug.DrawLine((squares[i] + ((Vector2.left + Vector2.up) * boxSize)), (squares[i] + ((Vector2.left + Vector2.down) * boxSize)), Color.white);
            Debug.DrawLine((squares[i] + ((Vector2.right + Vector2.up) * boxSize)), (squares[i] + ((Vector2.right + Vector2.down) * boxSize)), Color.white);
        }


        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            placeBox();
        }

    }

    void drawCursorBox()
    {
        Debug.DrawLine ((mousePosition + ((Vector2.left + Vector2.up) * boxSize)), (mousePosition + ((Vector2.right + Vector2.up) * boxSize)), white);
        Debug.DrawLine ((mousePosition + ((Vector2.left + Vector2.down) * boxSize)), (mousePosition + ((Vector2.right + Vector2.down) * boxSize)), white);
        Debug.DrawLine ((mousePosition + ((Vector2.left + Vector2.up) * boxSize)), (mousePosition + ((Vector2.left + Vector2.down) * boxSize)), white);
        Debug.DrawLine ((mousePosition + ((Vector2.right + Vector2.up) * boxSize)), (mousePosition + ((Vector2.right + Vector2.down) * boxSize)), white);

    }

    void placeBox()
    {
        squares.Add(mousePosition);
    }


}
