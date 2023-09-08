using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroManager : MonoBehaviour
{
    private Animator anim;
    public PauseManager pauseManager;
    private Rigidbody2D rb;
    private bool onGround;
    public bool dead, godMode, levelEnded;
    public bool gamePaused;
    public int health;
    public float score;
    public int lives;
    public int fuelCollected;
    private float blinkingTime, count;
    public bool bulletIsFiring;
    public GameObject currentGun;
    public int gunState;
    private float distance;
    public Text healthText;
    public Text scoreText;
    private AudioSource audioData;
    public AudioClip jump, shot, lifeLost, gameOver;
    //public FixedJoystick joystick; for MOBILE VERSION

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        onGround = true;
        dead = false;
        health = 100;
        score = 0;
        fuelCollected = 0;
        lives = 3;
        healthText.text = health + "";
        scoreText.text = score + "";
        anim.SetInteger("Transition", 1);
        distance = 0.1f;
        
        gunState = 0;
        blinkingTime = 3f;
        count = 0f;
        gamePaused = false;
        levelEnded = false;
        audioData = GetComponent<AudioSource>();

    }

    public void IncreaseHealth(int updateParam)
    {
        int healthStart = health;
        health += updateParam;
        if (health > 100) health = 100;
        healthText.text = health + "";
        fuelCollected = fuelCollected + health - healthStart;
    }

    public void DecreaseHealth(int updateParam)
    {
        if (!godMode)
        {
            health -= updateParam;
            if (health <= 0)
            {
                
                dead = true;
                health = 0;

                lives--;

                if (lives == 2)
                {
                    healthText.transform.GetChild(0).gameObject.SetActive(false);
                }
                if (lives == 1)
                {
                    healthText.transform.GetChild(1).gameObject.SetActive(false);
                }
                if (lives == 0)
                {
                    healthText.transform.GetChild(2).gameObject.SetActive(false);
                }

                if (lives >= 0)
                {
                    audioData.PlayOneShot(lifeLost);
                } else
                {
                    audioData.PlayOneShot(gameOver);
                }
            }
            healthText.text = health + "";
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Prefab1" || collision.gameObject.name == "Prefab2" || collision.gameObject.name == "Prefab3" || collision.gameObject.name == "FinalPrefab" || collision.gameObject.name == "CrateSurface")
        {
            anim.SetInteger("Transition", 0);
            onGround = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gamePaused && !levelEnded)
        {
            if ((Input.GetKey("right")) && !dead) //  || joystick.Horizontal >= 0.2 // for MOBILE
            {
                transform.position = new Vector3(transform.position.x + Time.deltaTime * 8, transform.position.y, 0.0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            if ((Input.GetKey("left")) && !dead) //  || joystick.Horizontal <= -0.2 // for MOBILE
            {
                transform.position = new Vector3(transform.position.x - Time.deltaTime * 8, transform.position.y, 0.0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            if (!Input.GetKey("right") && !Input.GetKey("left") && onGround && !dead) //  && Mathf.Abs(joystick.Horizontal) <= 0.2 // for MOBILE
            {
                // stand animation
                anim.SetInteger("Transition", 1);
            }
            if ((Input.GetKey("right") || Input.GetKey("left")) && onGround && !dead) // || Mathf.Abs(joystick.Horizontal) >= 0.2
            {
                // running animation
                anim.SetInteger("Transition", 2);
            }
            if (Input.GetKeyDown("up") && !dead && onGround) //  || joystick.Vertical >= 0.5   // for MOBILE
            {

                anim.SetInteger("Transition", 3);
                onGround = false;                    
                audioData.PlayOneShot(jump);
                rb.AddForce(new Vector2(0f, 8.3f), ForceMode2D.Impulse);
                    

            }
            if (dead && anim.GetInteger("Transition") != 4)
            {
                anim.SetInteger("Transition", 4);               

            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("HeroFallAnimation") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && lives >= 0)
            {
                
                anim.SetInteger("Transition", 1);
                health = 100;
                healthText.text = health + "";
                dead = false;
                blinkingTime = 3f;
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("HeroFallAnimation") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && lives < 0)
            {
                
                SceneManager.LoadScene("GameEndMob");
            }


            if (transform.position.x > distance)
            {
                score = score + (transform.position.x - distance) * 20;
                scoreText.text = (int)score + "";
                distance = transform.position.x;

            }

            if (blinkingTime >= 0.3f)
            {
                count = count + Time.deltaTime;
                if (count >= 0.12f)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().enabled = !this.gameObject.GetComponent<SpriteRenderer>().enabled;

                    count = 0;
                }
                blinkingTime -= Time.deltaTime;
                godMode = true;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                godMode = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pauseManager.PauseGame();
                
        }

    }

    void OnDisable() // saving scores for the final scene
    {
        PlayerPrefs.SetInt("score", (int)score);
        PlayerPrefs.SetInt("fuel", fuelCollected);
    }
}
