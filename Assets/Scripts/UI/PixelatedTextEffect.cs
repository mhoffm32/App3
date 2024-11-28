using UnityEngine;

public class PixelatedObjectEffect : MonoBehaviour
{
    public float jitterAmount = 2f;
    public float jitterInterval = 0.4f;

    private int step = 0;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;

        // Add a slight random delay to desynchronize movements of multiple objects
        float randomDelay = Random.Range(0f, jitterInterval / 2f);
        InvokeRepeating(nameof(JitterObject), randomDelay, jitterInterval);
    }

    void JitterObject()
    {
        Vector3[] jitterSteps = new Vector3[]
        {
            new Vector3(-jitterAmount, jitterAmount, 0),
            new Vector3(jitterAmount, -jitterAmount, 0)
        };

        transform.localPosition = originalPosition + jitterSteps[step];
        step = (step + 1) % jitterSteps.Length;
    }

    void OnDisable()
    {
        transform.localPosition = originalPosition;
    }
}
