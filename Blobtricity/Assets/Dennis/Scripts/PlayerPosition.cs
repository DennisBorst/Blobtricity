using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    private static PlayerPosition instance;

    private void Awake()
    {
        instance = this;
    }

    public static PlayerPosition Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new PlayerPosition();
            }

            return instance;
        }
    }
}
