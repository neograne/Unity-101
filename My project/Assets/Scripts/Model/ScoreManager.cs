using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Spawner spawner;

    private float offset = 0.001f;
    private int score = 0;
    public Action<int> OnScoreUpdated;
    public Action<string> OnRoundResult;

    private int winCondition;
    private int drawCondition;
    private int loseCondition;

    public int Score
    {
        get => score;
    }

    public void SetConditions(int win, int draw, int lose)
    {
        winCondition = win;
        drawCondition = draw;
        loseCondition = lose;
    }

    private List<string> GetTopFaces(List<GameObject> dices)
    {
        var dicesList = spawner.spawnedDices;
        var topFacesList = new List<string>();

        foreach (var dice in dicesList)
        {
            var maxY = 0f;
            Transform topFace = null;
            foreach (Transform face in dice.transform)
            {
                float faceY = face.position.y;
                if (faceY > maxY)
                {
                    maxY = faceY;
                    topFace = face;
                }
            }
            if (topFace != null)
                topFacesList.Add(topFace.name);
        }

        return topFacesList;
    }

    private bool IsStill(List<GameObject> dices)
    {
        foreach (var dice in dices)
        {
            var rb = dice.GetComponent<Rigidbody>();
            if ((rb.linearVelocity.magnitude > offset || rb.angularVelocity.magnitude > offset))
                return false;
        }
        return true;
    }

    private string GetRoundResult(int totalScore)
    {
        if (totalScore >= winCondition)
            return "Победа";
        else if (totalScore == drawCondition)
            return "Ничья";
        else if (totalScore <= loseCondition)
            return "Поражение";
        else
            return "Что-то не так";
    }

    private void SumScore(List<GameObject> dices)
    {
        var dicesList = spawner.spawnedDices;
        var topFacesList = new List<string>();
        score = 0;

        topFacesList = GetTopFaces(dicesList);

        for (int i = 0; i < topFacesList.Count; i++)
        {
            score += int.Parse(topFacesList[i]);
        }

        Debug.Log($"Текущий счет: {score}");
        OnScoreUpdated?.Invoke(score);

        string result = GetRoundResult(score);
        OnRoundResult?.Invoke(result);
    }

    private IEnumerator TimeDelay(float delay)
    {
        while (!IsStill(spawner.spawnedDices))
        {
            yield return null;
        }
        yield return new WaitForSeconds(delay);
        SumScore(spawner.spawnedDices);
    }

    public void StartScoringAfterThrow()
    {
        StartCoroutine(TimeDelay(5f));
    }
}
