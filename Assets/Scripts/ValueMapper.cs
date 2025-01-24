using UnityEngine;

public class ValueMapper : MonoBehaviour
{
    // Map a value from one range to another
    public static float MapValue(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        // Ensure the input range isn't zero to prevent division by zero
        if (Mathf.Approximately(fromMax - fromMin, 0))
        {
            Debug.LogWarning("fromMax and fromMin are too close or equal, returning toMin.");
            return toMin;
        }

        // Calculate the mapped value
        return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
    }

    // Example usage
    void Start()
    {
        float value = 5f; // Input value
        float fromMin = 0f, fromMax = 10f;
        float toMin = 0f, toMax = 100f;

        float mappedValue = MapValue(value, fromMin, fromMax, toMin, toMax);
        Debug.Log($"Mapped Value: {mappedValue}"); // Output: Mapped Value: 50
    }
}
