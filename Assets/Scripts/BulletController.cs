using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float projSpeed;

    public float lifeTime;
    private float lifeCounter = 0;

    void FixedUpdate()
    {
        transform.Translate(Vector3.down * projSpeed * Time.deltaTime, Space.Self);
        lifeCounter += Time.deltaTime;
        if(lifeCounter>= lifeTime)
        {
            Destroy(gameObject);
        }

    }
}
