using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum StateEnum { Idle, Walk, Follow, Netflix, Done }

public class Blob : MonoBehaviour, IUser
{
    public PlayerControls playerControls;

    public FSM fsm;
    public State startState;

    private NavMeshAgent navMeshAgent;

    [SerializeField] private bool googleMaps = false;
    [SerializeField] private bool netflix = false;
    [SerializeField] private bool tinder = false;

    [SerializeField] private Transform[] cinemaPoints;

    NavMeshAgent IUser.navMeshAgent => navMeshAgent;
    Transform[] IUser.cinemaPoints => cinemaPoints;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        fsm = new FSM(this, StateEnum.Idle, new IdleState(StateEnum.Idle), 
                    new WalkState(StateEnum.Walk), new FollowState(StateEnum.Follow), 
                    new NetflixState(StateEnum.Netflix), new DoneState(StateEnum.Done));
    }

    private void Update()
    {
        if (fsm != null)
        {
            fsm.OnUpdate();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if(googleMaps == true && playerControls.isBusy == false)
            {
                playerControls.isBusy = true;
                fsm.SwitchState(StateEnum.Follow);
            }
            else if(netflix == true && playerControls.isBusy == false)
            {
                playerControls.isBusy = true;
                fsm.SwitchState(StateEnum.Netflix);
            }
            else if(tinder == true && playerControls.isBusy == false)
            {
                playerControls.isBusy = true;
            }
        }
    }

    void IUser.IsBusyFlip()
    {
        playerControls.isBusy = !playerControls.isBusy;
    }
}
