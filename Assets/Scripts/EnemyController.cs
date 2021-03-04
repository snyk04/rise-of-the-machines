using System.Collections;
using UnityEngine;

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
    [SerializeField] private int checksPerSecond;
    [SerializeField] private float fieldOfView;
    [SerializeField] private float viewDistance;
    [SerializeField] private LayerMask raycastObstacleLayer;

    private State currentState = State.Patrol;

    private void Start()
    {
        StartCoroutine("FindPlayer");
    }

    private IEnumerator FindPlayer()
    {
        while (currentState == State.Patrol)
        {
            yield return new WaitForSeconds(1 / checksPerSecond);

            if (Physics.Linecast(eyezone.position, player.position, raycastObstacleLayer))
            {
                continue;
            }
            Vector3 vectorBetweenEnemyAndPlayer = player.position - eyezone.position;
            if (!(Vector3.Angle(transform.forward, vectorBetweenEnemyAndPlayer) <= fieldOfView / 2))
            {
                continue;
            }
            if (!(vectorBetweenEnemyAndPlayer.magnitude <= viewDistance))
            {
                continue;
            }

            Debug.Log($"I can see player! | Angle: {Vector3.Angle(transform.forward, vectorBetweenEnemyAndPlayer)} | Distance: {vectorBetweenEnemyAndPlayer.magnitude}");
            currentState = State.Pursuit;
        }

        yield return null;
    }
}
