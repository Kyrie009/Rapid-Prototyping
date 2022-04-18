using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Prototype2;

public class EnemyAI4 : GameBehaviour
{
    //Stats
    public EnemyData enemyData;
    public string enemyName;
    public int health;
    public int attack;
    public int exp;
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
    public float walkPointRange = 5f;

    //Attacking
    public float timeBetweenAttacks = 1.6f;
    bool alreadyAttacked;
    int attackCombo;

    //States
    public float sightRange, attackrange;
    public bool playerInSightRange, playerInAttackRange;

    //Animation
    public Animator anim;
    //debug
    bool isRewarded = false;

    private void Awake()
    {
        //get references
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        transf = transform;
        hitbox = transf.Find("EnemyHitbox").gameObject;
        //Startup
        Setup();
    }

    private void Update()
    {
        if (!IsDead())
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackrange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }

    }
    //Sets up the enemy
    public void Setup()
    {
        enemyName = enemyData.enemyName;
        health = enemyData.health;
        attack = Random.Range(enemyData.attack / 2, enemyData.attack);
        exp = enemyData.exp;

        hitbox.SetActive(false);
    }

    //MovementTypes
    private void ChasePlayer()
    {
        if (!playerInAttackRange)
        {
            agent.speed = 2.5f;
            anim.SetFloat("Speed", agent.speed);
            agent.SetDestination(player.position);
        }
    }
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.speed = 0f;
        anim.SetFloat("Speed", agent.speed);
        //face player when attacking
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            Attack();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.speed = 1;
            anim.SetFloat("Speed", agent.speed);
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
    IEnumerator DestinationTimeOut() //Time before ai changes direction
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

    //AttackCode
    private void Attack()
    {
        attackCombo++;
        hitbox.SetActive(true);
        anim.SetTrigger("Attack" + attackCombo);
        StartCoroutine(HitTimer());
        if (attackCombo > 1) attackCombo = 0;
    }
    IEnumerator HitTimer()
    {
        yield return new WaitForSeconds(0.2f);
        hitbox.SetActive(false);
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //Takes Hit
    public void Hit(int _dmg)
    {
        if (!IsDead())
        {
            health -= _dmg;
            anim.SetTrigger("Hit");
            if (IsDead())
            {
                Kill();
            }
        }
    }

    private void Kill() //Kill code
    {
        anim.SetTrigger("Died");
        killReward();
        Destroy(this.gameObject, 4f);
    }

    private void killReward()
    {
        if (!isRewarded)
        {
            _UI4.player4.RewardExp(exp);
            isRewarded = true;
        }

    }

    public bool IsDead() //Check if enemy dead
    {
        return health <= 0;
    }
}


