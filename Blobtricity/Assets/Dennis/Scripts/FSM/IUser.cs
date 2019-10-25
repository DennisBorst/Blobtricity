using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IUser
{
    NavMeshAgent navMeshAgent { get; }
    Transform transform { get; }
    Transform[] cinemaPoints { get; }
    Transform[] tinderBlobs { get; }
    bool isDone { get; }

    PlayerControls playerControls { get; }

    void IsBusyFlip();
}
