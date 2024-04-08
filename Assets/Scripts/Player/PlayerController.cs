using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRB;
    [SerializeField] private GameObject playerCam, grownPlayerCam;
    [SerializeField] private AudioClip soundPowerup, soundCollision, SoundGrow;

    [Header("PLAYER STATS")] private Vector3 targetScale;
    [SerializeField] private float speed = 5.0f, velocity;
    [SerializeField] private float health = 10f;
    [SerializeField] public bool Big = false;
    private float damage = 3.0f;

    [SerializeField] private Slider healthSlider, foodSlider;
    [SerializeField] private int objectsAbsorbed = 0;
    private bool hasPowerup;
    private float growthSpeed = 7.0f;
    private int MaxAbsorb = 3;

    [Header("ANIMACIONES")] private Animator playerAnimator;
    [SerializeField] private bool hasPain = false;
    [SerializeField] private bool isDead = false;


    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponent<Animator>();

        healthSlider.maxValue = health;
        foodSlider.maxValue = MaxAbsorb;
        foodSlider.value = 0;
        healthSlider.value = healthSlider.maxValue;
        targetScale = transform.localScale;
    }

    private void Update()
    {
        velocity = playerRB.velocity.magnitude;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;

        playerAnimator.SetFloat("Velocity", Mathf.Abs(velocity));

        playerRB.velocity = movement * speed;
        if (health <= 0)
        {
            //  Destroy(gameObject);
            isDead = true;
            playerAnimator.SetBool("Death", true);
            StartCoroutine(WaitForDeathAnimation());
        }

        if (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, growthSpeed * Time.deltaTime);
        }
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(1f);

        // Luego, pausa el tiempo del juego
        Time.timeScale = 0f;
    }

    private IEnumerator HasPain()
    {
        yield return new WaitForSeconds(1);
        hasPain = false;
        playerAnimator.SetBool("Pain", hasPain);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("powerup"))
        {
            hasPowerup = true;
            speed *= GiveExtraSpeed();
            other.gameObject.SetActive(false);
            StartCoroutine(PowerupTimer());
            AudioManager.Instance.GetAudioSourceSfx().PlayOneShot(soundPowerup, 1.0f);
        }
        else if (other.CompareTag("ObjectToAbsorb"))
        {
            AbsorbObject();

            other.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) //
        {
            AudioManager.Instance.GetAudioSourceSfx().PlayOneShot(soundCollision, 1.0f);
            TakeDamage(1);
        }
    }

    private float GiveExtraSpeed()
    {
        float extraSpeed = 2.0f;
        return extraSpeed;
    }

    private IEnumerator PowerupTimer()
    {
        yield return new WaitForSeconds(3);
        hasPowerup = false;
        speed /= GiveExtraSpeed();
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Reduce la salud del jugador
        healthSlider.value = health;
        hasPain = true;
        playerAnimator.SetBool("Pain", hasPain);
        StartCoroutine(HasPain());
    }

    public float GetDamage()
    {
        return damage;
    }

    public void AbsorbObject()
    {
        AudioManager.Instance.GetAudioSourceSfx().PlayOneShot(SoundGrow, 1.0f);
        objectsAbsorbed++;
        foodSlider.value = objectsAbsorbed;
        // Aument the scale of the player
        if (objectsAbsorbed % MaxAbsorb == 0 && objectsAbsorbed > 0)
        {
            playerCam.SetActive(false);
            grownPlayerCam.SetActive(true);
            MaxAbsorb++;
            targetScale *= 2f;
            foodSlider.value = 0;
            Big = true;
            healthSlider.maxValue += 2;
            health = healthSlider.maxValue;
            foodSlider.maxValue = MaxAbsorb;
            healthSlider.value = healthSlider.maxValue;
        }
    }
}