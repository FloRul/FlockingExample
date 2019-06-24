using RIT.AI.Flocking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    public Target Target { get; private set; }
    [SerializeField] float _targetRefreshRate = 1;
    [SerializeField] float _targetAreaRadius = 1;

    private void Awake()
    {
        Target = new Target(Vector3.zero);
        StartCoroutine(_SetTargetOverTime());
    }

    IEnumerator _SetTargetOverTime()
    {
        while (gameObject)
        {
            Target.Position = Random.insideUnitSphere * ((_targetAreaRadius >= 0) ? Mathf.Abs(_targetAreaRadius) : 1);
            yield return new WaitForSeconds((_targetRefreshRate >= 0) ? Mathf.Abs(_targetRefreshRate) : 1);
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(Target.Position, .2f);
        }
    }
}
