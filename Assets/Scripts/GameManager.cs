using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        Playing,
        Paused,
        Won,
        Lost
    }

    private float elapsedTime;
    private int penaltyCount;
    private int currentScore;
    private Vector3 checkPointPosition;
    private GameState currentGameState;

    [Header("References")]
    [SerializeField] private Rigidbody player;
    [SerializeField] private Transform startCheckPoint;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CheckPoint[] checkPoints;
    [SerializeField] private PatrolEnemy[] patrolEnemies;
    [SerializeField] private LeaderboardManager leaderboardManager;

    [Header("Respawn Settings")]
    [SerializeField] private float respawnHeight = 0.5f;

    [Header("Score Settings")]
    [SerializeField] private int startingScore = 1000;
    [SerializeField] private int obstaclePenalty = 50;
    [SerializeField] private float timePenaltyMultiplier = 2f;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        elapsedTime = 0f;
        penaltyCount = 0;
        currentScore = startingScore;

        checkPointPosition = startCheckPoint.position + Vector3.up * respawnHeight;

        foreach (CheckPoint checkPoint in checkPoints)
        {
            checkPoint.ResetCheckPoint();
        }

        foreach (PatrolEnemy enemy in patrolEnemies)
        {
            enemy.ResetEnemy();
        }

        RespawnPlayer();

        currentGameState = GameState.Playing;
    }

    public void RestartGame()
    {
        StartGame();
        uiManager.ShowHUD();
    }

    public void PauseGame()
    {
        if (currentGameState != GameState.Playing)
        {
            return;
        }

        currentGameState = GameState.Paused;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (currentGameState != GameState.Paused)
        {
            return;
        }

        currentGameState = GameState.Playing;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (!IsPlaying)
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
        if (currentGameState == GameState.Won)
        {
            return;
        }

        Time.timeScale = 0f;
        currentGameState = GameState.Won;

        if (leaderboardManager != null)
        {
            leaderboardManager.AddScore(currentScore);
        }

        uiManager.ShowWinScreen(currentScore, elapsedTime);
    }

    public void LoseGame()
    {
        if (currentGameState == GameState.Lost)
        {
            return;
        }

        Time.timeScale = 0f;
        currentGameState = GameState.Lost;
        uiManager.ShowLoseScreen(currentScore, elapsedTime);
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

    public bool IsPlaying => currentGameState == GameState.Playing;
    public float ElapsedTime => elapsedTime;
    public int CurrentScore => currentScore;
}