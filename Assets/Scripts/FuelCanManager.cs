using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public HeroManager heroCopy;
    public GameObject fuelCan;
    private AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (heroCopy.health < 100 && fuelCan.gameObject.activeSelf)
        {

            audioData.Play();
            heroCopy.IncreaseHealth(10);
            fuelCan.SetActive(false);            
        }
        
    }
}
