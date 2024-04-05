using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5.0f; // Velocidad de movimiento
    public int health = 10;
    private float damage;
    private Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // Obtiene el input horizontal 
        float moveVertical = Input.GetAxis("Vertical"); // Obtiene el input vertical (

        Vector2 movement = new Vector2(moveHorizontal, moveVertical); // Crea un vector de movimiento

        playerRB.velocity = movement * speed; // Aplica el movimiento al Rigidbody2D del jugador
        if (health <= 0) // Si la salud es 0 o menos
        {
            Destroy(gameObject); // Destruye el objeto del jugador
        }
    }

    // Función para recibir daño
    public void TakeDamage(int damage)
    {
        health -= damage; // Reduce la salud del jugador
    }

    public float GetDamage()
    {

        return damage;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) //
        {
            TakeDamage(1); // 
        }
    }

}