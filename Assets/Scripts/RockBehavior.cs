using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehavior : MonoBehaviour
{
    public GameObject lightSaber;
    public Camera mainCamera;
    private float direction;
    public int requiredSwipes = 3;
    private int currentSwipes = 0;
    public int hitDistance = 2;
    private bool isIn = false;
    private float rotationSize = 90f;
    private int angledir = -1;
    public GameObject curentMeteo;
    private Quaternion originalRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = Random.Range(-10, 10);
        direction = direction / Mathf.Abs(direction);
        originalRotation = transform.rotation;
    }

    public void handleSwipes()
    {
        currentSwipes++;
        if (angledir > 0)
        {
            rotationSize = 90;
        } else
        {
            rotationSize = -90;
        }
        angledir = angledir * (-1);
        lightSaber.transform.Rotate(0, 0, rotationSize);
        
        if (currentSwipes >= requiredSwipes)
        {
            currentSwipes = 0;
            // TODO - substitute with switch case
            if (requiredSwipes == 1) // meaning human
            {
                curentMeteo.GetComponent<RunTimeMeteoManager>().removeMeteo(this.gameObject);
                Destroy(gameObject);
                return; // do not update score
            }
            
            if(requiredSwipes >= 5) // meaning its a boss
            {
                lightSaber.GetComponent<LightSaberMoveWithMouse>().updateBossScore();
                curentMeteo.GetComponent<RunTimeMeteoManager>().isBossOut = false;
            }
            else
            {
                lightSaber.GetComponent<LightSaberMoveWithMouse>().updateScore();
            }
            curentMeteo.GetComponent<RunTimeMeteoManager>().removeMeteo(this.gameObject);
            Destroy(gameObject);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkLightSaberHit();
        if (!gameObject.CompareTag("Human")) 
        { 
        transform.Rotate(Vector3.up, 50 * Time.deltaTime * direction, Space.World);
        transform.Rotate(Vector3.right, 50 * Time.deltaTime * direction, Space.World);
        transform.Rotate(Vector3.forward, 50 * Time.deltaTime * direction, Space.World);
        } 
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -85, 0));
        }

    }

    private void checkLightSaberHit()
    {
        
        Vector3 cursorPos = mainCamera.WorldToScreenPoint(lightSaber.transform.position);
        Vector3 meteorPos = mainCamera.WorldToScreenPoint(transform.position);
        float allowedDepthDifference = 25.0f; // Depth tolerance

        // Convert positions to 2D for comparison, but consider Z depth within a tolerance
        if (!isIn)
        {
            if (Mathf.Abs(cursorPos.z - meteorPos.z) < allowedDepthDifference &&
            Vector2.Distance(new Vector2(cursorPos.x / 100, cursorPos.y / 100), new Vector2(meteorPos.x / 100, meteorPos.y / 100)) < hitDistance)
            {
                // Handle collision
                handleSwipes();
                isIn = true;
            }
        }
        else
        {
            if (Mathf.Abs(cursorPos.z - meteorPos.z) < allowedDepthDifference &&
            Vector2.Distance(new Vector2(cursorPos.x / 100, cursorPos.y / 100), new Vector2(meteorPos.x / 100, meteorPos.y / 100)) > hitDistance)
            {
                isIn = false;
            }


        }
        
    }
}
