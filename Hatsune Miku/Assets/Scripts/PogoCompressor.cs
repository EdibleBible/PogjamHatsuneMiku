using UnityEngine;
using DG.Tweening;

public class PogoCompressor : MonoBehaviour
{
    public Transform pogoStickVisual; // Visual reference for pogo stick
    public float compressedScaleY = 0.5f; // Scale when fully compressed
    public float compressionDuration = 0.2f; // Duration of the compression animation
    public float releaseDuration = 0.1f; // Duration of the release animation
    public float pogoForce = 15f;
    //public bool isGrounded;
    //public Collider groundCollider;

    private Rigidbody rb;
    private Vector3 originalScale;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PogoSurface"))
        {
            //isGrounded = true;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = pogoStickVisual.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
        // Smoothly scale down the pogo stick using DoTween
        pogoStickVisual.DOScaleY(compressedScaleY, compressionDuration)
            .SetEase(Ease.InOutSine);
    }

    void ReleasePogo()
    {
        // Smoothly scale back to the original size and add pogo bounce
        pogoStickVisual.DOScaleY(originalScale.y, releaseDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); // Reset Y velocity
                rb.AddForce(transform.up * pogoForce, ForceMode.Impulse);
            });
    }

    void LockRotationToZAxis()
    {
        // Reset any accidental rotation in X or Y
        Vector3 currentEuler = transform.localEulerAngles;
        transform.localRotation = Quaternion.Euler(0f, 0f, currentEuler.z);
    }
}
