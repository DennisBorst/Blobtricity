using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinema : MonoBehaviour
{
    private static Cinema instance;

    private void Awake()
    {
        instance = this;
    }

    public static Cinema Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new Cinema();
            }

            return instance;
        }
    }
}
