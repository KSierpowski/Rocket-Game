using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour { 

    public float mainThrust = 100f;
    public float rotationThrust = 10f;
    public AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;

    Rigidbody rb;
    AudioSource audioSource;
    
  
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = rb.GetComponent<AudioSource>();

    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();

        }
        else
        {
            StopThrust();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))  //else if dziala tylko dla tego wyzej 
        {
            RotateRight();
        }
        else
        {
            StopRotate();
        }
    }

    void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);  //RelativeForce da sile konkretnemu kierunkowi w tym przypadku vector3.up to 0, 1, 0 
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)  //jesli nie jest aktywny"nie gra" to nieh gra
        {
            mainEngineParticles.Play();
        }
    }
    void StopThrust()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }



    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!rightEngineParticles.isPlaying)  //jesli nie jest aktywny"nie gra" to nieh gra
        {
            rightEngineParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!leftEngineParticles.isPlaying)  //jesli nie jest aktywny"nie gra" to nieh gra
        {
            leftEngineParticles.Play();
        }
    }

    private void StopRotate()
    {
        rightEngineParticles.Stop();
        leftEngineParticles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)                         
    {
        rb.freezeRotation = true; //freezerotation wiec mozemy recznie rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);  //foward to 0, 0, 1
        rb.freezeRotation = false; // generalnie chodzi o to ze gdy udezymy w cos to nie tracimy kontroli nad rakieta   
    }



}
