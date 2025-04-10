using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LoadingWheel : MonoBehaviour
{
    float rotationSpeed = 0f;

    void Update()
    {
        //if (Input.GetKey(KeyCode.Space)) 
        //{
            rotationSpeed = 2f;
       // }

        transform.Rotate(0, 0, rotationSpeed);
        rotationSpeed *= 0.99f;
        
    }
}
