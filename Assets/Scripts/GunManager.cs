using UnityEngine;

public class GunManager : MonoBehaviour

{
    private Animator anim;
    public GameObject bullet;
    public GameObject gunHolder;
    public GameObject bulletPlace;
    private float deltaX, deltaY, gunAngle;
    private bool gunIsVisible;
    public HeroManager heroManager;
    private AudioSource audioData;
    public AudioClip charge, fire;
    private bool chargePlayed, firePlayed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioData = GetComponent<AudioSource>();
        chargePlayed = false;
        firePlayed = false;
    }

    // Update is called once per frame
    public void restartGun()
    {
        bullet.SetActive(false);
        bullet.transform.position = new Vector2(bulletPlace.transform.position.x, bulletPlace.transform.position.y);
        anim.SetInteger("GunState", 0);
        heroManager.bulletIsFiring = false;
        heroManager.gunState = 0;
    }

    
    private void OnBecameVisible()
    {
        anim.SetInteger("GunState", 0);
        heroManager.currentGun = gameObject;
        gunIsVisible = true;
    }

    private void OnBecameInvisible()
    {
        gunIsVisible = false;
    }


    void Update()
    {
        if (gunIsVisible)
        {
            gunAngle = Mathf.Atan((transform.position.y - heroManager.transform.position.y) / (transform.position.x - heroManager.transform.position.x)) * Mathf.Rad2Deg;
            if (gunAngle <= 0) { gunAngle = 180 + gunAngle; }
            gunHolder.transform.rotation = Quaternion.Euler(0, 0, gunAngle);
        }

        if (!heroManager.bulletIsFiring && heroManager.currentGun == gameObject && heroManager.gunState == 0 && gunIsVisible)
        {
                anim.SetInteger("GunState", 1);
            heroManager.currentGun = gameObject;
            if (!chargePlayed)
            {
                audioData.PlayOneShot(charge);
                chargePlayed = true;
                firePlayed = false;
            }
                
                heroManager.gunState = 1;

            
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("GunCharge") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
                if (!firePlayed)
            {
                audioData.PlayOneShot(fire);
                firePlayed = true;
                chargePlayed = false;
            }
            
 
                anim.SetInteger("GunState", 2);
                heroManager.gunState = 2;
                      
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("GunFire") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
                deltaX = 0f;
                deltaY = 0f;
                anim.SetInteger("GunState", 3);
                heroManager.gunState = 3;
        }
        if (anim.GetInteger("GunState") == 3) {
            if (deltaX == 0f && deltaY == 0f && !heroManager.bulletIsFiring)
            {
                bullet.transform.position = new Vector2(bulletPlace.transform.position.x, bulletPlace.transform.position.y);
                deltaX = (heroManager.transform.position.x - bullet.transform.position.x);
                deltaY = (heroManager.transform.position.y - bullet.transform.position.y);
                bullet.SetActive(true);
                heroManager.bulletIsFiring = true;
            }
        }
        if (heroManager.bulletIsFiring)
        {
            bullet.transform.position = new Vector2(bullet.transform.position.x + deltaX * Time.deltaTime, bullet.transform.position.y + deltaY * Time.deltaTime);
        }        
        }         
}
