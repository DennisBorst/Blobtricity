using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneState : State
{

    public DoneState(StateEnum id)
    {
        this.id = id;
    }

    public override void OnEnter(IUser _iUser)
    {
        base.OnEnter(_iUser);
        Debug.Log("I am done");
        _iUser.blob.PlayParticles();
        _iUser.blob.blobIcon.SetActive(false);
        UIManagement.Instance.PlacedTree();

        _iUser.navMeshAgent.destination = _iUser.transform.position;
        _iUser.blob.googleMaps = false;
        _iUser.blob.netflix = false;
        _iUser.blob.gamer = false;
        _iUser.blob.tinder = false;
        _iUser.blob.electricity = false;

        _iUser.blob.enabled = false;
    }

    public override void OnExit()
    {
        Debug.Log("Exit Time");
    }

    public override void OnUpdate()
    {
    }
}
