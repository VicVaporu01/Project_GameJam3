using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPool : MonoBehaviour
{
    [SerializeField] private GameObject powerupPrefab;
    private List<GameObject> powerupList = new List<GameObject>();

    private int poolSize = 5;

    private void Start()
    {
        AddPowerupToPool();
    }

    private void AddPowerupToPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject powerUp = Instantiate(powerupPrefab, transform);
            powerupList.Add(powerUp);

            powerUp.SetActive(false);
        }
    }

    public GameObject RequestPowerup()
    {
        foreach (GameObject powerup in powerupList)
        {
            if (!powerup.activeSelf)
            {
                powerup.SetActive(true);

                return powerup;
            }
        }

        return null;
    }
}