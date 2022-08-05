using System;

public class Farmer : Enemy
{

    public override void SetProvider(ITargetsProvider provider)
    {
        var resultProvider = new PatrolFromRandomPoint(provider, 2f);
        base.SetProvider(resultProvider);
    }
}