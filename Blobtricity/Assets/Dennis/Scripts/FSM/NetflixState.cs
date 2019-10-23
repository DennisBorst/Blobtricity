using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NetflixState : State
{
    private int decreaseEnergy = 25;

    private int maxDistanceToLocation = 2;
    private int maxDistanceToPlayer = 7;

    private int distanceToLocation;
    private int distanceToPlayer;

    private float goalRadius = 50;
    private Vector3 finalPostion;

    private bool destinationReached = false;
    private float nearestCinema;



    public NetflixState(StateEnum id)
    {
        this.id = id;
    }

    public override void OnEnter(IUser _iUser)
    {
        base.OnEnter(_iUser);

        Debug.Log(_iUser.cinemaPoints[0].transform.position);
        Debug.Log(_iUser.cinemaPoints[1].transform.position);

        SetDestination();
    }

    public override void OnExit()
    {
        Debug.Log("Exit Time");
        _iUser.IsBusyFlip();
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
        nearestCinema = CalculateClosestDistance(_iUser.cinemaPoints.Length, _iUser.transform.position, _iUser.cinemaPoints);

        for (int i = 0; i < _iUser.cinemaPoints.Length; i++)
        {
            if(Vector3.Distance(_iUser.cinemaPoints[i].transform.position, _iUser.transform.position) == nearestCinema)
            {
                finalPostion = _iUser.cinemaPoints[i].transform.position;
            }
        }

        UIManagement.Instance.DrawDestinationVisual(finalPostion, 2);

        Debug.Log("The closest cinema is on: " + finalPostion);
    }

    private float CalculateClosestDistance(int index,Vector3 origin ,Transform[] cinemaPoints)
    {
        List<float> cinemaDistances = new List<float>();
        float nearestCinema = 100000000f;

        for (int i = 0; i < index; i++)
        {
            if (_iUser.cinemaPoints[i] != null)
            {
                Debug.Log(_iUser.cinemaPoints[i]);
                cinemaDistances.Add(Vector3.Distance(origin, cinemaPoints[i].position));
                Debug.Log(cinemaDistances[i]);
            }
        }

        foreach (float a in cinemaDistances)
        {
            if (a <= nearestCinema)
            {
                nearestCinema = a;
                Debug.Log(a);
            }
        }
        return nearestCinema;
    }

    private void DestinationReached()
    {
        fsm.SwitchState(StateEnum.Done);
        Debug.Log("I have made it to my destination");
    }
}
