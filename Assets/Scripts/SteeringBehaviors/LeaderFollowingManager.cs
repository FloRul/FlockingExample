using RIT.AI.Flocking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderFollowingManager : MonoBehaviour
{
    [SerializeField] FlockingAgent Leader;
    [SerializeField] FlockingAgent[] Followers;
    NeighborQuerier _neighborQuerier;

    void Awake()
    {
        _neighborQuerier = new NeighborQuerier(Followers);
        Leader.AddFlocking(new WanderStrategy(Leader, 1, new WanderParams(Vector3.zero, 20, .05f, 1, 1)));

        for (int i = 0; i < Followers.Length; i++)
        {
            var follower = Followers[i];
            follower.AddFlocking(new LeaderFollowing(follower, 1, Leader, 1, _neighborQuerier, 5));
        }
    }

    void Update()
    {
        _neighborQuerier.UpdateWorld();
        Leader.UpdateSteering();
        foreach(FlockingAgent boid in Followers)
        {
            boid.UpdateSteering();
        }
    }
}
