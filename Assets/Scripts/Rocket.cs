using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;

    public float rcsThrust =1;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        processInput();
    }

    private void processInput()
    {
        //cannot rotate right and left at the same time.
        //can use rotate and thrust together

        //space key for thrust
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying) //so audio doesnt layer on itself
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        //A key to rotate left, D key rotate left, cannot rotate both at same time.
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward,rcsThrust * Time.deltaTime);

        }else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward,rcsThrust * Time.deltaTime);
        }

       
    }
}
