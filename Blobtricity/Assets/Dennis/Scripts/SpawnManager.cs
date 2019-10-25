using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] happyBlobMaps;
    [SerializeField] private GameObject[] happyBlobNetflix;
    [SerializeField] private GameObject[] happyBlobTinder;

    private int happyBlobMapsCount = 0;
    private int happyBlobNetflixCount = 0;
    private int happyBlobTinderCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < happyBlobMaps.Length; i++)
        {
            happyBlobMaps[i].SetActive(false);
        }

        for (int i = 0; i < happyBlobNetflix.Length; i++)
        {
            happyBlobNetflix[i].SetActive(false);
        }

        for (int i = 0; i < happyBlobTinder.Length; i++)
        {
            happyBlobTinder[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnHappyBlob(int blob)
    {
        if(blob == 1)
        {
            happyBlobMapsCount++;
            if(happyBlobMapsCount < happyBlobMaps.Length) { happyBlobMaps[happyBlobMapsCount - 1].SetActive(true); }
        }
        if (blob == 2)
        {
            happyBlobNetflixCount++;
            if (happyBlobNetflixCount < happyBlobNetflix.Length) { happyBlobNetflix[happyBlobNetflixCount - 1].SetActive(true); }
        }
        if (blob == 3)
        {
            happyBlobTinderCount++;
            if (happyBlobTinderCount < happyBlobTinder.Length) { happyBlobTinder[happyBlobTinderCount - 1].SetActive(true); }
        }
    }

    #region Singleton
    private static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static SpawnManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new SpawnManager();
            }

            return instance;
        }
    }
    #endregion
}
