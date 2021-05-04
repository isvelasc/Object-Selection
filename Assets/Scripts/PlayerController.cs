using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    
    public ObjectController objectGrabbed;
    [SerializeField] Transform orientation;
    [SerializeField] Transform cam;
    [SerializeField] Camera FPCam;
    [SerializeField] public bool hasObject;

    float objectSize;
    public bool inGrid;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] public bool isGrounded;
    float groundDistance = 0.1f;

    // 2nd Iteration

    /*
     TODO : 
            * Use boxraycast from selected object to check surroundings, adjust to surroundings appropriately (optional, turn rigidbody off while 
                in selection mode to ensure no collision between objects and get funky interactions)
            * Check on release there is no overlapping (use physics compute penetration)
            
     */

    
    private void Update()
    {
        
        if (objectGrabbed != null)
            isGrounded = Physics.CheckBox(objectGrabbed.transform.position, new Vector3(objectGrabbed.GetComponent<Renderer>().bounds.size.x, objectGrabbed.GetComponent<Renderer>().bounds.size.y, objectGrabbed.GetComponent<Renderer>().bounds.size.z));

        MoveObject();

        if (Input.GetMouseButtonDown(1))
        {
            objectGrabbed = null;
            hasObject = false;
        }
    }


    // Add offset to be able to use Raycast from FP-View or add logic to ObjectController to use Raycast
    void MoveObject()
    {
        RaycastHit hit;
        Ray mousePointer = FPCam.ScreenPointToRay(Input.mousePosition);

        if (objectGrabbed != null && hasObject)
        {
            objectGrabbed.transform.position = FPCam.transform.position + FPCam.transform.forward * objectSize;
            objectGrabbed.transform.localEulerAngles = orientation.transform.localEulerAngles;
            GroundControl();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(mousePointer, out hit))
                {
                    if (hit.transform.gameObject.GetComponent<ObjectController>() != null)
                    {
                        objectGrabbed = hit.transform.gameObject.GetComponent<ObjectController>();
                        objectSize = objectGrabbed.GetComponent<Renderer>().bounds.size.magnitude;
                        hasObject = true;
                        objectGrabbed.placed = false;
                        objectGrabbed.GetComponent<Rigidbody>().useGravity = false;
                        if (inGrid)
                        {
                            objectGrabbed.inGrid = true;
                        }
                    }
                }
            }
        }
    }

    void GroundControl()
    {
        // Update Vector to receive raycast from box and check for height (use bottom of box)
        Vector3 fromGroundPostion = new Vector3(FPCam.transform.position.x + FPCam.transform.forward.x * objectSize, FPCam.transform.position.y + FPCam.transform.forward.y * objectSize - objectSize / 2.0f, FPCam.transform.position.z + FPCam.transform.forward.z * objectSize);
        if (fromGroundPostion.y > groundDistance)
            isGrounded = false;
        else if (isGrounded)
        {
            objectGrabbed.transform.position = new Vector3(FPCam.transform.position.x + FPCam.transform.forward.x * objectSize, objectSize / 3.0f, FPCam.transform.position.z + FPCam.transform.forward.z * objectSize);
        }
    }

}
