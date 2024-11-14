using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    
    [Header("Enemy Stats")]
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float scoreValue;
    [Header("Other Settings")]
    [SerializeField] private float damageTime;
    [SerializeField] private NavMeshAgent agent;

    private Transform player;
    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        agent.speed = speed;
        agent.stoppingDistance = 2f;
    }

    
    void Update()
    {
        LookAtPlayer();
        if(Vector3.Distance(transform.position, player.position) > agent.stoppingDistance)
        {
            FollowPlayer();
        }
        else
        {
            StopNearPlayer();
            if(!isAttacking)
            {
                isAttacking = true;
                StartCoroutine(AttackPlayer());
            }
        }

        if(health <= 0)
        {
            gameObject.SetActive(false);
            FindObjectOfType<PlayerStats>().AddScore(scoreValue);
        }
    }

    public void RemoveHealth(float damage)
    {
        health -= damage;
    }

    private void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void FollowPlayer()
    {
        agent.isStopped = false;
        agent.updatePosition = true;
        agent.destination = player.position;
    }

    private void StopNearPlayer()
    {
        agent.velocity = Vector3.zero;
        agent.updatePosition = false;
        agent.isStopped = true;
    }

    private IEnumerator AttackPlayer()
    {
        animator.SetTrigger("Attack");
        player.GetComponent<PlayerStats>().DamagePlayer(damage);
        yield return new WaitForSeconds(damageTime);
        isAttacking = false;
    }
}
