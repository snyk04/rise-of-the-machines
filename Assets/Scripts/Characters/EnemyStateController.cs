using Classes;
using Classes.ScriptableObjects;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Characters
{
    public class EnemyStateController : MonoBehaviour
    {
        private enum State
        {
            Patrol,
            Pursuit,
            Battle
        }

        #region Properties

        [Header("Find settings")]
        [SerializeField] private LayerMask raycastObstacleLayer;
        [SerializeField] private int checksPerSecondForFindPlayer;

        [Header("Pursuit settings")]
        [SerializeField] private int checksPerSecondForPursuitPlayer;

        [Header("Fight settings")]
        [Range(0, 1)] [SerializeField] private float damageError; 

        private NavMeshAgent navMeshAgent;
        private EnemyController enemyController;
        private EnemySO enemy;

        private State currentState;

        #endregion

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyController = GetComponent<EnemyController>();
            enemy = enemyController.GetEnemySO();
        }
        private void Start()
        {
            ChangeState(State.Patrol, FindPlayer());
        }

        private IEnumerator FindPlayer()
        {
            while (currentState == State.Patrol)
            {
                yield return new WaitForSeconds(1f / checksPerSecondForFindPlayer);

                if (Physics.Linecast(transform.position, Player.Instance.Transform.position, raycastObstacleLayer))
                {
                    continue;
                }
                Vector3 vectorBetweenEnemyAndPlayer = Player.Instance.Transform.position - transform.position;
                if (Vector3.Angle(transform.forward, vectorBetweenEnemyAndPlayer) > enemy.fieldOfView / 2)
                {
                    continue;
                }
                if (vectorBetweenEnemyAndPlayer.magnitude > enemy.viewDistance)
                {
                    continue;
                }

                ChangeState(State.Pursuit, PursuitPlayer());
            }
        }
        private IEnumerator PursuitPlayer()
        {
            navMeshAgent.Warp(transform.position);

            while (currentState == State.Pursuit)
            {
                yield return new WaitForSeconds(1f / checksPerSecondForPursuitPlayer);

                float distanceVectorLength = (Player.Instance.Transform.position - transform.position).magnitude;
                if (distanceVectorLength > enemy.viewDistance * 1.5f)
                {
                    navMeshAgent.isStopped = true;
                    ChangeState(State.Patrol, FindPlayer());
                    break;
                }
                else if (distanceVectorLength <= enemy.fightStartDistance)
                {
                    navMeshAgent.isStopped = true;
                    ChangeState(State.Battle, FightPlayer());
                    break;
                }

                navMeshAgent.destination = Player.Instance.Transform.position;
            }
        }
        private IEnumerator FightPlayer()
        {
            while ((Player.Instance.Transform.position - transform.position).magnitude <= enemy.fightStopDistance)
            {
                yield return new WaitForSeconds(enemy.attackInterval);
                float amountOfDamage = enemy.damagePerHit;
                amountOfDamage += amountOfDamage * (2 * Random.value - 1) * damageError;
                Player.Instance.takeDamage(amountOfDamage);
            }

            ChangeState(State.Pursuit, PursuitPlayer());
        }

        private void ChangeState(State state, IEnumerator coroutine)
        {
            currentState = state;
            StartCoroutine(coroutine);
        }
    }
}
