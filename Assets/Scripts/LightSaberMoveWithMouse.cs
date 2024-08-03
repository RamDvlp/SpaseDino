using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LightSaberMoveWithMouse : MonoBehaviour
{
    public Camera gameCamera;
    public int scoreBase = 10;
    private int currentScore = 0;
    public TextMeshProUGUI score;
    public GameObject lightSaber;
    private bool isMoved = false;
    private Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            MoveObject(touch.position);
            isMoved = true;
        }
        else if (Input.GetMouseButton(0))
        {
            MoveObject(Input.mousePosition);
            isMoved = true;
        }
        else
        {
            MoveObject(new Vector2(100,100));
            transform.rotation = originalRotation;
            //transform.LocalRotate = Quaternion.Euler(0,0,-45);
            if (!isMoved)
            {
                
            }
            
        }
    }


    private void MoveObject(Vector2 screenPosition)
    {
        Vector3 worldPosition = gameCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, gameCamera.nearClipPlane + 10));
        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z); 
    }

    internal void updateScore()
    {
        currentScore += scoreBase;
        score.text = "Score : " + currentScore;
        
    }
}
