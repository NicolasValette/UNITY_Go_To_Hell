using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glowing : MonoBehaviour
{
    // Start is called before the first frame update

    private int _intensity;
    private Color _actualColor;
    private Material material;

    [Tooltip("Minimum random dice intensity")]
    [SerializeField]
    private float minIntensity = -1f;
    [Tooltip("Maximum random dice intensity")]
    [SerializeField]
    private float maxIntensity = 2f;
    [Tooltip("How much to smooth out the randomness; lower values = sparks, higher = lantern")]
    [SerializeField]
    [Range(1, 200)]
    private int smoothing = 200;

    // Continuous average calculation via FIFO queue
    // Saves us iterating every time we update, we just change by the delta
    Queue<float> smoothQueue;
    float lastSum = 0;


    void Start()
    {
        smoothQueue = new Queue<float>(smoothing);

        var meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        _actualColor = material.GetColor("_EmissionColor");
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (material == null)
            return;

        // pop off an item if too big
        while (smoothQueue.Count >= smoothing)
        {
            lastSum -= smoothQueue.Dequeue();
        }

        // Generate random new item, calculate new average
        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        // Calculate new smoothed average
        //light.intensity = lastSum / (float)smoothQueue.Count;
        material.SetColor("_EmissionColor", _actualColor * (lastSum / (float)smoothQueue.Count));
    }
}
