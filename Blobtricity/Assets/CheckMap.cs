using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMap : MonoBehaviour
{
    public GameObject canvas;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.M))
        {
            canvas.SetActive(true);
        }
    }

    private static CheckMap instance;

    private void Awake()
    {
        instance = this;
        canvas.SetActive(false);
    }

    public static CheckMap Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new CheckMap();
            }

            return instance;
        }
    }
}
