using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public enum ZombieState
    {
        IDLE,
        MOVE_FREELY,
        CHASE,
        ATTACK,
        DIE
    }
    private Animator zombieAnimator;
    private NavMeshAgent agent;

    public ZombieState currentZomState = ZombieState.IDLE;
    public Transform playerTransform;
    public float chaseDistance;
    public float attackDistance;
    public float attackCooldown;
    public float attackDelay;
    public float dieDelay;
    public int zombieDamage;
    private float timer = 0;
    private bool isAttacking = false;
    private bool isDie = false;
    //
    public int minZombieSpeed = 3;
    public int maxZombieSpeed = 6;
    //
    public bool canMoveFreely = false;
    public float timeBetweenChangeState = 10;
    private float timerCheckChange;
    //
    public float timeToChangeSpeed = 4f;
    //
    public float maxHealth = 5;
    private float currentHealth;


    // flame area damage
    private bool isIgnited;
    private float igniteTimer;
    private float igniteColldown;
    //
    public List<GameObject> items;
    // Start is called before the first frame update
    void Start()
    {
        zombieAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        ChangeRamdomState(0);
        SetRamdomSpeedZom();
        currentHealth = maxHealth;
        playerTransform = FindObjectOfType<PlayerManager>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        ChangeZombieState(currentZomState);

        if (isIgnited)
        {
            ApplyIgnite();
        }
    }

    public void ChangeZombieState(ZombieState state)
    {
        switch (state)
        {
            case ZombieState.IDLE:
                //anim idel
                zombieAnimator.SetBool("Move", false);
                agent.ResetPath();
                ChangeRamdomState(timeBetweenChangeState);
                if (PlayerInRange(chaseDistance))
                    currentZomState = ZombieState.CHASE;
                break;
            case ZombieState.MOVE_FREELY:
                //anim idel
                zombieAnimator.SetBool("Move", true);
                SetRandomDestination();
                ChangeRamdomState(timeBetweenChangeState);
                if (PlayerInRange(chaseDistance))
                {
                    agent.ResetPath();
                    currentZomState = ZombieState.CHASE;
                }
                break;
            case ZombieState.CHASE:
                //anim move
                zombieAnimator.SetBool("Move", true);
                agent.SetDestination(playerTransform.position);
                if (PlayerInRange(attackDistance))
                    currentZomState = ZombieState.ATTACK;
                else if (PlayerOutRange(chaseDistance))
                    ChangeRamdomState(0);
                break;
            case ZombieState.ATTACK:
                //anim attack
                agent.SetDestination(transform.position);
                timer -= Time.deltaTime;
                if (!isAttacking && timer <= 0)//
                {
                    zombieAnimator.SetTrigger("Attack");
                    StartCoroutine(ZomAttackDelay());
                    //Debug.Log("Zom attack");
                }
                if (PlayerOutRange(attackDistance))
                {
                    currentZomState = ZombieState.CHASE;
                    timer = 0;
                }
                break;
            case ZombieState.DIE:
                if (!isDie)
                {
                    //anim die
                    zombieAnimator.SetTrigger("Die");
                    agent.ResetPath();
                    StartCoroutine(ZomDieDelay());

                }
                break;
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentZomState == ZombieState.IDLE || currentZomState == ZombieState.MOVE_FREELY)
        {
            currentZomState = ZombieState.CHASE;
        }
        currentHealth -= damage;
        //Debug.Log("trung dan" + currentHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth <= 0)
        {
            currentZomState = ZombieState.DIE;
        }
    }

    public bool PlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= range;
    }
    public bool PlayerOutRange(float range)
    {
        return Vector3.Distance(transform.position, playerTransform.position) > range;
    }
    public void ChangeRamdomState(float time)
    {
        if (!canMoveFreely) { return; }

        if (time == 0)
        {
            currentZomState = (Random.Range(0, 2) == 0) ? ZombieState.IDLE : ZombieState.MOVE_FREELY;
            return;
        }
        timerCheckChange += Time.deltaTime;
        if (timerCheckChange > time)
        {
            timerCheckChange = 0;
            currentZomState = (Random.Range(0, 2) == 0) ? ZombieState.IDLE : ZombieState.MOVE_FREELY;
            //Debug.Log(currentZomState.ToString());
        }

    }

    void SetRandomDestination()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            //Vector3 randomPoint = GetRandomPointOnNavMesh();
            agent.SetDestination(GetRandomPointOnNavMesh());
        }
    }

    Vector3 GetRandomPointOnNavMesh()
    {
        NavMeshHit hit;
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * 100f;

        if (NavMesh.SamplePosition(randomPoint, out hit, 100f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        // If a random point is not on NavMesh, try again.
        return GetRandomPointOnNavMesh();
    }

    public void SetRamdomSpeedZom()
    {
        agent.speed = Random.Range(minZombieSpeed, maxZombieSpeed + 1);
        Invoke("SetRamdomSpeedZom", timeToChangeSpeed);
    }

    private void ItemsSpawn()
    {
        if (!items.Any()) return;
        if(Random.Range(0, 100)<50)
            Instantiate(items[(int)Random.Range(0, items.Count)], transform.position, Quaternion.identity);
    }

    IEnumerator ZomAttackDelay()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackDelay);
        PlayerManager player = playerTransform.GetComponent<PlayerManager>();
        if (player != null && PlayerInRange(attackDistance + 0.5f))
        {
            player.TakeDamage(zombieDamage);

        }
        timer = attackCooldown;
        isAttacking = false;
    }

    IEnumerator ZomDieDelay()
    {
        isDie = true;
        GameManager.instance.CountEnemy();
        ItemsSpawn();
        yield return new WaitForSeconds(dieDelay);
        Destroy(gameObject);
    }

    public void ApplyIgnite()
    {
        igniteTimer -= Time.deltaTime;

        if (igniteTimer < 0)
        {
            TakeDamage(1);
            igniteTimer = igniteColldown;
            Debug.Log(gameObject.name + " take ignite damage");
        }
    }

    public void SetupIgnite(bool isIgnited, float duration)
    {
        this.isIgnited = isIgnited;
        igniteColldown = duration;
    }
}
