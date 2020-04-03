using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;

    [SerializeField]  float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        thrust();
        rotate();
    }

    private void rotate()
    {
        //cannot rotate right and left at the same time.
        //can use rotate and thrust together

        rigidbody.freezeRotation = true;  //take manual control of rotation
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        //A key to rotate left, D key rotate left, cannot rotate both at same time.
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);

        }else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidbody.freezeRotation = false; //resume physics control of rotation. 
       
    }
    private void thrust()
    {
        
        //space key for thrust
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * mainThrust);

            if (!audioSource.isPlaying) //so audio doesnt layer on itself
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                {
                    print("friendly");
                    break;
                }
             default:
                {
                    print("destroyed");
                    break;
                }
        }
    }

}
