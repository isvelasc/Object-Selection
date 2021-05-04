using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{

    [SerializeField] public PlayerController controller;
    public GameObject hover;
    GameObject newHover;
    public SnapLocation grid;

    public bool isHolding;
    public bool inGrid;
    public bool placed;
    public bool hovering;
    bool Snapped;
    public float z_offset;
    public float x_offset;

    // Start is called before the first frame update
    void Start() {
        hovering = false;
    }

    // Update is called once per frame
    void Update()
    {

        insideGrid();

        // An indirect reference of the object to itself 
        isHolding = controller.hasObject;

        kinematic();

        // If the object is in the grid and not being held by anything
        //  it needs to snap to a location
        if (inGrid && !placed)
        {
            snap();
        }

    }

    // This function checks if the object is inside the grid region
    void insideGrid() {
      
        if (grid.GetComponent<Collider>().bounds.Contains(transform.position))
        {
            inGrid = true;
        }
        else
        {
            inGrid = false;
        }
    }

    void kinematic()
    {
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

    void snap()
    {
        var vec = transform.eulerAngles;
        vec.x = 0f;
        vec.y = Mathf.Round(transform.eulerAngles.y / 90) * 90;
        Debug.Log("Vec y: " + vec.y);
        vec.z = 0f;

        if (vec.y == 0 || vec.y == 180 || vec.y == 360)
        {
            x_offset = .5f;
            z_offset = -0.1f;
        }
        else
        {
            x_offset = 0f;
            z_offset = 0.4f;
        }

        Vector3 position =
            new Vector3(
            Mathf.Round(transform.position.x) + x_offset,
            0.1f,
            Mathf.Round(transform.position.z) + z_offset
            );

        if (!isHolding)
        {
            if (hovering == true)
            {
                Destroy(newHover);
                hovering = false;
            }
            transform.position = position;
            transform.eulerAngles = vec;
            inGrid = false;
            placed = true;
            transform.GetComponent<Rigidbody>().useGravity = true;
        }

        else if (hovering == true && newHover.transform.position != position)
        {
            Destroy(newHover);
            hovering = false;
        }

        else if (hovering == false)
        {
            newHover = Instantiate(hover, position, Quaternion.Euler(vec));
            hovering = true;
        }
    }
}
