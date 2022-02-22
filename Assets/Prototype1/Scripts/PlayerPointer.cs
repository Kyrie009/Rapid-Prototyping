using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype1
{
    public class PlayerPointer : MonoBehaviour
    {
        private Vector3 offset;

        private GameObject focalPoint;
        private GameObject player;
        void Start()
        {
            player = GameObject.Find("Player");
            focalPoint = GameObject.Find("Focal Point");
            //pointer offset to player
            offset = transform.position - player.transform.position;
        }

        void Update()
        {
            //Pointer follows the player
            Vector3 newPos = player.transform.position + offset;
            transform.position = newPos;
            //Get Direction of focal point
            transform.forward = focalPoint.transform.forward;
        }
    }

}

