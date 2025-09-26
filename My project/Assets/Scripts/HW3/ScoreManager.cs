using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    private float offset = 0.001f;

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

    private void SumScore(List<GameObject> dices)
    {
        var dicesList = spawner.spawnedDices;
        var topFacesList = new List<string>();
        var score = 0;
        topFacesList = GetTopFaces(dicesList);

        for (int i = 0; i < topFacesList.Count; i++)
        {
            score += int.Parse(topFacesList[i]);
        }

        Debug.Log($"Текущий счет: {score}");
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
