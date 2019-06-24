using RIT.AI.Flocking;
using UnityEngine;

public class PursuitManager : MonoBehaviour
{
    [SerializeField] FlockingAgent Wanderer;
    [SerializeField] FlockingAgent Pursuiver;
    void Start()
    {
        Wanderer.AddFlocking(new WanderStrategy(Wanderer, 1, new WanderParams(Vector3.zero, 10, .2f, 1f, 1f)));
        Pursuiver.AddFlocking(new PursuitStrategy(Pursuiver, 1f, Wanderer));
    }
}
