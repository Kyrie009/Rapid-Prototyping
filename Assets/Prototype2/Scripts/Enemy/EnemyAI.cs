using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Prototype2
{
    public class EnemyAI : GameBehaviour
    {
        //Stats
        public EnemyData enemyData;
        string enemyName;
        int health;
        int attack;
        //Components
        public Transform transf;
        public GameObject hitbox;
        //Navigation
        public NavMeshAgent agent;
        public Transform player;
        public LayerMask whatIsGround, whatIsPlayer;
        private bool destinationTimeOut = false;
      
        //Patrolling
        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange = 30f;

        //Attacking
        public float timeBetweenAttacks = 1.6f;
        bool alreadyAttacked;
        int attackCombo;

        //States
        public float sightRange, attackrange;
        public bool playerInSightRange, playerInAttackRange;

        //Animation
        public Animator anim;
        

        private void Awake()
        {
            anim = GetComponent<Animator>();
            player = GameObject.Find("P05_Aki_N").transform;
            agent = GetComponent<NavMeshAgent>();
            transf = transform;
            hitbox = transf.Find("Hitbox").gameObject;
        }

        void Start()
        {
            Setup();
        }

        private void Update()
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackrange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }

        public void Setup()
        {
            enemyName = enemyData.enemyName;
            health = enemyData.health;
            attack = Random.Range(enemyData.attack/2, enemyData.attack);

            hitbox.SetActive(false);
        }

        private void Patrolling()
        {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
            {
                anim.SetBool("canRun", true);
                agent.SetDestination(walkPoint);
            }
                

            Vector3 distanceToWalkPoint = transform.position - walkPoint;
            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f || destinationTimeOut == true)
            {
                walkPointSet = false;
                StartCoroutine(DestinationTimeOut());              
            }               
        }
        //Time before ai changes direction
        IEnumerator DestinationTimeOut()
        {
            yield return new WaitForSeconds(5f);
            destinationTimeOut = false;
        }

        private void SearchWalkPoint()
        {
            //Calculate random point in range
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);
            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
                walkPointSet = true;
        }
        private void ChasePlayer()
        {
            if (!playerInAttackRange)
            {
                anim.SetBool("canRun", true);
                agent.SetDestination(player.position);
            }          
        }
        private void AttackPlayer()
        {

            //Make sure enemy doesn't move
            anim.SetBool("canRun", false);
            agent.SetDestination(transform.position);
            //face player when attacking
            transform.LookAt(player);
            if (!alreadyAttacked)
            {
                Attack();
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
        //AttackCode
        private void Attack()
        {            
            if (attackCombo == 0)
            {
                anim.SetTrigger("Attack1");
                attackCombo++;
            }
            else
            {
                anim.SetTrigger("Attack2");
                attackCombo = 0;
            }
        }
        private void ResetAttack()
        {
            alreadyAttacked = false;
        }

        //Takes Damage
        public void Hit(int _dmg)
        {
            health -= _dmg;
            StartCoroutine(GotHit());
            if (IsDead())
            {
                anim.SetBool("isDead", true);
                //enemydies
                Destroy(this.gameObject, 4f);               
            }
        }
        //Hit indicator
        IEnumerator GotHit()
        {
            anim.SetTrigger("Hit");
            this.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.5f);
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }
        //Check if enemy dead
        public bool IsDead()
        {
            return health <= 0;
        }

    }

}

