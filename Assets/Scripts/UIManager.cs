using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    [Header("References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private PlayerController playerController;

    [Header("HUD")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;

    [Header("Win Screen")]
    [SerializeField] private TMP_Text winTimeText;
    [SerializeField] private TMP_Text winScoreText;

    [Header("Lose Screen")]
    [SerializeField] private TMP_Text loseTimeText;
    [SerializeField] private TMP_Text loseScoreText;

    [Header("Leaderboard")]
    [SerializeField] private TMP_Text[] leaderboardTexts;
    [SerializeField] private LeaderboardManager leaderboardManager;

    [Header("Sensitivity")]
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TMP_Text sensitivityValueText;

    private void Start()
    {
        ShowHUD();

        if (playerController != null)
        {
            sensitivitySlider.value = playerController.AccelerometerSensitivity;
            sensitivityValueText.text = $"Sensitivity: {playerController.AccelerometerSensitivity:F2}";
        }

        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    private void Update()
    {
        if (gameManager == null || !gameManager.IsPlaying)
        {
            return;
        }

        UpdateHUD();
    }

    private void UpdateHUD()
    {
        timerText.text = $"Time: {gameManager.ElapsedTime:F2} s";
        scoreText.text = $"Score: {gameManager.CurrentScore}";
    }

    public void ShowHUD()
    {
        hud.SetActive(true);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    public void ShowPauseScreen()
    {
        hud.SetActive(false);
        pauseScreen.SetActive(true);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    public void ShowWinScreen(int score, float time)
    {
        hud.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(true);
        loseScreen.SetActive(false);

        winScoreText.text = $"Score: {score}";
        winTimeText.text = $"Time: {time:F2} s";

        UpdateLeaderboard();
    }

    public void ShowLoseScreen(int score, float time)
    {
        hud.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(true);

        loseScoreText.text = $"Score: {score}";
        loseTimeText.text = $"Time: {time:F2} s";
    }

    public void OnPauseButtonPressed()
    {
        gameManager.PauseGame();
        ShowPauseScreen();
    }

    public void OnResumeButtonPressed()
    {
        gameManager.ResumeGame();
        ShowHUD();
    }

    public void OnRestartButtonPressed()
    {
        gameManager.RestartGame();
    }

    public void OnCameraButtonPressed()
    {
        cameraManager.ToggleCamera();
    }

    public void OnSensitivityChanged(float value)
    {
        if (playerController == null)
        {
            return;
        }

        playerController.AccelerometerSensitivity = value;

        sensitivityValueText.text = $"Sensitivity: {value:F2}";

        PlayerPrefs.SetFloat("Sensitivity", value);
        PlayerPrefs.Save();
    }

    public void OnMainMenuButtonPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }

    private void UpdateLeaderboard()
    {
        var scores = leaderboardManager.GetScores();

        for (int i = 0; i < leaderboardTexts.Length; i++)
        {
            if (i < scores.Count)
            {
                leaderboardTexts[i].text = $"{i + 1}. {scores[i]}";
            }
            else
            {
                leaderboardTexts[i].text = $"{i + 1}. ---";
            }
        }
    }

    private void OnDestroy()
    {
        if (sensitivitySlider != null)
        {
            sensitivitySlider.onValueChanged.RemoveListener(OnSensitivityChanged);
        }
    }
}