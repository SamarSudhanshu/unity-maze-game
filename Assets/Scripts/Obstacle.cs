using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager reference is missing in Obstacle script.");
            return;
        }
        gameManager.AddPenalty();
    }
}
