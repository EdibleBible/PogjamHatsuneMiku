using UnityEngine;

public class PogoController : MonoBehaviour
{
    public CapsuleCollider pogoCollider;  // Assign the capsule collider in the inspector
    public float minHeight = 0.5f;        // Minimum height for compression
    public float maxHeight = 2.0f;        // Maximum height when extended
    public float compressionSpeed = 2.0f; // Speed of shrinking/expanding
    public float pogoForce = 15f;         // Base bounce force

    private Rigidbody rb;
    private bool isCompressing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            CompressPogo();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ReleasePogo();
        }
    }

    void CompressPogo()
    {
        // Gradually compress the pogo stick
        pogoCollider.height = Mathf.Lerp(pogoCollider.height, minHeight, Time.deltaTime * compressionSpeed);
    }

    void ReleasePogo()
    {
        // Release the pogo, extend the collider, and apply an upward force
        pogoCollider.height = maxHeight;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); // Reset Y velocity
        float releaseForce = pogoForce * Mathf.InverseLerp(minHeight, maxHeight, pogoCollider.height);
        rb.AddForce(Vector3.up * releaseForce, ForceMode.Impulse);
    }
}
