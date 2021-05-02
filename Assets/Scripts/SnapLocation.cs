using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapLocation : MonoBehaviour
{
    // Only works with highlatable objects
    // CHANGE TAG TO A MORE BROAD UNDERSTANDING OF OBJECT ENVIRONMENT
    [SerializeField] private string highlightTag = "Highlightable";
    bool isHolding;
    bool inSnapZone;

    [SerializeField] public bool hasSnapped;

    [SerializeField] public PlayerController controller;
    ObjectController currentObject;

    // Update is called once per frame
    void Update()
    {
        currentObject = controller.objectGrabbed;
        isHolding = controller.hasObject;

        Snap2Positon();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(highlightTag))
        {
            inSnapZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag(highlightTag))
        {
            inSnapZone = false;
        }
    }

    void Snap2Positon()
    {
        if (!isHolding && inSnapZone)
        {
            if (currentObject != null)
            {
                currentObject.transform.position = transform.position;
                hasSnapped = true;
            }
        }
    }
}
