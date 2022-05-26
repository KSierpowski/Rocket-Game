using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour { 
    public float levelLoadDelay = 1f;
    public AudioClip successFinish;
    public AudioClip collisionBoom;

    AudioSource audioSource;


     void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    void OnCollisionEnter(Collision other)
    {
       

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;

            case "Finish":
                NexLevelSequence();
                break ;
            case "Fuel":
                Debug.Log("You picked up fuel!");
                break;
            default:
                {
                    StartCrashSequences();
                    break;
                }

        }
 
        }
    void NexLevelSequence()
    {
        audioSource.PlayOneShot(successFinish);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", 1f);
    }
    void StartCrashSequences()
    {
        audioSource.PlayOneShot(collisionBoom);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 1f);  
    }
    void NextLevel()
        {
            
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)  
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }
         void ReloadLevel()
    {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);



    }







}
