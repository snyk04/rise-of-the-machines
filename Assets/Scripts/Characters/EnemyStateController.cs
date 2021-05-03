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
        private GameState gameState;

        private State currentState;

        #endregion

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyController = GetComponent<EnemyController>();
            enemy = enemyController.GetEnemySo();
        }
        private void Start()
        {
            gameState = GameState.Instance;
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
                if (vectorBetweenEnemyAndPlayer.magnitude <= enemy.fightStartDistance)
                {
                    enemyController.Animator.SetBool("IsFighting", true);
                    enemyController.Animator.SetBool("IsIdle", false);
                    ChangeState(State.Battle, FightPlayer());
                }
                else
                {
                    enemyController.Animator.SetBool("IsIdle", false);
                    enemyController.Animator.SetBool("IsMoving", true);
                    ChangeState(State.Pursuit, PursuitPlayer());
                }

                gameState.AddTriggeredEnemies(1);
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
                    enemyController.Animator.SetBool("IsIdle", true);
                    enemyController.Animator.SetBool("IsMoving", false);
                    gameState.RemoveTriggeredEnemies(1);
                    break;
                }
                else if (distanceVectorLength <= enemy.fightStartDistance)
                {
                    navMeshAgent.isStopped = true;
                    ChangeState(State.Battle, FightPlayer());
                    enemyController.Animator.SetBool("IsFighting", true);
                    enemyController.Animator.SetBool("IsMoving", false);
                    break;
                }

                navMeshAgent.destination = Player.Instance.Transform.position;
            }
        }
        private IEnumerator FightPlayer()
        {
            // TODO: добавить проверку на то, жив ли игрок
            while (currentState == State.Battle)
            {
                if ((Player.Instance.Transform.position - transform.position).magnitude <= enemy.fightStopDistance)
                {
                    float amountOfDamage = enemy.damagePerHit;
                    amountOfDamage += amountOfDamage * (2 * Random.value - 1) * damageError;
                    Player.Instance.takeDamage(amountOfDamage);
                    yield return new WaitForSeconds(enemy.attackInterval);
                }
                else
                {
                    ChangeState(State.Pursuit, PursuitPlayer());
                    enemyController.Animator.SetBool("IsFighting", false);
                    enemyController.Animator.SetBool("IsIdle", false);
                    enemyController.Animator.SetBool("IsMoving", true);
                }
            }
        }

        private void ChangeState(State state, IEnumerator coroutine)
        {
            currentState = state;
            StartCoroutine(coroutine);
        }
    }
}
