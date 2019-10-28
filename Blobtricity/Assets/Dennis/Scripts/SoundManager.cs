using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource houseSource;
    [SerializeField] private AudioSource scaredSource;
    [SerializeField] private AudioSource scaredSource2;
    [SerializeField] private AudioSource blobSource;
    [SerializeField] private AudioSource blobHappySource;

    [SerializeField] private AudioClip[] houseSound;
    [SerializeField] private AudioClip[] scaredSound;
    [SerializeField] private AudioClip[] blobSound;
    [SerializeField] private AudioClip[] blobHappySound;

    private int randomNumber;
    private int randomNumber1;

    public void PlayHouseSound()
    {
        randomNumber = Random.RandomRange(0, houseSound.Length);
        houseSource.clip = houseSound[randomNumber];
        houseSource.Play();
    }

    public void PlayScaredBlob()
    {
        randomNumber = Random.RandomRange(0, scaredSound.Length);
        randomNumber1 = Random.RandomRange(0, scaredSound.Length);
        scaredSource.clip = scaredSound[randomNumber];
        scaredSource.Play();
        scaredSource2.clip = scaredSound[randomNumber1];
        scaredSource2.Play();
    }

    public void PlayBlobSound()
    {
        randomNumber = Random.RandomRange(0, blobSound.Length);
        blobSource.clip = blobSound[randomNumber];
        blobSource.Play();
    }

    public void PlayHappyBlob()
    {
        randomNumber = Random.RandomRange(0, blobHappySound.Length);
        blobHappySource.clip = blobHappySound[randomNumber];
        blobHappySource.Play();
    }


    #region Singleton
    private static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static SoundManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new SoundManager();
            }

            return instance;
        }
    }
    #endregion
}

