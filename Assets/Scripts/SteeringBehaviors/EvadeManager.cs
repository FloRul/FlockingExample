using RIT.AI.Flocking;
using UnityEngine;

public class EvadeManager : MonoBehaviour
{
    [SerializeField] FlockingAgent Wanderer;
    [SerializeField] FlockingAgent[] Evaders;

    void Start()
    {
        Wanderer.AddFlocking(new WanderStrategy(Wanderer, 1, new WanderParams(Vector3.zero, 8, .2f, 1f, 1f)));
        foreach (var agent in Evaders)
        {
            agent.AddFlocking(new EvadeStrategy(agent, 1, 12f, Wanderer));
            agent.AddFlocking(new WanderStrategy(Wanderer, .5f, new WanderParams(Vector3.zero, 5, .2f, 1f, 1f)));
        }
    }
}
