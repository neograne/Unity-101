using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private int dicesCount = 5;
    [SerializeField] public Vector3 diceStartPos = new Vector3(0, 0, 1);

    public List<GameObject> spawnedDices = new List<GameObject>();

    private void Awake()
    {
        if (Prefab == null)
        {
            Debug.LogError("Не задан префаб игрового кубика");
            return;
        }

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
