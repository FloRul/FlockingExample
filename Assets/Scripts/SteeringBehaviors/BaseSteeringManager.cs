using RIT.AI.Flocking;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSteeringManager : MonoBehaviour
{
    [SerializeField] protected List<FlockingAgent> _agents = new List<FlockingAgent>();

    protected abstract void Start();
}
