using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficLight : MonoBehaviour
{
    [SerializeField]
    GameObject slightRed;
    [SerializeField]
    GameObject slightBlue;


    private float current = 0f;
    private float change;



    // Start is called before the first frame update
    void Start()
    {
       
       
    }




    // Update is called once per frame
    void Update()
    {

        float trtr = slightRed.GetComponent<Light>().intensity;
        change = Random.Range(5.0f, 9.0f);
        current += Time.deltaTime;
        if (current >= change)
        {
            current = 0f;
            change = 0f;

            if (trtr >= 10)
            {
                slightRed.GetComponent<Light>().intensity = 1f;
                slightBlue.GetComponent<Light>().intensity = 22f;
            }
            else
            {
                slightRed.GetComponent<Light>().intensity = 12f;
                slightBlue.GetComponent<Light>().intensity = 1f;
            }
            
        }

    }
}
