using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TinderState : State
{
    private int decreaseEnergy = 70;

    private int maxDistanceToLocation = 3;
    private int maxDistanceToPlayer = 3;

    private int distanceToLocation;
    private int distanceToPlayer;

    private float goalRadius = 50;
    private Vector3 finalPostion;

    private bool destinationReached = false;
    private float nearestBlob;


    public TinderState(StateEnum id)
    {
        this.id = id;
    }

    public override void OnEnter(IUser _iUser)
    {
        base.OnEnter(_iUser);
    }

    public override void OnExit()
    {
        _iUser.IsBusyFlip();
    }

    public override void OnUpdate()
    {
        if (!_iUser.playerControls.isBusy)
        {
            fsm.SwitchState(StateEnum.Idle);
        }

        if (!destinationReached && !_iUser.playerControls.stoppedBlob)
        {
            SetDestination();
            Following();
        }
        else if(!destinationReached && _iUser.playerControls.stoppedBlob)
        {
            Following();
        }
    }

    private void Following()
    {
        //Debug.DrawRay(_iUser.transform.position, finalPostion.normalized, Color.red);

        distanceToLocation = Convert.ToInt32((Vector3.Distance(_iUser.transform.position, finalPostion)));
        distanceToPlayer = Convert.ToInt32((Vector3.Distance(_iUser.navMeshAgent.destination, PlayerPosition.Instance.transform.position)));

        if (_iUser.transform.position == finalPostion || distanceToLocation <= maxDistanceToLocation)
        {
            UIManagement.Instance.DecreaseEnergy(decreaseEnergy);
            SoundManager.Instance.PlayTinderBlob();

            SpawnManager.Instance.SpawnHappyBlob(3);
            SpawnManager.Instance.SpawnTree();
            fsm.SwitchState(StateEnum.Done);
            return;
        }

        if (distanceToPlayer >= maxDistanceToPlayer)
        {
            _iUser.navMeshAgent.destination = PlayerPosition.Instance.transform.position;
            //_iUser.navMeshAgent.destination = finalPostion;
        }
    }

    private void SetDestination()
    {
        nearestBlob = CalculateClosestDistance(_iUser.tinderBlobs.Length, PlayerPosition.Instance.transform.position, _iUser.tinderBlobs);

        for (int i = 0; i < _iUser.tinderBlobs.Length; i++)
        {
            if(Vector3.Distance(_iUser.tinderBlobs[i].transform.position, PlayerPosition.Instance.transform.position) == nearestBlob)
            {
                finalPostion = _iUser.tinderBlobs[i].transform.position;
            }
        }

        Debug.Log("The closest blob is on: " + finalPostion);
    }

    private float CalculateClosestDistance(int index,Vector3 origin ,Transform[] tinderBlobs)
    {
        List<float> blobDistances = new List<float>();
        float nearestTinderBlob = 100000000f;

        for (int i = 0; i < index; i++)
        {
            if (_iUser.tinderBlobs[i] != null)
            {
                blobDistances.Add(Vector3.Distance(origin, tinderBlobs[i].position));
            }
        }

        foreach (float a in blobDistances)
        {
            if (a <= nearestTinderBlob)
            {
                nearestTinderBlob = a;
                Debug.Log(a);
            }
        }
        return nearestTinderBlob;
    }
}
