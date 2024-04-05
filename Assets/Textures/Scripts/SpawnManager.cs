using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo a generar
    public float spawnRangeX = 10f; // Rango en el eje X donde se generan los enemigos
    public float spawnRangeY = 10f; // Rango en el eje Y donde se generan los enemigos
    public int enemyCount = 5; // Cantidad inicial de enemigos
    public float spawnDelay = 1f; // Retraso entre cada generación de enemigos
    public GameObject prefabPowerup; // Prefab del powerup a generar

    // Start is called before the first frame update
    void Start()
    {
        // Generar los enemigos iniciales
        SpawnEnemies(enemyCount);

        // Iniciar la generación continua de enemigos
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnDelay);
    }

    // Genera una cantidad específica de enemigos
    void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy();
        }
    }

    // Genera un solo enemigo en una posición aleatoria
    void SpawnEnemy()
    {
        // Calcular la posición aleatoria dentro del rango definido
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY), 0f);

        // Instanciar el enemigo en la posición calculada
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Instanciar el powerup en la misma posición (si lo deseas)
        Instantiate(prefabPowerup, spawnPosition, Quaternion.identity);
    }
}
