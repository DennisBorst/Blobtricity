using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource houseSource;
    [SerializeField] private AudioSource mapSource;
    [SerializeField] private AudioSource scaredSource;
    [SerializeField] private AudioSource scaredSource2;
    [SerializeField] private AudioSource blobSource;
    [SerializeField] private AudioSource blobHappySource;
    [SerializeField] private AudioSource blobTinderSource;
    [SerializeField] private AudioSource blobTinderSource2;
    [SerializeField] private AudioSource blobElectroSource;

    [SerializeField] private AudioClip houseSound;
    [SerializeField] private AudioClip mapSound;
    [SerializeField] private AudioClip[] scaredSound;
    [SerializeField] private AudioClip[] blobSound;
    [SerializeField] private AudioClip[] blobHappySound;
    [SerializeField] private AudioClip[] blobTinderSound;
    [SerializeField] private AudioClip[] blobElectroSound;

    private int randomNumber;
    private int randomNumber1;

    public void PlayMapSound()
    {
        mapSource.clip = mapSound;
        mapSource.Play();
    }

    public void PlayHouseSound()
    {
        houseSource.clip = houseSound;
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
        scaredSource.volume += 0.1f;
        scaredSource2.volume += 0.1f;
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

    public void PlayTinderBlob()
    {
        randomNumber = Random.RandomRange(0, blobTinderSound.Length);
        randomNumber1 = Random.RandomRange(0, blobTinderSound.Length);

        blobTinderSource.clip = blobTinderSound[randomNumber];
        blobTinderSource2.clip = blobTinderSound[randomNumber1];

        blobTinderSource.Play();
        blobTinderSource2.Play();
    }

    public void PlayElectroBlob()
    {
        randomNumber = Random.RandomRange(0, blobElectroSound.Length);
        blobElectroSource.clip = blobElectroSound[randomNumber];
        blobElectroSource.Play();
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

