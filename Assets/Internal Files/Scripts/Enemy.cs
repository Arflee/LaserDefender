using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [Header("Laser")]
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private float speedOfLaser = 3f;
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 3f;

    [Header("Enemy")]
    [Space]
    [SerializeField] private GameObject explosionPrefab = null;
    [SerializeField] private float health = 100f;
    [SerializeField] private AudioClip enemyDeath = null;
    [SerializeField] private AudioClip enemyShot = null;

    private float shootCounter;
    private ParticleSystem explosionsParticles;
    private AudioSource enemySFX = null;

    private void Start()
    {
        explosionsParticles = explosionPrefab.GetComponent<ParticleSystem>();
        enemySFX = GetComponent<AudioSource>();
        shootCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update()
    {
        CountDown();
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
        enemySFX.PlayOneShot(enemyShot);
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
            GameObject particleObject =
                Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);

            Destroy(particleObject, explosionsParticles.main.duration);

            AudioSource.PlayClipAtPoint(enemyDeath, transform.position);
            Destroy(this.gameObject);
        }
    }
}