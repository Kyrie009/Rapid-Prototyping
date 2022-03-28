using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype3
{
    public class GrapplingHook : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private ThirdPersonController movementScript;
        [SerializeField] private CharacterController controller;
        [SerializeField] private Transform playerbody;
        [SerializeField] private Transform grapplingHook;
        [SerializeField] private Transform grapplingHookEndPoint;
        [SerializeField] private Transform grapplingHookInitialPoint;
        [SerializeField] private Transform handpos;
        [SerializeField] private LayerMask grappleLayer;
        [SerializeField] private float maxGrappleDistance;
        [SerializeField] private float hookSpeed;
        [SerializeField] private Vector3 offset;
        public Animator anim;

        private bool isShooting, isGrappling;
        private Vector3 hookPoint;

        private void Start()
        {
            isShooting = false;
            isGrappling = false;
        }

        private void Update()
        {
            ShootHook(); //hook input

            if (isGrappling) //grapples player
            {
                HandleGrapple();
            }

            if (grapplingHook.parent == handpos)
            {
                grapplingHook.localPosition = grapplingHookInitialPoint.localPosition;
                grapplingHook.localRotation = grapplingHookInitialPoint.localRotation;
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

        public void ShootHook()
        {
            if (CheckInputDownHookshot()) //hookshot input
            {
                if (isShooting || isGrappling) return; //check if already shooting the grappling, if so don't do it

                isShooting = true; // if we arent shooting then shoot
                //shoot raycast to where you are looking at
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, maxGrappleDistance, grappleLayer))
                {
                    //Hit something
                    movementScript.GetHookState();
                    hookPoint = hit.point;
                    isGrappling = true; //start grappling to that point
                    grapplingHook.parent = null; // removes hook from parent
                    grapplingHook.LookAt(hookPoint); // looks at the hook point
                    playerbody.LookAt(hookPoint);
                    lineRenderer.enabled = true; //draws hook line                   
                }
                isShooting = false;
            }
        }

        private void HandleGrapple()
        {
            //These variables are used to calculate how the caracter is grappled
            Vector3 hookshotDir = (hookPoint - transform.position).normalized;
            float minGrappleSpeed = 10f;
            float maxGrappleSpeed = 40f;
            float hookshotSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hookPoint), minGrappleSpeed, maxGrappleSpeed);
            float SpeedMultiplier = 1f;

            grapplingHook.position = Vector3.Lerp(grapplingHook.position, hookPoint, hookSpeed * Time.deltaTime); //shoots the hook
            if (Vector3.Distance(grapplingHook.position, hookPoint) < 0.5f) //checks if hook hits the hook point
            {
                // Grapples the player via move function                                                                		
                controller.Move(hookshotDir * hookshotSpeed * SpeedMultiplier * Time.deltaTime);
                float hookShotDistance = 1f;
                if (Vector3.Distance(playerbody.transform.position, hookPoint - offset) < hookShotDistance) //Check if player has reached Hookshot position
                {
                    ResetHook();
                }
                //Cancel Hookshot during grapple
                if (CheckInputDownHookshot())
                {
                    ResetHook();
                }
                // Cancel Hookshot with jump (its not an actual jump but emulates the feel of jumping out of the hook)
                if (movementScript.CheckInputJump())
                {
                    //float momentumSpeedBoost = 7f;
                    //characterVelocityMomentum = hookshotDir * hookshotSpeed * momentumSpeedBoost;
                    ResetHook();
                }
            }          
        }

        //Resets the hook and returns player to normal movement state
        public void ResetHook()
        {
            movementScript.ReturnToNormalState();
            isGrappling = false;
            grapplingHook.SetParent(handpos); //reparents the hook to the hand
            lineRenderer.enabled = false;
        }
        private bool CheckInputDownHookshot()
        {
            return Input.GetMouseButtonDown(0);
        }

    }

}
