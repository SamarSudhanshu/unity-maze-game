using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameManager gameManager;
    private Transform currentTarget;
    private void Start()
    {
        currentTarget = pointB;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        if (transform.position == currentTarget.position)
        {
            currentTarget = currentTarget == pointA ? pointB : pointA;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameManager == null)
            {
                Debug.LogError("GameManager reference is missing.");
                return;
            }
            gameManager.AddPenalty();
            gameManager.RespawnPlayer();
        }
    }
}
