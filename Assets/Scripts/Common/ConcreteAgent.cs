using RIT.AI.Flocking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteAgent : FlockingAgent
{
    [SerializeField] BoidSettings _settings;

    public override float MaxSpeed => Mathf.Abs(_settings.maxSpeed);
    public override float SlowingRadius => _settings.slowingRadius;
    public override float Mass => Mathf.Abs(_settings.mass);

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(_transform.position, _transform.position + Velocity.normalized * 2);
    //}
}
