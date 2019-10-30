using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NetflixState : State
{
    private int decreaseEnergyNetflix = 80;
    private int decreaseEnergyGamer = 60;

    private int maxDistanceToLocation = 3;
    private int maxDistanceToPlayer = 3;

    private int distanceToLocation;
    private int distanceToPlayer;

    private float goalRadius = 50;
    private Vector3 finalPostion;

    private float nearestCinema;


    public NetflixState(StateEnum id)
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
        _iUser.playerControls.followBlobUI.SetActive(false);
        _iUser.navMeshAgent.speed = 3.5f;
        CheckMap.Instance.canvas.SetActive(false);
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
        Debug.DrawRay(_iUser.transform.position, finalPostion.normalized, Color.red);

        distanceToLocation = Convert.ToInt32((Vector3.Distance(_iUser.transform.position, finalPostion)));
        distanceToPlayer = Convert.ToInt32((Vector3.Distance(_iUser.navMeshAgent.destination, PlayerPosition.Instance.transform.position)));

        if (_iUser.transform.position == finalPostion || distanceToLocation <= maxDistanceToLocation)
        {
            SpawnManager.Instance.SpawnHappyBlob(2);
            SpawnManager.Instance.SpawnTree();

            if (_iUser.isNetflix)
            {
                UIManagement.Instance.DecreaseEnergy(decreaseEnergyNetflix);
            }
            else
            {
                UIManagement.Instance.DecreaseEnergy(decreaseEnergyGamer);
            }

            SoundManager.Instance.PlayHappyBlob();
            fsm.SwitchState(StateEnum.Done);
        }

        if (distanceToPlayer >= maxDistanceToPlayer)
        {
            _iUser.navMeshAgent.destination = PlayerPosition.Instance.transform.position;
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

        if (_iUser.isNetflix)
        {
            UIManagement.Instance.DrawDestinationVisual(finalPostion, 2);
        }
        else
        {
            UIManagement.Instance.DrawDestinationVisual(finalPostion, 3);
        }

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
}
