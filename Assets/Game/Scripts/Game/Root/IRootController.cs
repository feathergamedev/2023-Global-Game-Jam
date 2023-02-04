using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRootController
{
    event Action<int> OnGrowAction;

    event Action OnRootCrash;

    void StartGrow();
    void StopGrow();
    void SwitchBranch();
}
