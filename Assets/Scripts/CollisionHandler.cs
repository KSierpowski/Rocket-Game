using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour { 
    public float levelLoadDelay = 1f;
    public AudioClip Success;
    public AudioClip Explosion;

    public ParticleSystem successParticles;
    public ParticleSystem explosionParticles;


    AudioSource audioSource;
   
     
    bool isTransitioning = false;  //false, nic sie nie dzieje gramy w gre, dopiero gdy w cos uderzymy bedzie true
    bool collisionDisabled = false;

     void Start()
    {
        audioSource = GetComponent<AudioSource>();   
       
    }

    private void Update()
    {
        DebugKeys();
    }

    void DebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //prze³¹czenie kolizji
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }    // return czyli nie robi z metdy nizej nic  
                                                                //if(isTransitionig) tak mozna zapisac ze jest true
                                                                // wo³amy collisionDisabled czyli po wlaczeniu przez C
                                                                // pomija wszystko to co ponizej
                                            
       switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;

            case "Finish":
                NexLevelSequence();
                break ;

            default:
                {
                    StartCrashSequences();
                    break;
                }

        }
 
    }

    void NexLevelSequence()
    {
        isTransitioning = true;  //gdy w cos uderzymy robi sie true
        audioSource.Stop(); 
        audioSource.PlayOneShot(Success);
        successParticles    .Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", 1f);
    }
    void StartCrashSequences()
    {
        isTransitioning = true;
        audioSource.Stop(); 
        audioSource.PlayOneShot(Explosion);
        explosionParticles.Play();
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
