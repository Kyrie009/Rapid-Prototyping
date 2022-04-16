using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    //[SerializeField] private Transform hitVFX;
    private Rigidbody bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        bulletRigidbody.velocity = transform.forward * speed; //bullet movement
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyShooter>() != null)
        {
            //hit target
        }
        else
        {
            //hit something else
        }
        //Instantiate(hitVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
