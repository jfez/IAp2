using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tictacAnimation : MonoBehaviour
{
    private float turnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        turnSpeed = 75f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-Vector3.forward * Time.deltaTime * turnSpeed);
    }
}
