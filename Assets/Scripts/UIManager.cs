using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        ShowStartScreen();
    }

    private void Update()
    {
        if (gameManager == null)
        {
            return;
        }

        UpdateHUD();
    }

    private void UpdateHUD()
    {
        if (gameManager != null)
        {
            timerText.text = $"Time: {gameManager.ElapsedTime:F2} s";
            scoreText.text = $"Score: {gameManager.CurrentScore}";
        }
    }

    public void ShowStartScreen()
    {
        startScreen.SetActive(true);
        hud.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    public void ShowHUD()
    {
        startScreen.SetActive(false);
        hud.SetActive(true);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    public void ShowPauseScreen()
    {
        startScreen.SetActive(false);
        hud.SetActive(false);
        pauseScreen.SetActive(true);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    public void ShowWinScreen()
    {
        startScreen.SetActive(false);
        hud.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(true);
        loseScreen.SetActive(false);
    }

    public void ShowLoseScreen()
    {
        startScreen.SetActive(false);
        hud.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(true);
    }

    public void OnPlayButtonPressed()
    {
        gameManager.StartGame();
        ShowHUD();
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
}
