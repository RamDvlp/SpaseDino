using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SpawnMeneger : MonoBehaviour
{
    
    public GameObject meteorPrefab;
    public GameObject bossPrefab;
    public GameObject humanPrefab;
    public Transform earth;
    public float spawnInterval = 2.0f;
    public float spawnHeight = 30f;
    public float meteorSpeed = 5.0f;
    public float spawnRange = 10f;
    private float timer;
    private bool gameover = false;
    public GameObject logoGameOver;
    public GameObject meteorsList;
    private float randomHuman;


    private float randomX;
    private Vector3 spawnPosition;
    private GameObject newMeteor;
    private Vector3 moveDirection;
    private bool isSpawnBOSS = false;

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
            if (timer <= 0 && meteorsList.GetComponent<RunTimeMeteoManager>().isBossOut == false)
            {

                if(meteorsList.GetComponent<RunTimeMeteoManager>().isBossOut == false)
                {
                    SpawnMeteor();
                    if(isSpawnBOSS)
                    {
                        isSpawnBOSS = false;
                        spawnInterval = 1f;
                        spawnBoss();
                    }
                    
                }

                timer = spawnInterval;
            }
        } 
        else // game over
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

            
        } // game over

    }

    private void spawnBoss()
    {
        spawnPosition = new Vector3(0, earth.position.y + spawnHeight + 5, -2);
        newMeteor = Instantiate(bossPrefab, spawnPosition, Quaternion.identity); // create a new meteorite
        moveDirection = ((earth.position + new Vector3(0, 0, -5)) - newMeteor.transform.position).normalized; // fall toward earth
        newMeteor.GetComponent<Rigidbody>().velocity = moveDirection * meteorSpeed/3;
        meteorsList.GetComponent<RunTimeMeteoManager>().addMeteo(newMeteor);

        meteorsList.GetComponent<RunTimeMeteoManager>().isBossOut = true;
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

        randomHuman = Random.Range(1, 100);
        randomX = Random.Range(-spawnRange, spawnRange); // Randomize the X position
        

        if (randomHuman <= 10)
        {
            spawnPosition = new Vector3(randomX, earth.position.y + spawnHeight, -5);
            newMeteor = Instantiate(humanPrefab, spawnPosition, Quaternion.identity);
            moveDirection = ((earth.position + new Vector3(0, 0, -2)) - newMeteor.transform.position).normalized;
            newMeteor.GetComponent<Rigidbody>().velocity = moveDirection * meteorSpeed;
        } 
        else
        {
            spawnPosition = new Vector3(randomX, earth.position.y + spawnHeight, -2);
            newMeteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity); // create a new meteorite
            moveDirection = ((earth.position + new Vector3(0, 0, -5)) - newMeteor.transform.position).normalized; // fall toward earth
            newMeteor.GetComponent<Rigidbody>().velocity = moveDirection * meteorSpeed;

        }

        meteorsList.GetComponent<RunTimeMeteoManager>().addMeteo(newMeteor);

        if (spawnInterval >= 1.0)
        {

            spawnInterval -= 0.05f;
        }
        else
        {
            if (spawnInterval > 0.9 & spawnInterval < 1.0)
            {
                spawnInterval -= 0.02f;
            }
            else
            {
                count++;
                if (count == 5)
                {
                    isSpawnBOSS = true;
                    count = 0;
                }

            }
        }
    }

}
