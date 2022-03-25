using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class MovementTypes : MonoBehaviour
    {
        [Header("Parameters")]
        public float dashSpeed;
        public float dashTime;

        ThirdPersonController moveScript;
        private StarterAssetsInputs _input;
        private CharacterController _controller;
        private Animator _animator;

        private void Start()
        {
            moveScript = GetComponent<ThirdPersonController>();
            _input = GetComponent<StarterAssetsInputs>();
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(2) && moveScript.Grounded)
            {
                _animator.SetTrigger("Dodge");
            }          
        }
    }

}

