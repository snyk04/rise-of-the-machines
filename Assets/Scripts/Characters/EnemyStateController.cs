using Classes;
using Classes.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Characters
{
    public class EnemyStateController : MonoBehaviour
    {
        private enum State
        {
            Patrol,
            Pursuit,
            Fight
        }

        #region Properties

        [Header("Find settings")]
        [SerializeField] private LayerMask raycastObstacleLayer;
        [SerializeField] private int checksPerSecondForFindPlayer;

        [Header("Pursuit settings")]
        [SerializeField] private int checksPerSecondForPursuitPlayer;

        [Header("Fight settings")]
        [Range(0, 1)] [SerializeField] private float damageError; 
        [SerializeField] private float smoothTime;
        [SerializeField] private float maxSpeed;
        
        private NavMeshAgent navMeshAgent;
        private EnemyController enemyController;
        private EnemyAnimation enemyAnimation;
        private EnemySO enemy;
        private GameState gameState;
        private PlayerShooting playerShooting;

        private State currentState;
        private bool isTriggered;

        private Dictionary<State, (EnemyAnimation.State, string)> stateDictionary;

        #endregion

        #region Behaviour methods

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyController = GetComponent<EnemyController>();
            enemyAnimation = GetComponent<EnemyAnimation>();
            enemy = enemyController.EnemySo;

            isTriggered = false;
            
            // если не использовать текст, а передавать прямую ссылку на корутину, то
            // она не вызовется во второй раз (видимо даже после завершения корутины ссылка на неё остаётся)
            stateDictionary = new Dictionary<State, (EnemyAnimation.State, string)>
            {
                {State.Patrol, (EnemyAnimation.State.Idle, "Patrolling")},
                {State.Pursuit, (EnemyAnimation.State.Moving, "Pursuing")},
                {State.Fight, (EnemyAnimation.State.Fighting, "Fighting")}
            };
        }
        private void Start()
        {
            gameState = GameState.Instance;
            playerShooting = PlayerShooting.Instance;
            navMeshAgent.stoppingDistance = enemy.fightStartDistance;
            
            StartPatrolling();
        }
        private void OnDisable()
        {
            DisableHearing();
        }

        #endregion

        #region Methods

        private void ChangeState(State state)
        {
            currentState = state;
            enemyAnimation.SetAnimation(stateDictionary[state].Item1);
            StartCoroutine(stateDictionary[state].Item2);
        }
        
        private void EnableHearing()
        {
            playerShooting.OnShot += ListenForShot;
        }
        private void DisableHearing()
        {
            playerShooting.OnShot -= ListenForShot;
        }

        private void StartPatrolling()
        {
            ChangeState(State.Patrol);
            EnableHearing();
            TryToUnTrigger();
        }
        private void StartPursuing()
        {
            ChangeState(State.Pursuit);
            DisableHearing();
            TryToTrigger();
        }
        private void StartFighting()
        {
            ChangeState(State.Fight);
            DisableHearing();
            TryToTrigger();
        }

        private void TryToTrigger()
        {
            if (isTriggered)
            {
                return;
            }

            isTriggered = true;
            gameState.AddTriggeredEnemies(1);
        }
        private void TryToUnTrigger()
        {
            if (!isTriggered)
            {
                return;
            }
            
            isTriggered = false;
            gameState.RemoveTriggeredEnemies(1);
        }

        private void ListenForShot()
        {
            var playerEnemyDistance = (Player.Instance.Transform.position - transform.position).magnitude;
            if (playerEnemyDistance > enemy.viewDistance)
            {
                return;
            }
            
            StartPursuing();
        }

        #endregion

        #region Coroutines

        private IEnumerator Patrolling()
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
                    StartFighting();
                }
                else
                {
                    StartPursuing();
                }
            }
        }
        private IEnumerator Pursuing()
        {
            navMeshAgent.Warp(transform.position);
            navMeshAgent.isStopped = false;

            while (currentState == State.Pursuit)
            {
                yield return new WaitForSeconds(1f / checksPerSecondForPursuitPlayer);

                float distanceVectorLength = (Player.Instance.Transform.position - transform.position).magnitude;
                if (distanceVectorLength > enemy.viewDistance)
                {
                    navMeshAgent.isStopped = true;
                    StartPatrolling();
                }
                else if (distanceVectorLength <= enemy.fightStartDistance)
                {
                    navMeshAgent.isStopped = true;
                    StartFighting();
                }

                navMeshAgent.destination = Player.Instance.Transform.position;
            }
        }
        private IEnumerator Fighting()
        {
            // TODO: добавить проверку на то, жив ли игрок
            
            var lookAtPlayerCoroutine = StartCoroutine(LookAtPlayer());
            
            while (currentState == State.Fight)
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
                    StopCoroutine(lookAtPlayerCoroutine);
                    StartPursuing();
                }
            }
        }

        private IEnumerator LookAtPlayer()
        {
            var velocity = Vector3.zero;
            while (true)
            {
                var enemyPlayerVector = Player.Instance.Transform.position - transform.position;
                transform.forward = Vector3.SmoothDamp(transform.forward, enemyPlayerVector.normalized, ref velocity, smoothTime, maxSpeed);
                yield return new WaitForEndOfFrame();
            }
        }

        #endregion
    }
}
