using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        Tools.DrawLine(transform, transform.position, transform.position + transform.forward * 10, Color.cyan, 0.05f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit, 100, LayerMask.GetMask("Hero"));
            Tools.DrawLine(transform, ray.origin, ray.direction + transform.forward * 10, Color.cyan, 0.05f);
            if (hit.transform)
            {
                Debug.
            }
        }

    }
}
