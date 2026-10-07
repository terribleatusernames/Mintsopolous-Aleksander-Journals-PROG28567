using UnityEngine;
using UnityEngine.InputSystem;

public class DotProductExercise : MonoBehaviour
{
    public float redAngle = 0;
    public float blueAngle = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 redVector = new Vector2(Mathf.Cos(redAngle * Mathf.Deg2Rad), Mathf.Sin(redAngle * Mathf.Deg2Rad));

        Debug.DrawLine(Vector2.zero, redVector, Color.red);

        Vector2 blueVector = new Vector2(Mathf.Cos(blueAngle * Mathf.Deg2Rad), Mathf.Sin(blueAngle * Mathf.Deg2Rad));

        Debug.DrawLine(Vector2.zero, blueVector, Color.blue);


        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            float dotProduct = Vector2.Dot(redVector, blueVector);

            if (dotProduct <= 0.1f) {

                dotProduct = 0;

            }

            Debug.Log(dotProduct);
        }
    }
}
