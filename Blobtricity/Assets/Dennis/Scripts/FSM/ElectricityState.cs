using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityState : State
{
    private float maxTimer = 4;
    private float timer;

    public ElectricityState(StateEnum id)
    {
        this.id = id;
    }

    public override void OnEnter(IUser _iUser)
    {
        base.OnEnter(_iUser);
        timer = maxTimer;
        SpawnManager.Instance.SpawnWindmill();
        UIManagement.Instance.energyOvertimeDecrease++;
        UIManagement.Instance.IncreaseSustainable(25);
        SpawnManager.Instance.SpawnTree();
        UIManagement.Instance.PlacedTree();
        _iUser.navMeshAgent.destination = _iUser.transform.position;
    }

    public override void OnExit()
    {
        Debug.Log("exiting electric state");
        _iUser.IsBusyFlip();
    }

    public override void OnUpdate()
    {
        timer = Timer(timer);

        if (timer <= 0)
        {
            fsm.SwitchState(StateEnum.Walk);
        }
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
