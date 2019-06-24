using RIT.AI.Flocking;
using System.Collections.Generic;
using UnityEngine;

public class SeekManager : BaseSteeringManager
{
    [SerializeField] TargetGenerator generator;

    protected override void Start()
    {
        foreach (var agent in _agents)
        {
            agent.AddFlocking(new SeekTargetStrategy(agent, 1, generator.Target));
        }
    }
}
