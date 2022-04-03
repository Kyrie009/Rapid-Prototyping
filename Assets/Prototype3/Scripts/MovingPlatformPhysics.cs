using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformPhysics : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MovingGround")
        {
            transform.parent = other.transform.parent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MovingGround")
        {
            transform.parent = null;
        }
    }

}
