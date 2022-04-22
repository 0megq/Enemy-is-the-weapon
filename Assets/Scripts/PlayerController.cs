using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class PlayerController : MonoBehaviour
{


    public float moveSpeed = 2;

    public Animator animator;

    public Vector2 mouseScreenPos;
    private Vector2 dir;

    public Transform grabCenter;
    public float grabRadius;

    public LayerMask enemyMask;

    public bool grabbing = false;

    private Collider2D coll;
    private Collider2D lastColl;
    private bool grabbed = false;

    private bool holding = false;

    void Awake()
    {
        
    }

    void FixedUpdate()
    {
        LookAtMouse();
        Movement();

        animator.SetBool("grabbing", grabbing);
        animator.SetBool("holding", holding);


        if (Input.GetButton("Fire1"))
        {
            grabbing = true;
        }
        else if(!Input.GetButton("Fire1"))
        {
            grabbing = false;
        }

        coll = Physics2D.OverlapCircle((Vector2)grabCenter.position, grabRadius, enemyMask);

        if (coll != null)
        {
            grabbed = true;
            lastColl = coll;
            if (grabbing)
            {
                coll.transform.parent = transform;
                holding = true;
            }
        }
        if (!grabbing && grabbed)
        {
            holding = false;
            lastColl.transform.parent = null;
            lastColl.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void LookAtMouse()
    {
        mouseScreenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        dir = (mouseScreenPos - (Vector2)transform.position).normalized;

        transform.up = -dir;
    }

    void Movement()
    {
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * moveSpeed * Time.deltaTime;
    }
}
