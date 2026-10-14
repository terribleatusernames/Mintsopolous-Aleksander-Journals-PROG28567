using UnityEngine;

public class CameraNoRotate : MonoBehaviour
{
    public Transform player;
    public Vector3 vectorPlayerPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       vectorPlayerPos = player.position;
        vectorPlayerPos.z = -10;

        transform.position = vectorPlayerPos;

    }
}
