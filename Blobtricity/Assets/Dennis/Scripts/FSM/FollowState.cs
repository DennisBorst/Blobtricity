using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowState : State
{
    private int decreaseEnergy = 25;

    private int maxDistanceToLocation = 2;
    private int maxDistanceToPlayer = 7;

    private int distanceToLocation;
    private int distanceToPlayer;

    private float goalRadius = 50;
    private Vector3 finalPostion;

    private bool destinationReached = false;

    public FollowState(StateEnum id)
    {
        this.id = id;
    }

    public override void OnEnter(IUser _iUser)
    {
        base.OnEnter(_iUser);
        SetDestination();
    }

    public override void OnExit()
    {
        Debug.Log("Exit Time");
    }

    public override void OnUpdate()
    {
        if (!destinationReached)
        {
            Following();
        }
        else
        {
            DestinationReached();
        }
    }

    private void Following()
    {
        Debug.DrawRay(_iUser.transform.position, finalPostion.normalized, Color.red);

        distanceToLocation = Convert.ToInt32((Vector3.Distance(_iUser.transform.position, finalPostion)));
        distanceToPlayer = Convert.ToInt32((Vector3.Distance(_iUser.navMeshAgent.destination, PlayerPosition.Instance.transform.position)));

        if (_iUser.transform.position == finalPostion || distanceToLocation <= maxDistanceToLocation)
        {
            UIManagement.Instance.DecreaseEnergy(decreaseEnergy);
            UIManagement.Instance.DestroyDestinationVisual();
            Debug.Log("I have reached my destination");
            destinationReached = true;
        }

        if (distanceToPlayer >= maxDistanceToPlayer)
        {
            _iUser.navMeshAgent.destination = PlayerPosition.Instance.transform.position;
            //_iUser.navMeshAgent.destination = finalPostion;
        }
    }

    private void SetDestination()
    {
        Vector3 _randomDirection = UnityEngine.Random.insideUnitSphere * goalRadius + _iUser.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(_randomDirection, out hit, goalRadius, 1);
        finalPostion = hit.position;
        Debug.Log(finalPostion);

        UIManagement.Instance.DrawDestinationVisual(finalPostion);
    }

    private void DestinationReached()
    {
        Debug.Log("I have made it to my destination");
    }
}
