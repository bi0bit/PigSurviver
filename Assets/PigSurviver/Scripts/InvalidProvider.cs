using UnityEngine;

public class InvalidProvider : ITargetsProvider
{
    public Vector2 ProvideTarget()
    {
        return Vector2.negativeInfinity;
    }

    public int ProvidePriority()
    {
        return -1;
    }
}