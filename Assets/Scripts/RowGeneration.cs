using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class RowGeneration : MonoBehaviour
{
    public GameObject inputBox;

    public List<Vector2> squares = new List<Vector2>();

    public int amountOfSquares = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        string amount = inputBox.GetComponent<TMP_InputField>().text;

        amountOfSquares = int.Parse(amount);

        Debug.Log(amountOfSquares);

        for (int i = 0; i < squares.Count; i++)
        {
            Debug.DrawLine((squares[i] + (Vector2.left + Vector2.up)), (squares[i] + (Vector2.right + Vector2.up)), Color.white);
            Debug.DrawLine((squares[i] + (Vector2.left + Vector2.down)), (squares[i] + (Vector2.right + Vector2.down)), Color.white);
            Debug.DrawLine((squares[i] + (Vector2.left + Vector2.up)), (squares[i] + (Vector2.left + Vector2.down)), Color.white);
            Debug.DrawLine((squares[i] + (Vector2.right + Vector2.up)), (squares[i] + (Vector2.right + Vector2.down)), Color.white);
        }

    }

    public void spawnSquares()
    {
        squares.Clear();

        if (amountOfSquares > 0)
        {
            for (int i = 0; i < amountOfSquares; i++)
            {

                squares.Add(new Vector2(i, 0));

            }
        }

    }
}
