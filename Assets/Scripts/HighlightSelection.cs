using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSelection : MonoBehaviour
{
    [SerializeField] private string highlightTag = "Highlightable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] Camera fpsCamera;
    [SerializeField] PlayerController controller;

    Transform highlightObject;

   
    void Update()
    {
        CheckHighlight();
    }

    void CheckHighlight()
    {
        Renderer objectRenderer;
        Transform currentObjectSelection;
        RaycastHit hit;
        Ray mousePointer = fpsCamera.ScreenPointToRay(Input.mousePosition);

        // Reset no longer selected object to default
        if (highlightObject != null)
        {
            objectRenderer = highlightObject.GetComponent<Renderer>();
            objectRenderer.material = defaultMaterial;
            highlightObject = null;
        }

        // Initial selection 
        if (!controller.hasObject)
        {
            if (Physics.Raycast(mousePointer, out hit))
            {
                // Set object to highlight material
                
                currentObjectSelection = hit.transform;

                if (currentObjectSelection.transform.CompareTag(highlightTag) && (objectRenderer = currentObjectSelection.GetComponent<Renderer>()) != null)
                {
                    objectRenderer.material = highlightMaterial;
                    highlightObject = currentObjectSelection;
                }
            }
        }

        // Maintain highlight of selection
        if (controller.hasObject)
        {
            currentObjectSelection = controller.objectGrabbed.transform;
            objectRenderer = currentObjectSelection.GetComponent<Renderer>();
            objectRenderer.material = highlightMaterial;
            highlightObject = currentObjectSelection;
        }
    }

}

