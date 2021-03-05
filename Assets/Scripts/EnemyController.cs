using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private enum State
    {
        Patrol,
        Pursuit,
        Battle
    }

    [SerializeField] private Transform eyezone;
    [SerializeField] private Transform player;
    [Space]
    [SerializeField] private int checksPerSecondForFindPlayer;
    [SerializeField] private int checksPerSecondForPursuitPlayer;
    [SerializeField] private float fieldOfView;
    [SerializeField] private float viewDistance;
    [SerializeField] private float fightStartDistance;
    [SerializeField] private float fightStopDistance;
    [SerializeField] private LayerMask raycastObstacleLayer;

    private NavMeshAgent navMeshAgent;

    private State currentState;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        ChangeState(State.Patrol, FindPlayer());
    }

    private IEnumerator FindPlayer()
    {
        while (currentState == State.Patrol)
        {
            yield return new WaitForSeconds(1 / checksPerSecondForFindPlayer);

            if (Physics.Linecast(transform.position, player.position, raycastObstacleLayer))
            {
                continue;
            }
            Vector3 vectorBetweenEnemyAndPlayer = player.position - transform.position;
            if (Vector3.Angle(transform.forward, vectorBetweenEnemyAndPlayer) > fieldOfView / 2)
            {
                continue;
            }
            if (vectorBetweenEnemyAndPlayer.magnitude > viewDistance)
            {
                continue;
            }

            ChangeState(State.Pursuit, PursuitPlayer());
        }

        yield return null;
    }
    private IEnumerator PursuitPlayer()
    {
        navMeshAgent.Warp(transform.position);
        while (currentState == State.Pursuit)
        {
            yield return new WaitForSeconds(1 / checksPerSecondForPursuitPlayer);

            float distanceVectorLength = (player.position - transform.position).magnitude;
            if (distanceVectorLength > viewDistance * 1.5f)
            {
                navMeshAgent.isStopped = true;
                ChangeState(State.Patrol, FindPlayer());
            }
            else if (distanceVectorLength <= fightStartDistance)
            {
                navMeshAgent.isStopped = true;
                ChangeState(State.Battle, FightPlayer());
            }
            navMeshAgent.destination = player.position;
        }
    }
    private IEnumerator FightPlayer()
    {
        while ((player.position - transform.position).magnitude <= fightStopDistance)
        {
            yield return new WaitForSeconds(1);
        }
        ChangeState(State.Pursuit, PursuitPlayer());
    }

    private void ChangeState(State state, IEnumerator coroutine)
    {
        currentState = state;
        StartCoroutine(coroutine);
    }
}
