using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadStats : MonoBehaviour
{
    // Start is called before the first frame update
    private int score, fuel;
    public Text scoreText, fuelText;
    void OnEnable()
    {
        score = PlayerPrefs.GetInt("score");
        fuel = PlayerPrefs.GetInt("fuel");
    }


    void Start()
    {
        scoreText.text = score + "m";
        fuelText.text = fuel + "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
