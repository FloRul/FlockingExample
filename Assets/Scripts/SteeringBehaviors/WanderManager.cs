using RIT.AI.Flocking;
using System.Collections.Generic;
using UnityEngine;

public class WanderManager : BaseSteeringManager
{
    protected override void Start()
    {
        foreach (var agent in _agents)
        {
            agent.AddFlocking(new WanderStrategy(agent, 1, new WanderParams(Vector3.zero, 100, .1f, 1, 1)));
        }
    }

    private void Update()
    {
        foreach (var b in _agents)
        {
            b.UpdateSteering();
        }
    }
}
