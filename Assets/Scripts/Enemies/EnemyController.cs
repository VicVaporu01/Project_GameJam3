using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D enemyRB;
    public bool isBig;
    private GameObject player;

    [SerializeField] private float movementSpeed = 3;

    [SerializeField] private float health = 5.0f;
    [SerializeField] private bool hasLineOfSight = false;
    [SerializeField] private float timeScared = 3.0f, timeScaping = 0;
    
    private bool isPushed = false;
    private float pushDelay = 0.5f;
    private float pushTimer = 0f;

    private void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
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
            if (isBig)
            {
                Escape();
            }
            else
            {
                Follow();
            }
        }

        // If the enemy doesn't have line of sight, it will reduce the timeScaping
        if (!hasLineOfSight && timeScaping >= 0.0f)
        {
            timeScaping -= Time.deltaTime;
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
        Ray2D rayToSee = new Ray2D(transform.position + direction * 3.0f, direction);
        Debug.DrawRay(rayToSee.origin, rayToSee.direction * 30.0f);

        // Raycast to detect the player
        RaycastHit2D ray = Physics2D.Raycast(transform.position + direction * 3.0f,
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