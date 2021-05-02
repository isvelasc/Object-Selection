using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{

    [SerializeField] public PlayerController controller; 

    bool isHolding;
    bool Snapped;

    SnapLocation snapReference;

    // Start is called before the first frame update
    void Start()
    {
        snapReference = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Get reference to closest snap-node
        // Detect whehter any object has snapped to it
        if (snapReference == null)
        {
            snapReference = other.gameObject.GetComponent<SnapLocation>();
        }
        Snapped = snapReference.GetComponent<SnapLocation>().hasSnapped;
    }

    // Update is called once per frame
    void Update()
    {

        // An indirect reference of the object to itself 
        isHolding = controller.hasObject;

        // Object Kinematic after being snapped
        if (Snapped)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        // Object can still be grabbed
        if (!isHolding && !Snapped)
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    
}
