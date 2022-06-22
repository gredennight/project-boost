using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    float rcsThrust = 100f;

    [SerializeField]
    float engineThrust = 100f;

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

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Fren");
                break;
            case "Fuel":
                print("Fuel");
                break ;
            default:
                print("Fucking dead bro");
                break ;
        }
    }

    private void Thrust()
    {
        //speed up
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up*engineThrust);
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
}
