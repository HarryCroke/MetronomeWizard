using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotationsPerMinute = 10f;
    void Update()
    {
        transform.Rotate(0, 6f * rotationsPerMinute * Time.deltaTime, 0f);
    }
}
