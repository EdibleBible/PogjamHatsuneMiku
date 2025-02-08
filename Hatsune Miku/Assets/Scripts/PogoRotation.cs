using UnityEngine;

public class PogoRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;           // Speed for player-controlled rotation
    public float correctionSpeed = 5f;           // Speed to correct back to upright
    public float maxRotationAngle = 45f;         // Optional limit for max rotation

    private void Update()
    {
        HandleRotation();
        AutoBalance();
    }

    void HandleRotation()
    {
        // Get player input (-1 for left, +1 for right)
        float rotationInput = Input.GetAxis("Horizontal");

        if (rotationInput != 0)
        {
            // Apply rotation around the Z-axis
            float rotationAmount = -rotationInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationAmount);
        }
    }

    void AutoBalance()
    {
        // Only auto-correct when no player input is detected
        if (Mathf.Abs(Input.GetAxis("Horizontal")) < 0.01f)
        {
            float currentZRotation = transform.localEulerAngles.z;

            // Handle Unity's 0-360 wrapping issue
            if (currentZRotation > 180f)
            {
                currentZRotation -= 360f;
            }

            // Smoothly interpolate back to 0 rotation on the Z-axis
            float newZRotation = Mathf.LerpAngle(currentZRotation, 0f, correctionSpeed * Time.deltaTime);

            // Apply the corrected rotation
            transform.localRotation = Quaternion.Euler(
                transform.localRotation.eulerAngles.x,
                transform.localRotation.eulerAngles.y,
                newZRotation
            );
        }
    }
}
