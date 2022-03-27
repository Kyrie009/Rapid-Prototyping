using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype3
{
    public class GrapplingHook : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private CharacterController controller;
        [SerializeField] private Transform playerbody;
        [SerializeField] private Transform grapplingHook;
        [SerializeField] private Transform grapplingHookEndPoint;
        [SerializeField] private Transform grapplingHookInitialPoint;
        [SerializeField] private Transform handpos;
        [SerializeField] private LayerMask grappleLayer;
        [SerializeField] private float maxGrappleDistance;
        [SerializeField] private float hookSpeed;
        [SerializeField] private float grappleSpeed;
        [SerializeField] private Vector3 offset;

        private bool isShooting, isGrappling;
        private Vector3 hookPoint;

        private void Start()
        {
            isShooting = false;
            isGrappling = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShootHook();
            }

            if (grapplingHook.parent == handpos)
            {
                grapplingHook.localPosition = grapplingHookInitialPoint.localPosition;
                grapplingHook.localRotation = grapplingHookInitialPoint.localRotation;
            }

            if (isGrappling) //grapples player
            {
                grapplingHook.position = Vector3.Lerp(grapplingHook.position, hookPoint, hookSpeed * Time.deltaTime); //shoots the hook
                if (Vector3.Distance(grapplingHook.position, hookPoint) < 0.5f) //checks if hook hits the hook point
                {
                    controller.enabled = false; // disable controller to disable gravity
                    playerbody.position = Vector3.Lerp(playerbody.position, hookPoint - offset, grappleSpeed * Time.deltaTime); //start moving player to position with lerp instead on instant teleport.
                    if (Vector3.Distance(playerbody.position, hookPoint - offset) < 0.5f) //check if player reack hook point, then drop player
                    {
                        controller.enabled = true;
                        isGrappling = false;
                        grapplingHook.SetParent(handpos);
                        lineRenderer.enabled = false;
                    }
                }

            }
        }

        private void LateUpdate()
        {
            if (lineRenderer.enabled) //visualise the grappling hook rope
            {
                lineRenderer.SetPosition(0, grapplingHookEndPoint.position);
                lineRenderer.SetPosition(1, grapplingHookInitialPoint.position);

            }
        }

        void ShootHook()
        {
            if (isShooting || isGrappling) return; //check if already shooting the grappling, if so don't do it

            isShooting = true; // if we arent shooting then shoot
            RaycastHit hit; //shoot a ray at whatever you want to graple at
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // need to convert this to third person
            if (Physics.Raycast(ray, out hit, maxGrappleDistance, grappleLayer))
            {
                hookPoint = hit.point; // if ray hits what we want then set the hook point to that point
                isGrappling = true; //start grappling to that point
                grapplingHook.parent = null;
                grapplingHook.LookAt(hookPoint); // looks at the hook point
                lineRenderer.enabled = true; //draws hook line

            }
            isShooting = false;

        }
    }

}
