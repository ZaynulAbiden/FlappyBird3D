using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    public Transform bird;

    public bool alive;

    private void Update()
    {
        while (alive)
        {
            bird.transform.Translate(transform.forward * 15);
        }

    }

}
