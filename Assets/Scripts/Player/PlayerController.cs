using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Velocidad de movimiento
    public int health = 10;
    private float damage = 3.0f;
    private Rigidbody2D playerRB;
    public Slider Vida;
    public Slider Comida;
    public int objectsAbsorbed = 0; // Número de objetos absorbidos
    public bool hasPowerup;
    private Vector3 targetScale;
    public float growthSpeed = 2f; 
    private int MaxAbsorb=3;
    private GameObject enemy; 
    


    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy");
        playerRB = GetComponent<Rigidbody2D>();
        
        Vida.maxValue=health;
        Comida.maxValue=MaxAbsorb;
        Comida.value=0;
        Vida.value=Vida.maxValue;
        targetScale = transform.localScale;
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
        if (transform.localScale != targetScale) //lo hace crecer
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, growthSpeed * Time.deltaTime);
        }
    }

    // Función para recibir daño
    public void TakeDamage(int damage)
    {
        health -= damage; // Reduce la salud del jugador
        Vida.value=health;
    }

    private void OnTriggerEnter2D(Collider2D other) // El jugador colisiona con el powerup y lo desaparece
    {
        if (other.gameObject.CompareTag("powerup"))
        {
            hasPowerup = true;
            speed = speed * GiveExtraSpeed();
            other.gameObject.SetActive(false);
            StartCoroutine(PowerupTimer());
        }
        if (other.gameObject.CompareTag("Enemy")) //
        {
            TakeDamage(1); // 
        }
        if (other.CompareTag("ObjectToAbsorb"))
        {
            // Absorber el objeto colisionado
            AbsorbObject();

            // Destruir el objeto colisionado
            Destroy(other.gameObject);
        }
    }

    private float GiveExtraSpeed()
    {
        float extraSpeed = 2.0f;
        return extraSpeed;
    }

    IEnumerator PowerupTimer()
    {
        yield return new WaitForSeconds(3);
        hasPowerup = false;
        speed = speed / GiveExtraSpeed();
    }

    public float GetDamage()
    {
        return damage;
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Enemy")) //
    //     {
    //         TakeDamage(1); // 
    //     }
    // }
}
    public void AbsorbObject()
    {
        objectsAbsorbed++; // Aumenta el contador de objetos absorbidos
        Comida.value=objectsAbsorbed; //aumenta la barra 
        if (objectsAbsorbed % MaxAbsorb == 0 && objectsAbsorbed > 0) //verifica si ya se comio el maximo
        {
            // Aumentar la escala de destino del objeto
            MaxAbsorb++;                // aumenta la cantidad de comida necesaria para volver a crecer
            targetScale *= 2f;
            Comida.value=0;
            EnemyController script = enemy.GetComponent<EnemyController>();  //puede que esto se pueda de hacer de otra forma mas optima 
            script.IsBig = true;
        }
    }
    
}
