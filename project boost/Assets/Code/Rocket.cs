using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    float rcsThrust = 100f;

    [SerializeField]
    float engineThrust = 1000f;

    [SerializeField] float levelLoadDelay = 1f;

    [SerializeField]
    AudioClip mainEngine;

    [SerializeField]
    AudioClip success;

    [SerializeField]
    AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;


    Rigidbody rigidBody;
    AudioSource audioSource;

    bool collisionsDisabled = false   ;
    enum State { Alive, Dying, Transcending}
    State state=State.Alive;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();

        }
        if (Debug.isDebugBuild)
        {
            RespondToDebugKey();

        }
    }
    private void RespondToDebugKey()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionsDisabled = !collisionsDisabled;//изменить состояние
        }
    }
    private void RespondToThrustInput()
    {
        //speed up
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * engineThrust * Time.deltaTime);
            //print(KeyCode.Space+"   pressed");
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
                mainEngineParticles.Play();


            }
            // print("vshhh");
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
            //print("no vshhh");


        }
    }

    private void RespondToRotateInput()
    {
        rigidBody.freezeRotation = true;

        //turn left
        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(Vector3.forward * Time.deltaTime * rcsThrust);
            //print(KeyCode.A + "   pressed");

        }
        //turn right
        else if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(-Vector3.forward * Time.deltaTime * rcsThrust);

            //print(KeyCode.D + "   pressed");

        }

        rigidBody.freezeRotation = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive || collisionsDisabled) {return;}
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Fren");
                break;
            case "Fuel":
                print("Fuel");
                break ;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                StartDeathSequence();

                break;
        }
    }

    private void StartFinishSequence()
    {
        //print("Finalochka");
        audioSource.Stop();

        audioSource.PlayOneShot(success);
        successParticles.Play();
        state = State.Transcending;

        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        state = State.Dying;
        deathParticles.Play();
        //print("Fucking dead bro");
        Invoke("LoadFirstLevel", levelLoadDelay);
    }


    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex+1;
        if (SceneManager.sceneCount >= nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
            print("Loaded Scene" + nextSceneIndex);

        }
        else
        {
            LoadFirstLevel();
            print("Loaded first scene");

        }
        /*
        //метод хуйня и не работает
        try
        {
            SceneManager.LoadScene(nextSceneIndex);
            print("Loaded Scene" + nextSceneIndex);
        }
        catch (Exception e)
        {
            LoadFirstLevel();
            print("Loaded first scene");
        }
        */
    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
    
}
