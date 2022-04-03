using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype3
{
    public class RotateScript : MonoBehaviour
    {
        public float xRotation; //rotates vertically
        public float yRotation; //rotates horizontally
        public float zRotation; //rotates object like a barrel roll
        public float lerpDuration = 0.5f; // duration of the rotate
        private bool isRotating = false;

        private void Update()
        {
            if (isRotating)
            {
                isRotating = false; //immediately set to false or else the function will be called multiple times which messes up the rotation
                StartCoroutine(GetRotation());
            }
        }

        IEnumerator GetRotation()
        {
            float timeElapsed = 0;
            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = transform.rotation * Quaternion.Euler(xRotation, yRotation, zRotation); // multiply with quaternion eular angle to add angle amount
            while (timeElapsed < lerpDuration) //Maintains the rotation within the while loop
            {
                //slerps the rotation instead of an instant change
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            transform.rotation = targetRotation; // ensures the rotation finishes at exactly the right angle.       
        }
        //A universal trigger for buttons to use remotely.
        public void RotateObject()
        {
            isRotating = true;
        }
    }
}

