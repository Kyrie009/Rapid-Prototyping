using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int atk = 100;
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
        if (other.GetComponent<EnemyAI4>() != null)
        {
            other.GetComponent<EnemyAI4>().Hit(atk);
        }
        //Instantiate(hitVFX, transform.position, Quaternion.identity);
        Destroy(gameObject, 1f);
    }
}
