using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthHit : MonoBehaviour
{
    public GameObject Life;
    private string[] lifeIcon;
    private int lifeCount = 4;
    public GameObject spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        lifeIcon = new string[] { "life1", "life2", "life3", "life4" };

    }

 

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
            if (other.gameObject.CompareTag("Meteor"))
            {
            if (lifeCount > 0)
            {
                lifeCount--;
                Life.transform.Find(lifeIcon[lifeCount]).gameObject.SetActive(false);

            }
            if (lifeCount <= 0)
            {
                spawnManager.GetComponent<SpawnMeneger>().gameOver();

            }
                if (other.gameObject.GetComponent<RockBehavior>())
                {
                
                spawnManager.GetComponent<SpawnMeneger>().meteorsList.GetComponent<RunTimeMeteoManager>().removeMeteo(other.gameObject);
                //Destroy(other.gameObject);

                }
            }
        if (other.gameObject.CompareTag("BossMeteo"))
        {
            while (lifeCount > 0)
            {
                lifeCount--;
                Life.transform.Find(lifeIcon[lifeCount]).gameObject.SetActive(false);
            }
            spawnManager.GetComponent<SpawnMeneger>().gameOver();
            spawnManager.GetComponent<SpawnMeneger>().meteorsList.GetComponent<RunTimeMeteoManager>().removeMeteo(other.gameObject);
            //Destroy(other.gameObject);

        }
        if (other.gameObject.CompareTag("Human") && lifeCount <= 3)
        {
            Life.transform.Find(lifeIcon[lifeCount]).gameObject.SetActive(true);
            lifeCount++;
           
        }

        Destroy(other.gameObject);
    }
}
