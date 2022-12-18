using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float speed;
    public float jumpforce;
    public LayerMask ground;
    public Collider2D coll;
    public int Cherry = 0;
    public Text CherryNum;
    private bool isHurt;
        
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHurt) 
        {
            Movement();           
        }
        SwitchAnim();

    }

    // Define the motion of the player
    void Movement()
    {
        float horizontal_Move = Input.GetAxis("Horizontal");
        // Horizontal  
        // -1 left
        // 0 still
        // 1 right
        float face_Direction = Input.GetAxisRaw("Horizontal");
        // GetAxisRaw return an int
        // player move
        if (horizontal_Move != 0)
        {
            rb.velocity = new Vector2(horizontal_Move * speed * Time.deltaTime, rb.velocity.y); 
            anim.SetFloat("running", Mathf.Abs(face_Direction));
            // Time.deltaTime 不同设备上的物理时钟百分比？
        }
        // player direction
        if (face_Direction != 0)// direction is left or right
        {
            transform.localScale= new Vector3(face_Direction, 1, 1);
            // y and z Axis still
        }
        // 注意组件应该归属在对应的sprite下，否则可能会出现背景移动的bug
        // player jump
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground)) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
            anim.SetBool("jumping", true);        
        }
         
    }

    void SwitchAnim() 
    {
        anim.SetBool("idle", true);// 解除idle状态
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (isHurt) 
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0f);
            if (Mathf.Abs(rb.velocity.x) < 0.1) 
            {
                anim.SetBool("hurt", true);
                anim.SetBool("idle", true);
                isHurt = false;                    
            }
        }
        else if (coll.IsTouchingLayers(ground)) // 返回idle状态
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        // 注意设置碰撞体时要同时修改tag，否则仅设置碰撞体无法达到收集物效果
        if (collision.tag == "Collection")  
        {
            Destroy(collision.gameObject);
            Cherry += 1;
            CherryNum.text = Cherry.ToString();
        } 

    }

    private void OnCollisionEnter2D(Collision2D collision) 
        // debug log: in monobehave method OnCollisionEnter2D & OnTriggerEnter2D, the parameters are different(collider / collision )
    {
        if (collision.gameObject.tag == "Enemies")
        {
            if (anim.GetBool("falling"))
            {
                Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
                anim.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                isHurt = true;
            }

        }

    }

}
