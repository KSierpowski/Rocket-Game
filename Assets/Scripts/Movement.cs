using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour { 

    public float mainThrust = 100f;
    public float rotationThrust = 10f;


    public Rigidbody rb;
    public AudioSource audioSource;
    public AudioClip mainEngine;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = rb.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {

        if (Input.GetKey(KeyCode.Space))
        {

            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);  //RelativeForce da sile konkretnemu kierunkowi w tym przypadku vector3.up to 0, 1, 0 
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else 
        { 
            audioSource.Stop(); 
        }
    }

    void ProcessRotation()

        
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))  //else if dziala tylko dla tego wyzej 
        {
            ApplyRotation(-rotationThrust);
        }   
    }

    private void ApplyRotation(float liczba)                         // nazwalismy sobie to liczba bo przez coœ trzeba pomnozyc, 
                                                                                // a ¿eby da³o minusa to wy¿eh wo³amy rotationThrist któe ma u nas wartoœæ 1
    {
        rb.freezeRotation = true; //freezerotation wiec mozemy recznie rotate
        transform.Rotate(Vector3.forward * liczba * Time.deltaTime);  //foward to 0, 0, 1
        rb.freezeRotation = false; // generalnie chodzi o to ze gdy udezymy w cos to nie tracimy kontroli nad rakieta   
    }



}
