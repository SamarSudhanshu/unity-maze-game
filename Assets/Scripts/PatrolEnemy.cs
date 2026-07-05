using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    private enum EnemyState
    {
        Patrol,
        Chase
    }

    [Header("References")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Transform player;
    [SerializeField] private GameManager gameManager;

    [Header("Movement")]
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 3.5f;

    [Header("Detection")]
    [SerializeField] private float detectionRadius = 4f;

    private EnemyState currentState;
    private int currentPatrolIndex;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        currentPatrolIndex = 0;
        currentState = EnemyState.Patrol;
    }

    private void Update()
    {
        if (gameManager == null || !gameManager.IsPlaying)
        {
            return;
        }

        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            return;
        }

        UpdateState();

        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Chase:
                Chase();
                break;
        }
    }

    private void UpdateState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = EnemyState.Patrol;
        }
    }

    private void Patrol()
    {
        Transform target = patrolPoints[currentPatrolIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            patrolSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            currentPatrolIndex++;

            if (currentPatrolIndex >= patrolPoints.Length)
            {
                currentPatrolIndex = 0;
            }
        }
    }

    private void Chase()
    {
        Vector3 targetPosition = new Vector3(
            player.position.x,
            transform.position.y,
            player.position.z);

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            chaseSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (gameManager == null)
        {
            Debug.LogError("GameManager reference is missing.");
            return;
        }

        gameManager.AddPenalty();
        gameManager.RespawnPlayer();
    }

    public void ResetEnemy()
    {
        transform.position = startPosition;
        currentPatrolIndex = 0;
        currentState = EnemyState.Patrol;
    }
}