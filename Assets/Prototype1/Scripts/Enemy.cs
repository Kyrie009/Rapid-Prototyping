using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype1
{
    public class Enemy : GameBehaviour
    {
        public int scoreValue;
        public float speed;
        private Rigidbody enemyRb;
        private GameObject player;

        void Start()
        {
            //initialises references
            enemyRb = GetComponent<Rigidbody>();
            player = GameObject.Find("Player");
            //Starting values
            scoreValue = Random.Range(12, 21);
        }

        // Update is called once per frame
        void Update()
        {
            if (_UI.uiNavigation.isOn == false)
            {
                //follow player
                Vector3 lookDirection = (player.transform.position - transform.position).normalized;
                enemyRb.AddForce(lookDirection * speed);
            }
            //destroys enemy if they fall off
            if (transform.position.y < -10)
            {
                _UI.scoring.GetScore(scoreValue);
                Destroy(gameObject);
            }
        }

    }
}

