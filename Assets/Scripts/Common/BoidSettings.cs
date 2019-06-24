using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boid setting", menuName = "Boid setting/Basic")]
public class BoidSettings : ScriptableObject
{
    public float maxSpeed;
    public float slowingRadius;
    public float mass;
}
