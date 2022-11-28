using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if(GameManager.instance.isGameRunning)
          transform.position =  new Vector3(transform.position.x, transform.position.y,target.position.z);
    }
}
