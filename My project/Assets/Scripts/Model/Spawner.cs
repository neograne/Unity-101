using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private int dicesCount = 5;
    [SerializeField] public Vector3 diceStartPos = new Vector3(0, 0, 1);
    [SerializeField] UIController uiController;

    public List<GameObject> spawnedDices = new List<GameObject>();

    public int DicesCount
    {
        get => dicesCount;
        set
        {
            dicesCount = value;
        }
    }

    private void Awake()
    {
        
    }

    public void RespawnDices()
    {
        foreach (var dice in spawnedDices)
        {
            if (dice != null)
                Destroy(dice);
        }
        spawnedDices.Clear();

        for (int i = 0; i < dicesCount; i++)
        {
            GameObject cube = Instantiate(Prefab);

            if (i == 0)
                cube.transform.position = diceStartPos;
            else
                cube.transform.position = new Vector3(i + 2, 0, 1);

            spawnedDices.Add(cube);
        }
    }
}
