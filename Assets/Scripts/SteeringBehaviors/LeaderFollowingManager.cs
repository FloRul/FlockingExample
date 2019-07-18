using RIT.AI.Flocking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderFollowingManager : MonoBehaviour
{
    [SerializeField] FlockingAgent Leader;
    [SerializeField] FlockingAgent[] Followers;

    void Awake()
    {
        Leader.AddFlocking(new WanderStrategy(Leader, 1, new WanderParams(Vector3.zero, 10, .05f, 1, 1)));

        for (int i = 0; i < Followers.Length; i++)
        {
            var follower = Followers[i];
            follower.AddFlocking(new LeaderFollowing(follower, 1, Leader, 1, Followers, 2));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(Leader.Position,Leader.Position - Leader.Velocity.normalized * 1);
    }
}
