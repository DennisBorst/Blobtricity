using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : State
{
    private int maxDistanceToLocation = 2;
    private int distanceToLocation;

    private float goalRadius = 200;
    private Vector3 finalPostion;

    public WalkState(StateEnum id)
    {
        this.id = id;
    }

    public override void OnEnter(IUser _iUser)
    {
        base.OnEnter(_iUser);
        //_iUser.anim.SetTrigger("isWalking");
        SetDestination();
    }

    public override void OnExit()
    {
        Debug.Log("Exit Time");
    }

    public override void OnUpdate()
    {

        distanceToLocation = Convert.ToInt32((Vector3.Distance(_iUser.transform.position, finalPostion)));

        if (_iUser.transform.position == finalPostion || distanceToLocation <= maxDistanceToLocation)
        {
            Debug.Log("I have reached my point");
            fsm.SwitchState(StateEnum.Idle);
        }
        else if (_iUser.isDone == true)
        {
            return;
        }
        else
        {
            _iUser.navMeshAgent.destination = finalPostion;
        }
    }

    private void SetDestination()
    {
        Vector3 _randomDirection = UnityEngine.Random.insideUnitSphere * goalRadius;
        NavMeshHit hit;
        NavMesh.SamplePosition(_randomDirection, out hit, goalRadius, NavMesh.AllAreas);
        finalPostion = hit.position;
    }
}
