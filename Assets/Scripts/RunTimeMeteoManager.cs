using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTimeMeteoManager : MonoBehaviour
{
    private ArrayList curentMeteo;

    // Start is called before the first frame update
    void Start()
    {
        curentMeteo = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addMeteo(GameObject meteo)
    {
        curentMeteo.Add(meteo);
    }

    public void removeMeteo(GameObject meteo)
    {
        curentMeteo.Remove(meteo);
    }

    public void removeAllRemaining()
    {
        curentMeteo.Clear();
    }

    public ArrayList getMeteos()
    {
        return curentMeteo;
    }
}
