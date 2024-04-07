using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject prefabPowerup; // Prefab del powerup a generar
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private PowerupPool powerupPool;

    private float spawnRangeX = 50.0f, spawnRangeY = 180.0f; // Rango en el eje X donde se generan los enemigos
    private float spawnDelay = 5.0f; // Retraso entre cada generación de enemigos

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            SpawnEnemy();
        }

        // Iniciar la generación continua de enemigos
        InvokeRepeating("SpawnEnemy", 0.5f, spawnDelay);

        // Starts the continuous generation of powerups
        InvokeRepeating("SpawnPowerUp", 5.0f, 10.0f);
    }

    // Genera un solo enemigo en una posición aleatoria
    private void SpawnEnemy()
    {
        // Calcular la posición aleatoria dentro del rango definido
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),
            Random.Range(-spawnRangeY, spawnRangeY), 0f);

        // Instanciar el enemigo en la posición calculada
        GameObject enemy = enemyPool.RequestEnemy();

        if (enemy != null)
        {
            enemy.transform.position = spawnPosition;
        }
        else
        {
            // Debug.Log("No enemy available.");
        }

        // Instanciar el powerup en la misma posición (si lo deseas)
        // Instantiate(prefabPowerup, spawnPosition, Quaternion.identity);
    }

    private void SpawnPowerUp()
    {
        Debug.Log("Instantiating powerup.");

        // Calculate the random position within the defined range
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),
            Random.Range(-spawnRangeY, spawnRangeY), 0f);

        // Instantiate the powerup at the calculated position
        GameObject powerup = powerupPool.RequestPowerup();

        if (powerup != null)
        {
            powerup.transform.position = spawnPosition;
        }
        else
        {
            Debug.Log("No powerup available.");
        }
    }
}