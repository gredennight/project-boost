using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    

    private void Thrust()
    {
        //speed up
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            //print(KeyCode.Space+"   pressed");
            if (!audioSource.isPlaying)
            {
                audioSource.Play();

            }
        }
        else
        {
            audioSource.Stop();

        }
    }

    private void Rotate()
    {

        //turn left
        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.freezeRotation = true;

            transform.Rotate(Vector3.forward * Time.deltaTime * 100);
            print(KeyCode.A + "   pressed");
            rigidBody.freezeRotation = false;

        }
        //turn right
        else if (Input.GetKey(KeyCode.D))
        {
            rigidBody.freezeRotation = true;

            transform.Rotate(-Vector3.forward * Time.deltaTime * 100);

            print(KeyCode.D + "   pressed");
            rigidBody.freezeRotation = false;

        }


    }
}
