using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private float maxTimer = 4;
    private float timer;

    public IdleState(StateEnum id)
    {
        this.id = id;
    }

    public override void OnEnter(IUser _iUser)
    {
        base.OnEnter(_iUser);
        timer = maxTimer;
        //_iUser.navMeshAgent.isStopped = true;
    }

    public override void OnExit()
    {
        Debug.Log("Exit Time");
        //_iUser.navMeshAgent.isStopped = false;
    }

    public override void OnUpdate()
    {
        timer = Timer(timer);

        if (timer <= 0)
        {
            fsm.SwitchState(StateEnum.Walk);
        }
        //fsm.SwitchState(StateEnum.Follow);
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
