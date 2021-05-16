using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float triggeredLimit = 60f; // within 60 seconds, he will start chasing
    [SerializeField] float runningSpeedWhenNear;
    [SerializeField] float runningSpeedWhenFarAway;
    [SerializeField] AudioClip[] provokedAudio;
    [SerializeField] AudioClip[] deathAudio;
    [SerializeField] AudioClip[] huntingAudio;
    public Sun sun;
    AudioSource audioPlayer;
    UnityEngine.AI.NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    public bool isProvoked = false;
    public bool isInRange;
    public EnemyHealth health;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        audioPlayer = GetComponent<AudioSource>();
        target = GameObject.Find("Player").transform;
        sun = FindObjectOfType<Sun>();

        float triggerTime = Random.Range(0, triggeredLimit);
        Debug.Log(triggerTime);
        Invoke("TriggerHuntingZombie", triggerTime);
    }
    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (sun.isDay)
        {
            navMeshAgent.enabled = false;
            GetComponent<Animator>().SetBool("Attack", false);
            GetComponent<Animator>().ResetTrigger("Move");
            GetComponent<Animator>().SetBool("Idle", true);
        } else
        {
            if (isProvoked)
            {
                EngageTarget();
            }
            else if (distanceToTarget <= chaseRange && !isProvoked)
            {
                isProvoked = true;
                playProvokedAudio();
            }

            if (distanceToTarget <= chaseRange && !isInRange)
            {
                isInRange = true;
                playProvokedAudio();
            }
            else if (distanceToTarget > chaseRange)
            {
                isInRange = false;
            }

            if (isInRange && isProvoked)
            {
                navMeshAgent.speed = runningSpeedWhenNear;
            }
            else
            {
                navMeshAgent.speed = runningSpeedWhenFarAway;
            }
        }
        if (health.IsDead())
        {
            // disable the navmesh agent + script
            playDyingAudio();
            enabled = false;
            navMeshAgent.enabled = false;
            return;
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
         

        

    }

    private void TriggerHuntingZombie()
    {
        isProvoked = true;
        if (!sun.isDay)
        {
            playHuntingAudio();
        }
  
    }

    private void playDyingAudio()
    {
        int n = Random.Range(0, deathAudio.Length);
        audioPlayer.PlayOneShot(deathAudio[n]);
    }

    // this method will be called via BroadcastMessage in EnemyHealth
    public void OnDamageTaken()
    {
        if (!isProvoked)
        {
            isProvoked = true;
            playProvokedAudio();
        }
    }

    void playProvokedAudio()
    {
        int n = Random.Range(0, provokedAudio.Length);
        audioPlayer.PlayOneShot(provokedAudio[n]);
    }

    void playHuntingAudio()
    {
        int n = Random.Range(0, huntingAudio.Length);
        audioPlayer.PlayOneShot(huntingAudio[n]);
    }

    private void EngageTarget()
    {
        // if we aren't close enough to attack, move closer
        // else attack the player and lose hitpoints on player
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if (distanceToTarget < navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        // trigger the move state -> true in our animator controller
        navMeshAgent.enabled = true;
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetBool("Idle", false);
        GetComponent<Animator>().SetTrigger("Move");
        // tell the enemy the location of player each frame
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("Attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        // get the quaternion that will help us look at target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
