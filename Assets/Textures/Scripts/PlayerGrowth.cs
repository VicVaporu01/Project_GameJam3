using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrowth : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del objeto
    public float growthRate = 0.1f; // Tasa de crecimiento del objeto
    public int objectsAbsorbed = 0; // Número de objetos absorbidos
    public float growthSpeed = 2f; // Velocidad de crecimiento gradual

    private Vector3 targetScale; // Escala a la que debe crecer gradualmente el objeto

    void Start()
    {
        // Inicializar la escala de destino como la escala actual del objeto
        targetScale = transform.localScale;
    }

    void Update()
    {
        // Obtener la entrada de las teclas de flecha para mover el objeto
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Mover el objeto en función de la entrada del teclado
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        transform.Translate(movement * speed * Time.deltaTime);

        // Aumentar gradualmente el tamaño del objeto si la escala no es igual a la escala de destino
        if (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, growthSpeed * Time.deltaTime);
        }
    }

    // Método que se llama cuando el objeto absorbe otro objeto
    public void AbsorbObject()
    {
        objectsAbsorbed++; // Aumenta el contador de objetos absorbidos
        if (objectsAbsorbed % 3 == 0 && objectsAbsorbed > 0)
        {
            // Aumentar la escala de destino del objeto
            targetScale *= 2f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto colisionado tiene la etiqueta "ObjectToAbsorb"
        if (other.CompareTag("ObjectToAbsorb"))
        {
            // Absorber el objeto colisionado
            AbsorbObject();

            // Destruir el objeto colisionado
            Destroy(other.gameObject);
        }
    }
}
