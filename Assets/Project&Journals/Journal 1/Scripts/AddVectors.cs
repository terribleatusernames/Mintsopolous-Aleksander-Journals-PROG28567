using UnityEngine;
using UnityEngine.InputSystem;

public class AddVectors : MonoBehaviour
{
    public Transform redTransform;
    public Transform blueTransform;

    void Update()
    {
        // Exercise: Visualizing Vector Addition
        // Create a new Vector2 called "rPlusB" which adds rTransform's position to bTransform's position.​
        Vector2 rPlusB = redTransform.position + blueTransform.position;
        Vector2 bplusR = blueTransform.position + redTransform.position;
        // When the B key is held down, draw a blue line from the origin to bTransform's position.​
        // When the R key is held down, draw a red line from the origin to rTransform's position.​
        // When the R and B keys are both held down, draw a magenta from the origin to rPlusB.​
        if (Keyboard.current.rKey.isPressed && Keyboard.current.bKey.isPressed)
        {

            Debug.DrawLine(Vector2.zero, blueTransform.position, Color.blue);

        }

        else if (Keyboard.current.bKey.isPressed)
        {

            Debug.DrawLine(Vector2.zero, redTransform.position, Color.red);

        }

        else if (Keyboard.current.rKey.isPressed)
        {

            Debug.DrawLine(Vector2.zero, rPlusB, Color.magenta);

        }
        // Exercise: Calculating Magnitude
        // Calculate the magnitude of bPlusR using the mathematical formula for calculating the length of a vector – Pythagorean Theroem.​
        // Output the magnitude to the console using either Debug.Log or print.
        float magnitude = Mathf.Sqrt(Mathf.Pow(bplusR.x, 2) + Mathf.Pow(bplusR.y, 2));
        //Debug.Log(magnitude);
    }
}
