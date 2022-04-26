using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResonanceProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int resonanceLevel = 0;
    [SerializeField] private int chargeDmgMultiplier = 100;
    //[SerializeField] private Transform hitVFX;
    private Rigidbody bulletRigidbody;
    private ResonanceInputs resonanceScript;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        resonanceScript = FindObjectOfType<ResonanceInputs>();
    }

    private void Start()
    {
        bulletRigidbody.velocity = transform.forward * speed; //bullet movement
        resonanceLevel = resonanceScript.resonanceLevel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyAI4>() != null)
        {
            other.GetComponent<EnemyAI4>().Hit(resonanceLevel * chargeDmgMultiplier);
        }
        if (other.GetComponent<MathQuestion>() != null)
        {
            other.GetComponent<MathQuestion>().EnterAnswer(resonanceLevel); // shoot answer into the math object
        }
        //Instantiate(hitVFX, transform.position, Quaternion.identity);
        Destroy(gameObject,1f);
    }

}
