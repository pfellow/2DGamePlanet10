using UnityEngine;

public class LazerManager : MonoBehaviour
{
    public HeroManager hero;
    private float counter;
    private AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        counter = counter + Time.deltaTime;
        if (counter >= 0.2f)
        {
            if (!hero.godMode && !hero.dead)
            {
                audioData.Play(0);
                hero.DecreaseHealth(5);
                counter = 0;
            }
            
        }
        
    }
}
