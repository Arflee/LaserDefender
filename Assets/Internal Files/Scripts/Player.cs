using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab = null;
    [SerializeField, Range(-5, 5)] private float xBoundsOffset = 0.5f;
    [SerializeField, Range(-5, 5)] private float yBoundsOffset = 0.5f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float laserSpeed = 10f;
    [SerializeField, Range(0.1f, 5)] private float profectileFiringPeriod = 0.1f;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    private Coroutine firingCoroutine;

    private void Start()
    {
        SetupMoveBoundaries();
    }

    private void Update()
    {
        Move();
        Fire();
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
        if(Input.GetButtonUp("Fire1"))
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
}
