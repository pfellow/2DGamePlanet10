using UnityEngine;

public class SecretKeyManager : MonoBehaviour
{
    public GameObject lazerToDeactivate, gunLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gunLight.GetComponent<AudioSource>().Play();
        this.gameObject.SetActive(false);
        lazerToDeactivate.SetActive(false);
    }
}
