﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField, Range(-5, 5)] private float xBoundsOffset = 0.5f;
    [SerializeField, Range(-5, 5)] private float yBoundsOffset = 0.5f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private int health = 100;
    [SerializeField] private GameObject explosionPrefab = null;

    [Header("Audio")]
    [SerializeField, Range(0, 1)] private float deathVolume = 0.5f;
    [SerializeField, Range(0, 1)] private float hitVolume = 0.5f;
    [SerializeField] private AudioClip deathSound = null;
    [SerializeField] private AudioClip hitSound = null;
    
    [Header("Projectile")]
    [SerializeField, Range(0.1f, 5)] private float profectileFiringPeriod = 0.1f;
    [SerializeField] private GameObject laserPrefab = null;
    [SerializeField] private float laserSpeed = 10f;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    private SceneLoader loader;
    private Coroutine firingCoroutine;
    private ParticleSystem explosionParticles;
    private AudioSource audioSource;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        loader = FindObjectOfType<SceneLoader>();
        explosionParticles = explosionPrefab.GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        SetupMoveBoundaries();
    }

    private void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        if (damageDealer != null)
        {
            health -= damageDealer.GetDamage();
            audioSource.PlayOneShot(hitSound, hitVolume);
            damageDealer.Hit();
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        loader.LoadGameOverScene();

        GameObject particleObject =
            Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);

        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathVolume);

        Destroy(particleObject, explosionParticles.main.duration);
        Destroy(this.gameObject);
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        float newXPos = transform.position.x + deltaX;
        newXPos = Mathf.Clamp(newXPos, xMin, xMax);

        float newYPos = transform.position.y + deltaY;
        newYPos = Mathf.Clamp(newYPos, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xBoundsOffset;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xBoundsOffset;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yBoundsOffset;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yBoundsOffset;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContioniously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireContioniously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);

            Rigidbody2D laserRB = laser.GetComponent<Rigidbody2D>();
            laserRB.velocity = new Vector2(0, laserSpeed);

            yield return new WaitForSeconds(profectileFiringPeriod);
        }
    }

    public int GetHealth()
    {
        return health;
    }
}