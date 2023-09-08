using UnityEngine;

public class BulletManager : MonoBehaviour
{
    
    public HeroManager heroManager;
    public GameObject gun;
    public GunManager gunManager;
    private AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        audioData = heroManager.GetComponent<AudioSource>();
    }

    private void OnBecameInvisible()
    {
        gunManager.restartGun();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -4.5f)
        {
            gunManager.restartGun();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero" && !heroManager.godMode && !heroManager.dead) {
            audioData.PlayOneShot(heroManager.shot);
            heroManager.DecreaseHealth(20);
            gunManager.restartGun();
        }
    }
}
