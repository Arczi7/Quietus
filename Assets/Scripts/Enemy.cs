using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    
    [Header("Enemy Stats")]
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float scoreValue;
    [Header("Other Settings")]
    [SerializeField] private float damageTime;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject healthPrefab;

    private Transform player;
    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        agent.speed = speed;
        agent.stoppingDistance = 3f;
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
            float chance = UnityEngine.Random.Range(1, 10);
            if(chance >= 8)
            {
                 Vector3 position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                Instantiate(healthPrefab, position , transform.rotation);
            }
            Destroy(gameObject);
            FindObjectOfType<PlayerStats>().AddScore(scoreValue);
            FindObjectOfType<GameManager>().RemoveEnemy();
        }
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

    public void RemoveHealth(int damage)
    {
        health -= damage;
        GetComponent<EnemyHealthBar>().UpdateHealthBar();
    }

    public int Health
    {
        get => health;
    }

}
