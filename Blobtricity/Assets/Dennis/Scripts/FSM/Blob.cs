using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum StateEnum { Idle, Walk, Follow, Netflix, Tinder, Done }

public class Blob : MonoBehaviour, IUser
{
    public PlayerControls playerControls;
    private StateEnum stateEnum;
    private State state;

    public FSM fsm;
    public State startState;

    private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator anim;

    [SerializeField] private bool googleMaps = false;
    [SerializeField] private bool netflix = false;
    [SerializeField] private bool tinder = false;
    [SerializeField] private bool thisBlob = false;
    private bool isDone;
    private Blob[] blob;

    [SerializeField] private Transform[] cinemaPoints;
    [SerializeField] private Transform[] tinderBlobs;

    NavMeshAgent IUser.navMeshAgent => navMeshAgent;
    Transform[] IUser.cinemaPoints => cinemaPoints;
    Transform[] IUser.tinderBlobs => tinderBlobs;
    bool IUser.isDone => isDone;

    PlayerControls IUser.playerControls => playerControls;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        fsm = new FSM(this, StateEnum.Idle, new IdleState(StateEnum.Idle), 
                    new WalkState(StateEnum.Walk), new FollowState(StateEnum.Follow), 
                    new NetflixState(StateEnum.Netflix),
                   new TinderState(StateEnum.Tinder), new DoneState(StateEnum.Done));
    }

    private void Update()
    {
        if (isDone)
        {
            tinder = false;
            fsm.SwitchState(StateEnum.Done);
            Debug.Log("I found my tinder date :D");
        }

        if (fsm != null)
        {
            fsm.OnUpdate();

            Animations();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        if(tinder == true && playerControls.isBusy && !thisBlob && !playerControls.stoppedBlob)
        {
            playerControls.stoppedBlob = true;
            isDone = true;
        }
        
        else if (Input.GetKeyUp(KeyCode.E))
        {
            if (googleMaps == true && playerControls.isBusy == false)
            {
                playerControls.isBusy = true;
                fsm.SwitchState(StateEnum.Follow);
            }
            else if (netflix == true && playerControls.isBusy == false)
            {
                playerControls.isBusy = true;
                fsm.SwitchState(StateEnum.Netflix);
            }
            else if (tinder == true && playerControls.isBusy == false)
            {
                playerControls.isBusy = true;
                thisBlob = true;
                fsm.SwitchState(StateEnum.Tinder);
                tinder = false;
            }
        }
    }

    void IUser.IsBusyFlip()
    {
        playerControls.isBusy = !playerControls.isBusy;
        playerControls.stoppedBlob = false;
        thisBlob = false;
    }

    private void Animations()
    {
        if (fsm.currentState == fsm.states[StateEnum.Follow] || fsm.currentState == fsm.states[StateEnum.Netflix] || fsm.currentState == fsm.states[StateEnum.Tinder])
        {
            anim.SetTrigger("isFollowing");

        }
        else if (fsm.currentState == fsm.states[StateEnum.Walk])
        {
            anim.SetTrigger("isWalking");

            //anim.SetBool("isWalking", true);
            //anim.SetBool("isIdle", false);

            Debug.Log("Walking State animation");
        }
        else if (fsm.currentState == fsm.states[StateEnum.Idle])
        {
            anim.SetTrigger("isIdle");

            //anim.SetBool("isIdle", true);
            //anim.SetBool("isWalking", false);

            Debug.Log("Idle State animation");
        }
        else if(fsm.currentState == fsm.states[StateEnum.Done])
        {
            anim.SetTrigger("isDone");
        }
        
    }  
}
