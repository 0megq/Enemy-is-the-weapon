using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;

    public Transform gun;

    public Transform firePoint;
    public GameObject bullet;

    public float moveSpeed = 1f;

    public float maxRange = 2;
    public float minRange = 1;
    public float fireSpeed;
    private float fireCounter;
    private bool shooting;

    private bool retreating;

    private bool walking;

    private Vector3 dir;

    
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        dir = (player.position - gun.position).normalized;

        gun.up = -dir;
        
        if(gun.rotation.eulerAngles.z < 350 && gun.rotation.eulerAngles.z > 170)
        {
            gun.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            gun.localScale = new Vector3(1, 1, 1);
        }

        if (maxRange > Vector2.Distance(player.transform.position, transform.position))
        {
            if (Vector2.Distance(player.transform.position, transform.position) < minRange && transform.parent == null)
            {
                retreating = true;
                shooting = false;
                walking = false;
            }
            else
            {
                shooting = true;
                walking = false;
                retreating = false;
            }
        }
        else
        {
            walking = true;
            shooting = false;
            retreating = false;
        }

        if (shooting)
        {
            fireCounter += Time.deltaTime;
            if (fireCounter >= fireSpeed)
            {
                GameObject spawnedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                fireCounter = 0;
            }
        }

        if (walking)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }

        if (retreating)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, -1 * moveSpeed * Time.deltaTime);
        }
    }
}
