using UnityEngine;

public class test : MonoBehaviour
    
{
    public Vector3 savedLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            savedLocation = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = savedLocation;
    }
}
