using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public delegate void JumpingEvent();

    public event JumpingEvent playerJump;
    
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;
    public float moveSpeed = 10f;
    public float rotateSpeed = 15f;
    public float jumpVelocity = 5f;

    public GameObject bullet;
    public float bulletSpeed = 100f;

    private float vInput;
    private float hInput;
    private Rigidbody _rb;
    private GameObject newBullet;
    private Rigidbody bulletRB;
    private bool shoot;

    private BoxCollider _en;

    private GameBehaviour _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _en = GetComponent<BoxCollider>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameBehaviour>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 参数collision 是Collision 类的，所以需要用gameObject定位到 GameObject上然后再获取名字
        if (collision.gameObject.name == "Enemy")
        {
            _gameManager.HP -= 1;

        }
    }

    // Update is called once per frame
    void Update()
    {
        // 将输入轴的输入值关联到变量上,变量作为具体输入情况的表征，每一帧都被更新一次
        // 速度*衰减参数
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        if (Input.GetMouseButtonDown(0))
        {
            newBullet = Instantiate(bullet,gameObject.transform.position+new Vector3(0,0.5f,0),gameObject.transform.rotation) as GameObject;
            bulletRB = newBullet.GetComponent<Rigidbody>();
            shoot = true;
        }
        /*
        gameObject.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        gameObject.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
        
    }

    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;
        Vector3 move = Vector3.forward * vInput;
        
        // Unity中刚体旋转一般使用四元数，可以使用Euler函数把欧拉角的旋转转化为一个四元数
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        
        // 移动=原位置+方向*速度*时间
        _rb.MovePosition(transform.position+transform.forward*vInput*Time.fixedDeltaTime);
        
        // 旋转=刚体对象的旋转组件*旋转的四元数
        _rb.MoveRotation(_rb.rotation*angleRot);
        
        if (IsGrounded(angleRot) && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            playerJump();
        }

        if (shoot)
        {
            bulletRB.velocity = gameObject.transform.forward*bulletSpeed;
        }
    }

    private bool IsGrounded(Quaternion boundAngle)
    {
        Vector3 enemyBottom = new Vector3(_en.bounds.extents.x, _en.bounds.extents.y, _en.bounds.extents.z);
        
        bool grounded = Physics.CheckBox(_en.bounds.center, enemyBottom, boundAngle, groundLayer,
            QueryTriggerInteraction.Ignore);
        return grounded;
    }
}
