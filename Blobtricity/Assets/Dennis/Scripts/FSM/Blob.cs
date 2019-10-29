using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum StateEnum { Idle, Walk, Follow, Netflix, Tinder, Electricity, Done }

public class Blob : MonoBehaviour, IUser
{
    public PlayerControls playerControls;
    private StateEnum stateEnum;
    private State state;

    public FSM fsm;
    public State startState;

    private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator anim;

    public GameObject blobIcon;

    [Space]
    [SerializeField] private GameObject particleSurprise;
    [SerializeField] private GameObject particleDone;

    [Space]
    public bool googleMaps = false;
    public bool netflix = false;
    public bool gamer = false;
    public bool tinder = false;
    public bool electricity = false;
    [SerializeField] private bool happyBlob = false;
    [SerializeField] private bool thisBlob = false;
    [SerializeField] private bool isBusy = false;

    private bool isDone;

    [SerializeField] private Transform[] cinemaPoints;
    [SerializeField] private Transform[] tinderBlobs;

    private Blob blob;
    private Canvas canvas;


    NavMeshAgent IUser.navMeshAgent => navMeshAgent;
    Transform[] IUser.cinemaPoints => cinemaPoints;
    Transform[] IUser.tinderBlobs => tinderBlobs;
    bool IUser.isDone => isDone;
    bool IUser.isNetflix => netflix;
    bool IUser.isBusy => isBusy;

    PlayerControls IUser.playerControls => playerControls;
    Blob IUser.blob => blob;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        canvas = GetComponentInChildren<Canvas>();
        blob = this;

        fsm = new FSM(this, StateEnum.Idle, new IdleState(StateEnum.Idle), 
                    new WalkState(StateEnum.Walk), new FollowState(StateEnum.Follow), 
                    new NetflixState(StateEnum.Netflix),
                   new TinderState(StateEnum.Tinder), new ElectricityState(StateEnum.Electricity),
                   new DoneState(StateEnum.Done));
    }

    private void Update()
    {
        if (isDone)
        {
            tinder = false;
            anim.SetTrigger("isLoving");
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

        if (tinder == true && playerControls.isBusy && !thisBlob && !playerControls.stoppedBlob)
        {
            playerControls.stoppedBlob = true;
            SoundManager.Instance.PlayBlobSound();
            isDone = true;
        }

        else if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (googleMaps == true && playerControls.isBusy == false)
            {
                playerControls.isBusy = true;
                isBusy = true;
                SoundManager.Instance.PlayBlobSound();
                particleSurprise.SetActive(true);
                UIManagement.Instance.PlaceArrow();
                fsm.SwitchState(StateEnum.Idle);
                fsm.SwitchState(StateEnum.Follow);
            }
            else if (netflix == true && playerControls.isBusy == false)
            {
                playerControls.isBusy = true;
                isBusy = true;
                particleSurprise.SetActive(true);
                UIManagement.Instance.PlaceArrow();
                SoundManager.Instance.PlayBlobSound();
                fsm.SwitchState(StateEnum.Netflix);
            }
            else if (gamer == true && playerControls.isBusy == false)
            {
                playerControls.isBusy = true;
                isBusy = true;
                particleSurprise.SetActive(true);
                UIManagement.Instance.PlaceArrow();
                SoundManager.Instance.PlayBlobSound();
                fsm.SwitchState(StateEnum.Netflix);
            }
            else if (tinder == true && playerControls.isBusy == false)
            {
                playerControls.isBusy = true;
                thisBlob = true;
                isBusy = true;
                particleSurprise.SetActive(true);
                SoundManager.Instance.PlayBlobSound();
                fsm.SwitchState(StateEnum.Tinder);
                tinder = false;
            }
            if (electricity == true && playerControls.isBusy == false)
            {
                playerControls.isBusy = true;
                particleSurprise.SetActive(true);
                SoundManager.Instance.PlayElectroBlob();
                canvas.enabled = false;
                fsm.SwitchState(StateEnum.Electricity);
            }
        }
    }

    void IUser.IsBusyFlip()
    {
        playerControls.isBusy = false;
        playerControls.stoppedBlob = false;
        electricity = false;
        thisBlob = false;
        isBusy = false;
    }

    public void PlayParticles()
    {
        particleDone.SetActive(true);
    }

    private void Animations()
    {
        if (fsm.currentState == fsm.states[StateEnum.Follow] || fsm.currentState == fsm.states[StateEnum.Netflix] || fsm.currentState == fsm.states[StateEnum.Tinder])
        {
            anim.SetTrigger("isFollowing");
            
        }
        else if (fsm.currentState == fsm.states[StateEnum.Walk])
        {
            if (electricity || happyBlob)
            {
                anim.SetTrigger("isHappy");
            }
            else
            {
                anim.SetTrigger("isWalking");
            }

            Debug.Log("Walking State animation");
        }
        else if (fsm.currentState == fsm.states[StateEnum.Idle] || fsm.currentState == fsm.states[StateEnum.Electricity])
        {
            anim.SetTrigger("isIdle");
            if (!playerControls.isBusy)
            {
                anim.SetTrigger("stoppedFollowing");
            }
            Debug.Log("Idle State animation");
        }
        else if(fsm.currentState == fsm.states[StateEnum.Done])
        {
            anim.SetTrigger("isDone");
        }
    }  
}
