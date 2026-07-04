using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        Start,
        Playing,
        Paused,
        Won,
        Lost
    }
    private float elapsedTime;
    private GameState currentGameState = GameState.Start;
    private int penaltyCount;
    private int currentScore;
    private Vector3 checkPointPosition;

    [Header("References")]
    [SerializeField] private Rigidbody player;
    [SerializeField] private Transform startCheckPoint;
    [SerializeField] private UIManager uiManager;

    [Header("Respawn Settings")]
    [SerializeField] private float respawnHeight = 0.5f;

    [Header("Score Settings")]
    [SerializeField] private int startingScore = 1000;
    [SerializeField] private int obstaclePenalty = 50;
    [SerializeField] private float timePenaltyMultiplier = 2f;

    public void StartGame()
    {
        elapsedTime = 0f;
        penaltyCount = 0;
        currentScore = startingScore;

        currentGameState = GameState.Playing;
    }

    public void PauseGame()
    {
        currentGameState = GameState.Paused;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        currentGameState = GameState.Playing;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        currentGameState = GameState.Start;
        checkPointPosition = startCheckPoint.position + Vector3.up * respawnHeight;
    }

    private void Update()
    {
        if (currentGameState != GameState.Playing)
        {
            return;
        }

        elapsedTime += Time.deltaTime;
        UpdateCurrentScore();
    }

    // Temporary testing only
    private void FixedUpdate()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RespawnPlayer();
        }
    }

    public void WinGame()
    {
        currentGameState = GameState.Won;
        uiManager.ShowWinScreen();
    }

    public void LoseGame()
    {
        currentGameState = GameState.Lost;
        uiManager.ShowLoseScreen();
    }

    public void AddPenalty()
    {
        penaltyCount++;
    }

    public void SetCheckPoint(Vector3 position)
    {
        checkPointPosition = position + Vector3.up * respawnHeight;
    }

    public void RespawnPlayer()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is missing.");
            return;
        }

        player.linearVelocity = Vector3.zero;
        player.angularVelocity = Vector3.zero;
        player.position = checkPointPosition;
    }

    private void UpdateCurrentScore()
    {
        int timePenalty = Mathf.RoundToInt(elapsedTime * timePenaltyMultiplier);
        int obstaclePenaltyTotal = penaltyCount * obstaclePenalty;

        currentScore = Mathf.Max(
            startingScore - timePenalty - obstaclePenaltyTotal,
            0);

        if (currentScore <= 0)
        {
            LoseGame();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(checkPointPosition, 0.2f);
    }

    public float ElapsedTime => elapsedTime;
    public int CurrentScore => currentScore;
}
