using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 确定相机看向的物体
    public GameObject cameraLook;
    public float rotationSpeed = 80;

    private float distance;

    private Vector3 resultPos;
    private Vector2 inputPos;
    private Quaternion rotation;
    private bool isClickRotate;
    //限定旋转角度
    public int yMinLimit = 3;

    public int yMaxLimit = 80;
    
    //要旋转的角度
    private float xAngle;
    private float yAngle;

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    // Start is called before the first frame update
    void Start()
    {
        //transform上挂载的是此脚本挂载的相机
        distance = Vector3.Distance(cameraLook.transform.position, transform.position);
        rotation = transform.rotation;
        
        //如何获取相机要旋转的角度？即把世界坐标里的正角度和物体的角度做对比
        xAngle = Vector3.Angle(Vector3.right, transform.right);
        yAngle = Vector3.Angle(Vector3.up, transform.up);

    }

    //相机应该在物体移动完成后再渲染，所以应该用LateUpdate
   
    void FixedUpdate()
    {
        //获取左键是否按下,光标锁定后可以直接改为true
        isClickRotate = Input.GetMouseButtonDown(0);
        //isClickRotate = true;
        //把鼠标的输入转化为轴？
        inputPos.x = Input.GetAxis("Mouse X");
        inputPos.y = Input.GetAxis("Mouse Y");
        Debug.Log(inputPos.x);
        Debug.Log(inputPos.y);
        
        /*
        光标锁定模式
        None：默认的光标模式，可以自由移动
        Locked：锁定模式，光标位于游戏窗口中心且不可见
        Confined：限制模式，光标限制在游戏窗口中，不会移动出窗口
         */
        Cursor.lockState = CursorLockMode.Locked;
        
        //鼠标按下时对相机进行旋转
        if (isClickRotate)
        {
            xAngle += inputPos.x * rotationSpeed * Time.fixedDeltaTime;
            yAngle -= inputPos.y * rotationSpeed * Time.fixedDeltaTime;
            //角度限制
            yAngle = ClampAngle(yAngle, yMinLimit, yMaxLimit);
            //相机绕x轴旋转多少度，绕y轴旋转多少度，绕z轴旋转多少度
            rotation = Quaternion.Euler(yAngle, xAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.fixedDeltaTime * 5);
        }

        //完成简单的一个相机跟进
        resultPos = cameraLook.transform.position - (rotation * Vector3.forward * distance);
        //在原位置到新位置之间做平滑过渡
        transform.position = Vector3.Lerp(transform.position, resultPos, Time.fixedDeltaTime);
        transform.position = resultPos;
    }
}
