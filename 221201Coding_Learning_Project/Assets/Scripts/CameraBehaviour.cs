using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Vector3 camOffset = new Vector3(0f, 2.6f, -2.6f);

    private Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Character").transform;

    }

    // Update is called once per frame
    // 因为摄像头的移动是跟随角色的移动发生的，所以摄像头的更新应该使用LateUpdate
    void LateUpdate()
    {
        transform.position = target.TransformPoint(camOffset);
        transform.LookAt(target);

    }
}
