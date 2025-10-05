using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.WSA;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_InputField diceCount;
    [SerializeField] private TMP_InputField winScore;
    [SerializeField] private TMP_InputField drawScore;
    [SerializeField] private TMP_InputField loseScore;
    [SerializeField] private Button throwButton;

    [SerializeField] private Spawner spawner;
    [SerializeField] private ScoreManager scoreManager;

    public UnityEvent throwButtonClicked;

    private void Awake()
    {
        throwButton.onClick.AddListener(OnThrowDice);
    }

    private void OnThrowDice()
    {
        if (!int.TryParse(diceCount.text, out int count) && count > 0)
        {
            Debug.LogWarning("Некорректный ввод");
            return;
        }

        if (!int.TryParse(winScore.text, out int win) ||
            !int.TryParse(drawScore.text, out int draw) || !int.TryParse(loseScore.text, out int lose))
        {
            Debug.LogWarning("Некорректные значения для победы/ничьи/поражения.");
            return;
        }


        spawner.DicesCount = count;
        spawner.RespawnDices();

        scoreManager.SetConditions(win, draw, lose);

        scoreManager?.StartScoringAfterThrow();
        throwButtonClicked?.Invoke();
    }

}
