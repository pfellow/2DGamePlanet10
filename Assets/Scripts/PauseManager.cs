using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject playIcon, pauseIcon, pauseForeground;
    public HeroManager heroManager;
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

    public void PauseGame()
    {
        if (playIcon.activeSelf) {
            audioData.Play();
            playIcon.SetActive(false);
            pauseIcon.SetActive(true);
            Time.timeScale = 0;
            heroManager.gamePaused = true;
            pauseForeground.SetActive(true);
        } else
        {
            playIcon.SetActive(true);
            pauseIcon.SetActive(false);
            Time.timeScale = 1;
            heroManager.gamePaused = false;
            pauseForeground.SetActive(false);
        }

    }
}
