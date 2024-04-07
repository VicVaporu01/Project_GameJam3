using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D enemyRB;
    public bool isBig;
    private GameObject player;
    private PlayerController playerScript;

    [SerializeField] private float movementSpeed = 3;
    private Vector2 randomDirection;

    [SerializeField] private float health = 5.0f;
    [SerializeField] private bool hasLineOfSight = false;
    [SerializeField] private float timeScared = 3.0f, timeScaping = 0, timePatrolling = 5.0f;

    private bool isPushed = false;
    private float pushDelay = 0.5f, pushTimer = 0f;

    [Header("ANIMACIONES")] private Animator EnemyAnimator;
    [SerializeField] private bool hasPain = false;

    private void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        EnemyAnimator = GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (hasLineOfSight)
        {
            timeScaping -= Time.deltaTime;
        }

        if (health <= 0)
        {
            DesactivateEnemy();
        }

        if (isPushed)
        {
            pushTimer += Time.deltaTime;
            if (pushTimer >= pushDelay)
            {
                isPushed = false;
                pushTimer = 0f;
            }
        }
    }

    private void OnEnable()
    {
        health = 5.0f;
    }

    private void FixedUpdate()
    {
        DetectPlayer();
        if (!isPushed)
        {
            isBig = playerScript.Big;
            if (isBig)
            {
                Escape();
                EnemyAnimator.SetBool("IsBig", true);
            }
            else
            {
                Follow();
                EnemyAnimator.SetBool("IsBig", false);
            }
        }

        // If the enemy doesn't have line of sight, it will reduce the timeScaping
        if (!hasLineOfSight && timeScaping >= 0.0f)
        {
            timeScaping -= Time.deltaTime;
        }

        if (!hasLineOfSight && timeScaping <= 0.0f)
        {
            if (timePatrolling >= 0.0f)
            {
                timePatrolling -= Time.deltaTime;
                enemyRB.velocity = randomDirection * movementSpeed;
            }
            else
            {
                RandomMovement();
                timePatrolling = 5.0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TakeDamage(other.gameObject.GetComponent<PlayerController>().GetDamage());
            enemyRB.AddForce((transform.position - other.transform.position) * 2.0f, ForceMode2D.Impulse);
            isPushed = true;
        }
    }

    private void DetectPlayer()
    {
        // A vector to give the raycast a direction so it can has an offset
        Vector3 direction = (player.transform.position - transform.position).normalized;

        // To see the raycast in the scene view
        Ray2D rayToSee = new Ray2D(transform.position + direction * 7.0f, direction);
        Debug.DrawRay(rayToSee.origin, rayToSee.direction * 30.0f);

        // Raycast to detect the player
        RaycastHit2D ray = Physics2D.Raycast(transform.position + direction * 7.0f,
            player.transform.position - transform.position, 30.0f);
        if (ray.collider)
        {
            if (ray.collider.name == "Player")
            {
                hasLineOfSight = true;
                timeScaping = timeScared;
            }
            else
            {
                hasLineOfSight = false;
            }
        }
        else
        {
            hasLineOfSight = false;
        }
    }

    private void RandomMovement()
    {
        float randomX = Random.Range(-10.0f, 10.0f);
        float randomY = Random.Range(-10.0f, 10.0f);

        randomDirection = new Vector2(randomX, randomY).normalized;
    }

    private void Escape()
    {
        // If has line of sight or has time to be scaping, the enemy will scape from the player
        if (hasLineOfSight || timeScaping >= 0.0f)
        {
            enemyRB.velocity = (transform.position - player.transform.position).normalized * movementSpeed;
        }
        else
        {
            enemyRB.velocity = Vector2.zero;
        }
    }

    private void Follow()
    {
        // If has line of sight or has time to be following, the enemy will follow the player
        if (hasLineOfSight || timeScaping >= 0.0f)
        {
            enemyRB.velocity = (player.transform.position - transform.position).normalized * movementSpeed;
        }
        else
        {
            enemyRB.velocity = Vector2.zero;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void DesactivateEnemy()
    {
        gameObject.SetActive(false);
    }
}