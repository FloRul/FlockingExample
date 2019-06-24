namespace RIT.AI.Flocking
{
    using UnityEngine;
    public class FleeManager : BaseSteeringManager
    {
        [SerializeField] TargetGenerator _generator;

        protected override void Start()
        {
            foreach (var agent in _agents)
            {
                agent.AddFlocking(new FleeTargetStrategy(agent, 1, 5,_generator.Target));
            }
        }
    }
}
