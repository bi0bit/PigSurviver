using System;
using UnityEngine;
using static Farmer;

public class Dog : Enemy
{

    public override void SetProvider(ITargetsProvider provider)
    {
        var resultProvider = new PatrolFromRandomPoint(provider, 1f);
        base.SetProvider(resultProvider);
    }
}
