using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdleHandler : MonoBehaviour
{
    public GameObject[] hurdles;
    private void OnEnable()
    {
        foreach (Transform point in transform)
        {
            Instantiate(hurdles[Random.Range( 0, hurdles.Length)], point.transform);
        }
    }
}
