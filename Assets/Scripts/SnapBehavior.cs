using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapBehavior : MonoBehaviour
{
    [SerializeField] private Vector3 grid = Vector3.one;
    [SerializeField] PlayerController controller;

    private void Update()
    {
        SnapToGrid();
    }


    // Currently not working, further investigation needed if parent object is a necesecity for snapping
    /*
     TODO: 
            * Reference location
            * Stop snapping if gone to far from refernce location
     */
    private void SnapToGrid()
    {
        if (controller.isGrounded)
        {
            var snappedPosition = new Vector3(
                Mathf.RoundToInt(transform.position.x / this.grid.x) * this.grid.x,
                Mathf.RoundToInt(transform.position.y / this.grid.y) * this.grid.y,
                Mathf.RoundToInt(transform.position.z / this.grid.z) * this.grid.z
                );

            transform.position = snappedPosition;
        }
    }

}
