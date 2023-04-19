using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _chaseRange = 5f;

    NavMeshAgent _navMeshAgent;
    float _distanceToTarget = Mathf.Infinity;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        _distanceToTarget = Vector3.Distance(_target.position, transform.position);
        if (_distanceToTarget <= _chaseRange)
        {
            _navMeshAgent.SetDestination(_target.position);
        }
    }
}
