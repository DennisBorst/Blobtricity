using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Blobs")]
    [SerializeField] private GameObject[] happyBlobMaps;
    [SerializeField] private GameObject[] happyBlobNetflix;
    [SerializeField] private GameObject[] happyBlobTinder;

    [Header("Other")]
    [SerializeField] private GameObject[] tree;
    [SerializeField] private GameObject[] windmill;


    private int happyBlobMapsCount = 0;
    private int happyBlobNetflixCount = 0;
    private int happyBlobTinderCount = 0;

    private int treeCount;
    private int windmillCount;


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

        for (int i = 0; i < tree.Length; i++)
        {
            tree[i].SetActive(false);
        }

        for (int i = 0; i < windmill.Length; i++)
        {
            windmill[i].SetActive(false);
        }
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

    public void SpawnTree()
    {
        treeCount++;
        if(treeCount < tree.Length)
        {
            tree[treeCount - 1].SetActive(true);
        }
    }

    public void SpawnWindmill()
    {
        windmillCount++;
        if(windmillCount < windmill.Length)
        {
            windmill[windmillCount - 1].SetActive(true);
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
