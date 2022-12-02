using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    // 获取刚体
    private Rigidbody rb;
    // 获取移动输入
    private Vector2 movementInput;
    public float speed = 3.5f;
    
    // 转向速度
    public float turnSpeed = 30;
    // 旋转
    private Quaternion targetRotation;
    
    private Vector3 movement;
    // 判断是否在移动
    private bool isRunning;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movement.Set(movementInput.x,0,movementInput.y);
        // 单位化向量，得到一个移动的方向
        movement.Normalize();
        // 检查输入
        bool horizontalInput = !Mathf.Approximately(movementInput.x, 0);
        bool verticalInput = !Mathf.Approximately(movementInput.y, 0);
        isRunning = horizontalInput || verticalInput;

        if (isRunning)
        {
            movement = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * movement;
        }
        // 从当前方向朝着目标方向旋转
        Vector3 lookForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.fixedDeltaTime, 0);
        targetRotation = Quaternion.LookRotation(lookForward);
        
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        rb.MoveRotation(targetRotation);
    }
}
