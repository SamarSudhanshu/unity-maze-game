using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private bool isActivated;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || isActivated)
        {
            return;
        }

        if (gameManager == null)
        {
            Debug.LogError("GameManager reference is missing.");
            return;
        }

        gameManager.SetCheckPoint(transform.position);
        isActivated = true;
    }
}
