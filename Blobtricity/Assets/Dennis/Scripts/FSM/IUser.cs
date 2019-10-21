﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IUser
{
    NavMeshAgent navMeshAgent { get; }
    Transform transform { get; }
}
