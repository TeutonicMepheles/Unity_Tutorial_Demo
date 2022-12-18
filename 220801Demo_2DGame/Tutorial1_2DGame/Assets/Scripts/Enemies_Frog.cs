using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_Frog : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Transform leftpoint, rightpoint;
    public LayerMask ground;
    private Collider2D coll;
    public float speed = 500f,jumpForce=300f;
    private bool faceLeft;
    private float leftx, rightx;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        transform.DetachChildren();//使左右两个点不随主体移动
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        SwitchAnim();
    }

    void Movement() 
    {
        if (faceLeft)
        {
            if (coll.IsTouchingLayers(ground)) 
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, jumpForce);
            }
            
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }          
        }
        else 
        {
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpForce);
            }
            
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }
    }

    void SwitchAnim() 
    {
        anim.SetBool("idle", true);
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0) 
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
            else if (coll.IsTouchingLayers(ground) && anim.GetBool("falling")) 
            {
                anim.SetBool("falling", false);
                anim.SetBool("idle",true);
            }
        }
    }
 } 
