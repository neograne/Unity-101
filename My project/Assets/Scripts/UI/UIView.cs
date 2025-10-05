using TMPro;
using UnityEngine;

public class UIView : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text roundResult;

    [SerializeField] private ScoreManager scoreManager;

    private void Awake()
    {
        scoreManager.OnScoreUpdated += OnScoreChanged;
        scoreManager.OnRoundResult += OnRoundResultChanged;
    }

    private void OnScoreChanged(int newScore)
    {
        scoreText.text = $"Текущий счёт: {newScore}";
    }

    private void OnRoundResultChanged(string result)
    {
        roundResult.text = $"Итог раунда - {result}";
    }

    private void OnDestroy()
    {
        scoreManager.OnScoreUpdated -= OnScoreChanged;
        scoreManager.OnRoundResult -= OnRoundResultChanged;
    }
}
