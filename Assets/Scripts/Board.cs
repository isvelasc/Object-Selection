using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    [SerializeField] public GameObject snap;
    

    // Start is called before the first frame update
    void Start()
    {
        Vector3 boardPos = transform.position;
        bool shouldOffest = false;
        float offset = 0.75f;

        float startPosx = 5f;
        float startPosz = 5f;

        for (int x = 0; x < 5; x++)
        {
            for (int z = 0; z < 5; z++)
            {
                if (!shouldOffest)
                {
                    GameObject snapObject = Instantiate(snap,
                    new Vector3(gameObject.transform.position.x + boardPos.x + startPosx, 
                            gameObject.transform.position.y, 
                            gameObject.transform.position.z + boardPos.z + startPosz),
                    Quaternion.identity);
                }
                else
                {
                    GameObject snapObject = Instantiate(snap,
                    new Vector3(gameObject.transform.position.x + boardPos.x + startPosx + offset,
                            gameObject.transform.position.y,
                            gameObject.transform.position.z + boardPos.z + startPosz + offset),
                    Quaternion.identity);
                }
                shouldOffest = true;
                startPosx -= 1f;
            }
            shouldOffest = false;
            startPosx = 5f;
            startPosz -= 1f;
        }

    }

}
