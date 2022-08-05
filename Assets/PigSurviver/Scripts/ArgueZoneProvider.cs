using UnityEngine;

public class ArgueZoneProvider : ITargetsProvider
{
    private float _zoneDistance;

    private Transform _zone;

    private int _mask;

    private bool isCachedTarget = true;
    private Transform _cacheTarget = null;

    private int _priority = 1;

    public ArgueZoneProvider(float zoneDistance, Transform zone, int mask)
    {
        _zone = zone;
        _zoneDistance = zoneDistance;
        _mask = mask;
    }
        
    public Vector2 ProvideTarget()
    {
        if (_cacheTarget != null)
            return _cacheTarget.position;
        Collider2D overlapCircle = Physics2D.OverlapCircle(_zone.position, _zoneDistance, _mask);
        if (overlapCircle != null && overlapCircle.TryGetComponent(out Pig pig))
        {
            _priority = 2;
            if (isCachedTarget)
                _cacheTarget = pig.transform;
            return pig.transform.position;
        }
        _priority = -1;
        return Vector2.negativeInfinity;
    }

    public int ProvidePriority()
    {
        return _priority;
    }
}