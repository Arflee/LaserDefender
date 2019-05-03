using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab = null;
    [SerializeField] private float speedOfLaser = 3f;
    [SerializeField] private float health = 100f;
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 3f;

    private float shootCounter;

    private void Start()
    {
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
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
