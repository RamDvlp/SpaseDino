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
    public int scoreBoss = 100;
    private int currentScore = 0;
    public TextMeshProUGUI score;
    public GameObject lightSaber;
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
            
        }
        else if (Input.GetMouseButton(0))
        {
            MoveObject(Input.mousePosition);
        
        }
        else
        {
            MoveObject(new Vector2(100,100));
            transform.rotation = originalRotation;
            
            
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

    internal void updateBossScore()
    {
        currentScore += scoreBoss;
        score.text = "Score : " + currentScore;

    }
}
