﻿using System.Collections;
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
        _iUser.navMeshAgent.destination = _iUser.transform.position;
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
