using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float gameTime;
    private void Update()
    {
        gameTime += Time.deltaTime;
    }
    public void WinGame()
    {
        Debug.Log($"Time: {gameTime:F2} seconds");
    }
}
