using UnityEngine;

public class Rotater : MonoBehaviour
{
    int rotateSpeed = 60;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed*Time.deltaTime, 0);
    }
}
