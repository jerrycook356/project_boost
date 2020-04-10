using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // added to change scenes progmatically

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;
    

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    [SerializeField] float levelLoadDelay = 2f;

    enum State
    {
        Alive,Dying,Transcending
    }

    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();       
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
            //debug testing keys only work on Debug Build
            if (Debug.isDebugBuild)
            {
                CheckDebugKeys();
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    private void CheckDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadTheNextScene();
        }
        if (Input.GetKey(KeyCode.C))
        {
            rigidbody.detectCollisions = false;
        }
        else
        {
            rigidbody.detectCollisions = true;
        }
    }
    private void RespondToRotateInput()
    {
        //cannot rotate right and left at the same time.
        //can use rotate and thrust together

        rigidbody.angularVelocity = Vector3.zero; //remove rotation due to physics

        float rotationThisFrame = rcsThrust * Time.deltaTime;
        //A key to rotate left, D key rotate left, cannot rotate both at same time.
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);

        }else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

       
    }
    private void RespondToThrustInput()
    {
        
        //space key for thrust
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            StopApplyingThrust();
        }


    }

    private void StopApplyingThrust()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void ApplyThrust()
    {
        mainEngineParticles.Play();
        rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying) //so audio doesnt layer on itself
        {
            audioSource.PlayOneShot(mainEngine);

        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive){ return; } //ignore collisions if dead

        switch (collision.gameObject.tag)
        {
            case "Friendly": //starting pad
                {
                    break;
                }
            case "Finish": //landing pad
                {
                    StartFinishSequence();
                    break;
                }
            default:
                {
                    StartDeathSequence();
                    break;
                }
        }

      
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop(); //stop thrusting sound when dying.

        audioSource.PlayOneShot(death);
        deathParticles.Play();

        Invoke("LoadTheFirstLevel", levelLoadDelay); //delay loading the first scene;
    }

    private void StartFinishSequence()
    {
        state = State.Transcending;

        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadTheNextScene", levelLoadDelay); //delay calling method LoadTheNextScene
    }

    private void LoadTheNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void LoadTheFirstLevel()
    {
        SceneManager.LoadScene(0);//load the first level
    }

}
