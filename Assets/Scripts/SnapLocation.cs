using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapLocation : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

    }

    /* Note, we don't have to wait for the object to be dropped 
     *  in order to do the collision things. */
    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnCollisionExit(Collision collision)
    {
    }
}
