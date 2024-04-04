using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5.0f; // Velocidad de movimiento

    public bool hasPowerup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // Obtiene el input horizontal (teclas A y D)
        float moveVertical = Input.GetAxis("Vertical"); // Obtiene el input vertical (teclas W y S)

        Vector2 movement = new Vector2(moveHorizontal, moveVertical); // Crea un vector de movimiento

        GetComponent<Rigidbody2D>().velocity = movement * speed; // Aplica el movimiento al Rigidbody2D del jugador
    }

    private void OnTriggerEnter2D(Collider2D other) //colisiona con el powerup y lo desaparece
    {
        if (other.gameObject.CompareTag("powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
        }
    }
}
