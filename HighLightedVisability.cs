using UnityEngine;

public class HighLightedVisability : MonoBehaviour
{
    public bool isHighlighted = false;

    private MeshRenderer meshRenderer;

    [Header("Materials")]
    public Material normalMat;
    public Material highlightMat;

    void Start()
    {

        isHighlighted = false; 
        meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer == null)
        {
            Debug.LogError("MeshRenderer not found on object!");
        }

        if (normalMat == null)
        {
            Debug.LogError("normalMat not assigned!");
        }

        if (highlightMat == null)
        {
            Debug.LogError("highlightMat not assigned!");
        }
    }

    void Update()
    {
        UpdateHighlight();
    }

    void UpdateHighlight()
    {
        if (isHighlighted)
        {
            meshRenderer.materials = new Material[] { normalMat, highlightMat };
        }
        else
        {
            meshRenderer.materials = new Material[] { normalMat };
        }
    }
}
