
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.InputSystem;  
public class Pipeline : MonoBehaviour
{
    public List<Vector2> positions = new List<Vector2>();
    public Camera gameCamera;
    public float magnitude = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            float time = Time.deltaTime;

            if (time < .1f)
            {
                time = 0;

                positions.Add(gameCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()));

            }

            // Only draw lines if we have at least two positions
            if (positions.Count >= 2)
            {
                for (int i = 0; i < positions.Count - 1; i++)
                {
                    Debug.DrawLine(positions[i], positions[i + 1], Color.red);

                    magnitude += Mathf.Sqrt(Mathf.Pow(positions[i + 1].x - positions[i].x, 2) + Mathf.Pow(positions[i + 1].y - positions[i].y, 2));
                }
            }
        }
      
        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {

            Debug.Log(magnitude);

            magnitude = 0f;    
            
            positions.Clear();
          
        }

        

        
    }
}
