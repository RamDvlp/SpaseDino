using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SpawnMeneger : MonoBehaviour
{
    
    public GameObject meteorPrefab;
    public GameObject bossPrefab;
    public Transform earth;
    public float spawnInterval = 2.0f;
    public float spawnHeight = 30f;
    public float meteorSpeed = 5.0f;
    public float spawnRange = 10f;
    private float timer;
    private bool gameover = false;
    public GameObject logoGameOver;
    public GameObject meteorsList;


    private float randomX;
    private Vector3 spawnPosition;
    private GameObject newMeteor;
    private Vector3 moveDirection;

    private int count = 0;



    void Start()
    {
        timer = spawnInterval; // Initialize timer to spawn the first meteor
    }

    void Update()
    {
        if (gameover == false)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                
                    SpawnMeteor();


                if (spawnInterval >= 1.0)
                {
                    
                    spawnInterval -= 0.05f;
                }
                else
                {
                    if(spawnInterval > 0.6 & spawnInterval < 1.0)
                    {
                        spawnInterval -= 0.02f;
                    }
                    else
                    {
                        count++;
                        if(count == 15)
                        {
                            spawnBoss();
                            count = 0;
                            spawnInterval = 100;
                        }
                    }
                }
                timer = spawnInterval;
            }
        } 
        else
        {
            ArrayList lst = meteorsList.GetComponent<RunTimeMeteoManager>().getMeteos();
            if (lst.Count > 0)
            {
                foreach (GameObject meteo in lst)
                {
                    
                    // TODO add with exploation animation
                    meteo.SetActive(false);
                }
            }
            meteorsList.GetComponent<RunTimeMeteoManager>().removeAllRemaining();

            
        }

    }

    private void spawnBoss()
    {
        throw new System.NotImplementedException();
    }

    public void gameOver()
    {
        if (gameover == false)
        {
            logoGameOver.GetComponent<PlayableDirector>().Play();
        }
        gameover = true;
        
    }


    void SpawnMeteor()
    {
        /*
        float randomHuman = Random.Range(1, 10);

        if (randomHuman <= 1)
        {
             randomX = Random.Range(-spawnRange, spawnRange); // Randomize the X position
             spawnPosition = new Vector3(randomX, earth.position.y + spawnHeight, -2);
             newMeteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
             moveDirection = ((earth.position + new Vector3(0, 0, -5)) - newMeteor.transform.position).normalized;
             newMeteor.GetComponent<Rigidbody>().velocity = moveDirection * meteorSpeed;
        }
        */
         randomX = Random.Range(-spawnRange, spawnRange); // Randomize the X position for spawn
         spawnPosition = new Vector3(randomX, earth.position.y + spawnHeight, -2);
         newMeteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity); // create a new meteorite
         moveDirection = ((earth.position + new Vector3(0,0,-5)) - newMeteor.transform.position).normalized; // fall toward earth
         newMeteor.GetComponent<Rigidbody>().velocity = moveDirection * meteorSpeed;
        // curenMeteo.Add(newMeteor);
         meteorsList.GetComponent<RunTimeMeteoManager>().addMeteo(newMeteor);
    }

}
