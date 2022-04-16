using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Prototype3; //this the name space for the starter assents are part off...
using UnityEngine.Animations.Rigging;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private Transform bulletProjectilePrefab;
    //[SerializeField] private Transform hitVFX; //For hiscan method
    [SerializeField] private GameObject aimRig;
    [SerializeField] private GameObject aimTarget;
    [SerializeField] private Rig rig;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs _Inputs;
    private Animator animator;
    

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        _Inputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        aimTarget = GameObject.Find("AimTarget");
        aimRig = GameObject.Find("Rig 2");
        rig = aimRig.GetComponent<Rig>();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero; //zeros out position of the mouseworld position if raycast hits nothing
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f); //this gets the center position of the screen
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint); //since always aim at center we use this instead of getting Input.mousePosition
                                                                   //Transform hitTransform = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 2000f, aimColliderLayerMask))
        {
            aimTarget.transform.position = raycastHit.point; //Set Rig aim towards target
            debugTransform.position = raycastHit.point; //used to debug the raycast pointer
            mouseWorldPosition = raycastHit.point; //the mouse world position is set to the hit point of the raycast
            //hitTransform = raycastHit.transform;
        }

        if (_Inputs.aim) //when aiming
        {
            rig.weight = 1f;
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f)); //set animation to layer on 2 index and lerp the weight transition so it is not instant

            Vector3 worldAimTarget = mouseWorldPosition; //world aim target is used to get the hitpoint when aiming
                                                         //Gets character to look at the hitpoint while standing upright
            worldAimTarget.y = transform.position.y; //used to get the left and right rotation for the character to face hitpoint without rotating the character up or down.
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized; // gets the direction player should face when they aim

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f); //rotates the player to target aiming direction

        }
        else //when not aiming
        {
            rig.weight = 0f;
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }

        if (_Inputs.shoot)
        {
            /*if (hitTransform != null)
            {
                if (other.GetComponent<EnemyShooter>() != null)
                {
                    //hit target
                }
                else
                {
                    //hit something else
                }
                Instantiate(hitVFX, transform.position, Quaternion.identity);
            }*/
            Vector3 aimDir = (mouseWorldPosition - firingPoint.position).normalized; //gets aim direction of the firing point instead of player
            Instantiate(bulletProjectilePrefab, firingPoint.position, Quaternion.LookRotation(aimDir, Vector3.up)); //now we want the full rotation in all angles for th bullet to spawn
            _Inputs.shoot = false; //prevent from constantly spawning pullets when you only need 1 per click
            CinemachineShake.Instance.ShakeCamera(0.2f, .1f); //shake camera when shooting
        }
    }
}

