using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAni : MonoBehaviour
{
    [SerializeField] Vector3 rotspeed = Vector3.up;

  
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotspeed * Time.deltaTime, Space.World);
    }
}