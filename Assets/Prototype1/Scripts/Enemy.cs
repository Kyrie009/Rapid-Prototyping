using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype1
{
    public class Enemy : MonoBehaviour
    {
        public float speed;
        private Rigidbody enemyRb;
        private GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            //initialises references
            enemyRb = GetComponent<Rigidbody>();
            player = GameObject.Find("Player");

        }

        // Update is called once per frame
        void Update()
        {
            //follow player
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed); 

            //destroys enemy if they fall off
            if(transform.position.y < -10)
            {
                Destroy(gameObject);
            }
        }
    }
}

