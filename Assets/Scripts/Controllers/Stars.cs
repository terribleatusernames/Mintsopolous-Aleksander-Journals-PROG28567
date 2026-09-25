using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;

    private Vector3 currentPosition;
    private Vector3 startPosition;
    private Vector3 endPosition;

    public int starCount = 0;

    private bool isDrawing = false;

    private bool doneDrawing = false;

    private Coroutine drawCoroutine;

    // Start drawing when the object is enabled
    void Start()
    {
        
    }

    void Update()
    {
       if (!isDrawing) {
            isDrawing = true;
            drawCoroutine = StartCoroutine(DrawConstellation());
        }

       if(doneDrawing)
        {
            StopCoroutine(drawCoroutine);
  
            isDrawing = false;
            doneDrawing = false;
        }  
    }

    private IEnumerator DrawConstellation()
    {
   
        for (int i = 0; i < starTransforms.Count - 1; i++)
        {
            startPosition = starTransforms[i].position;
            endPosition = starTransforms[i + 1].position;
            
            float elapsed = 0f;

            while (elapsed < drawingTime)
            {
                
                currentPosition = Vector3.Lerp(startPosition, endPosition, (elapsed / drawingTime));

                Debug.DrawLine(startPosition, currentPosition, Color.white);


                elapsed += Time.deltaTime;
               
                yield return null;
            }

            yield return null;
        }

       doneDrawing = true;

    }
}
