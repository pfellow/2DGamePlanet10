using UnityEngine;

public class MovePrefab : MonoBehaviour
{
    public HeroManager heroManager;
    public GameObject prefabToMove, finalPrefab;
    public GameObject sensorToActivate, sensorToDeactivate, fuelCanRandom, gunPieceRandom, lazerRandom, lazerRandom2, center;
    private float deltaX;

    // Start is called before the first frame update
    void Start()
    {
        deltaX = 75.7f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (prefabToMove.name == "Prefab2" || prefabToMove.name == "Prefab1") //randomizing places of fuelcan, gun, lazer
        {
            fuelCanRandom.transform.position = new Vector2(center.transform.position.x + new System.Random().Next(-6, 6), fuelCanRandom.transform.position.y);
            gunPieceRandom.transform.position = new Vector2(center.transform.position.x + new System.Random().Next(-6, 6), gunPieceRandom.transform.position.y);
            lazerRandom.transform.position = new Vector2(center.transform.position.x + new System.Random().Next(-6, 0), lazerRandom.transform.position.y);
            lazerRandom2.transform.position = new Vector2(center.transform.position.x + new System.Random().Next(3, 8), lazerRandom2.transform.position.y);
            
        }        

        if (prefabToMove.name == "Prefab3" && heroManager.score >= 5000) //initiating exit
        {
            prefabToMove.SetActive(false);
            finalPrefab.transform.position = new Vector2(prefabToMove.transform.position.x + deltaX, prefabToMove.transform.position.y);
        }
        else {
            sensorToActivate.SetActive(true);
            prefabToMove.transform.position = new Vector2(prefabToMove.transform.position.x + deltaX, prefabToMove.transform.position.y);
            sensorToDeactivate.SetActive(false);
            fuelCanRandom.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
