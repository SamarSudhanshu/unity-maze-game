using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class LeaderboardData
{
    public List<int> scores = new();
}

public class LeaderboardManager : MonoBehaviour
{
    private LeaderboardData leaderboardData = new();

    private string savePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");
        LoadLeaderboard();
    }

    private void LoadLeaderboard()
    {
        if (!File.Exists(savePath))
        {
            leaderboardData = new LeaderboardData();
            SaveLeaderboard();
            return;
        }

        string json = File.ReadAllText(savePath);

        leaderboardData = JsonUtility.FromJson<LeaderboardData>(json);

        if (leaderboardData == null)
        {
            leaderboardData = new LeaderboardData();
        }
    }

    private void SaveLeaderboard()
    {
        string json = JsonUtility.ToJson(leaderboardData, true);
        File.WriteAllText(savePath, json);
    }

    public void AddScore(int score)
    {
        leaderboardData.scores.Add(score);

        leaderboardData.scores.Sort((a, b) => b.CompareTo(a));

        if (leaderboardData.scores.Count > 5)
        {
            leaderboardData.scores.RemoveRange(5, leaderboardData.scores.Count - 5);
        }

        SaveLeaderboard();

        Debug.Log($"Saved score: {score}");
        Debug.Log($"Leaderboard saved at: {savePath}");
    }

    public IReadOnlyList<int> GetScores()
    {
        return leaderboardData.scores;
    }
}