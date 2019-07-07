using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private int scoreForKilling = 10;
    [SerializeField] private GameObject explosionPrefab = null;
    [SerializeField] private float health = 100f;

    [Header("Audio")]
    [SerializeField] private AudioClip enemyDeath = null;
    [SerializeField] private AudioClip enemyShot = null;

    [Header("Projectile")]
    [SerializeField] private bool isShooting = true;
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private float speedOfLaser = 3f;
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 3f;

    private float deathVolume = 0.5f;
    private float shotVolume = 0.5f;
    private float shootCounter;
    private ParticleSystem explosionsParticles = null;
    private AudioSource enemySFX = null;
    private GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();

        explosionsParticles = explosionPrefab.GetComponent<ParticleSystem>();

        enemySFX = GetComponent<AudioSource>();
        deathVolume = shotVolume;

        shootCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update()
    {
        if (isShooting)
        {
            CountDown();
        }
    }

    private void CountDown()
    {
        shootCounter -= Time.deltaTime;
        if (shootCounter <= 0f)
        {
            Fire();
            shootCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject enemyLaser =
            Instantiate(projectilePrefab, this.gameObject.transform.position, Quaternion.identity);
        enemyLaser.GetComponent<Rigidbody2D>().velocity = Vector2.down * speedOfLaser;
        enemySFX.PlayOneShot(enemyShot, shotVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        if (damageDealer != null)
        {
            health -= damageDealer.GetDamage();
            damageDealer.Hit();
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameSession.AddToScore(scoreForKilling);

        GameObject particleObject =
            Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);

        Destroy(particleObject, explosionsParticles.main.duration);

        AudioSource.PlayClipAtPoint(enemyDeath, Camera.main.transform.position, deathVolume);
        Destroy(this.gameObject);
    }
}