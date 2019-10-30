using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowState : State
{
    private int decreaseEnergy = 50;

    private int maxDistanceToLocation = 3;
    private int maxDistanceToPlayer = 3;

    private int distanceToLocation;
    private int distanceToPlayer;

    private float goalRadius = 50;
    private Vector3 finalPostion;

    public FollowState(StateEnum id)
    {
        this.id = id;
    }

    public override void OnEnter(IUser _iUser)
    {
        base.OnEnter(_iUser);
        SetDestination();
        _iUser.navMeshAgent.speed = 5;
    }

    public override void OnExit()
    {
        UIManagement.Instance.DestroyDestinationVisual();
        _iUser.navMeshAgent.speed = 3.5f;
        _iUser.IsBusyFlip();
    }

    public override void OnUpdate()
    {
        if (!_iUser.playerControls.isBusy)
        {
            fsm.SwitchState(StateEnum.Idle);
        }

        Following();
    }

    private void Following()
    {

        distanceToLocation = Convert.ToInt32((Vector3.Distance(_iUser.transform.position, finalPostion)));
        distanceToPlayer = Convert.ToInt32((Vector3.Distance(_iUser.navMeshAgent.destination, PlayerPosition.Instance.transform.position)));

        if (_iUser.transform.position == finalPostion || distanceToLocation <= maxDistanceToLocation)
        {
            SpawnManager.Instance.SpawnHappyBlob(1);
            SpawnManager.Instance.SpawnTree();

            UIManagement.Instance.DecreaseEnergy(decreaseEnergy);

            SoundManager.Instance.PlayHappyBlob();
            fsm.SwitchState(StateEnum.Done);
        }
        else
        {
            _iUser.navMeshAgent.destination = PlayerPosition.Instance.transform.position;
        }
    }

    private void SetDestination()
    {
        Vector3 _randomDirection = UnityEngine.Random.insideUnitSphere * goalRadius + _iUser.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(_randomDirection, out hit, goalRadius, 1);
        finalPostion = hit.position;
        Debug.Log(finalPostion);

        UIManagement.Instance.DrawDestinationVisual(finalPostion, 1);
    }
}
