using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
// Add this when use event API
using UnityEngine.Events;

public class Food : MonoBehaviour
{
    // Modified the isDestroy 
    public void OnEnable()
    {
        isDestroy = false;
    }

    // Add destroy event
    public bool isDestroy;
    public UnityEvent destroyEvent = new UnityEvent();
    
    // Destroy food object when colliding ground
    private void OnCollisionEnter(Collision collisionInfo)
    {
        // Destroy object
        if (collisionInfo.gameObject.CompareTag("Ground")&&!isDestroy)
        {
            isDestroy = true;
            destroyEvent?.Invoke();
        }

        throw new NotImplementedException();
    }
}
