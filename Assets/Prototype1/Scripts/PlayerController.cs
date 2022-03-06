using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype1
{
    public class PlayerController : GameBehaviour
    {
        private Vector3 offset;
        public bool hasPowerup;
        private float powerupstrength = 20;
        public float speed;
        public float forwardInput;

        public GameObject powerupIndicator;
        private Rigidbody playerRb;
        private GameObject focalPoint;
        // Start is called before the first frame update
        void Start()
        {
            playerRb = GetComponent<Rigidbody>();
            focalPoint = GameObject.Find("Focal Point");
            offset = powerupIndicator.transform.position - transform.position;
        }

        void Update()
        {
            if(_UI.uiNavigation.isOn == false)
            {
                //input
                forwardInput = Input.GetAxis("Vertical");
                playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
            }         
            //power up indicator position
            powerupIndicator.transform.position = transform.position + offset;

            //GameOver if you fall off
            if (transform.position.y < -10)
            {
                _UI.uiNavigation.ToggleGameOver(true);
                _UI.scoring.GameOver();
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Powerup"))
            {
                hasPowerup = true;
                powerupIndicator.gameObject.SetActive(true);
                Destroy(other.gameObject);
                StartCoroutine(PowerupCountdownRoutine());
                
            }
        }
        //coroutine for how long the powerup lingers
        IEnumerator PowerupCountdownRoutine()
        {
            yield return new WaitForSeconds(7);
            powerupIndicator.gameObject.SetActive(false);
            hasPowerup = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Knockback enemy if have powerup
            if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
            {
                Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayfromPlayer = (collision.gameObject.transform.position - transform.position);
                enemyRigidbody.AddForce(awayfromPlayer * powerupstrength, ForceMode.Impulse);
                //Debug.Log(collision.gameObject.name);
            }

        }
    }

}

