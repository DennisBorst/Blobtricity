using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    [SerializeField] private GameObject thisGameObject;

    public void SetActiveFalse()
    {
        thisGameObject.SetActive(false);
    }
}
