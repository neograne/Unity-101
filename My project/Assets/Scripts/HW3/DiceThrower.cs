using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DiceThrower : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private ScoreManager scoreManager;

    private void OnEnable()
    {
        inputManager.throwEvent.AddListener(OnThrow);
    }

    private void OnDisable()
    {
        inputManager.throwEvent.RemoveListener(OnThrow);
    }

    private void OnThrow()
    {
        foreach (var cube in spawner.spawnedDices)
        {
            Rigidbody rb = cube.GetComponent<Rigidbody>();
            Vector3 throwVector = new Vector3(Random.Range(-10f, 10f),
                Random.Range(0f, 10f), Random.Range(-10f, 10f));
            Vector3 torqueVector = new Vector3(Random.Range(-10f, 10f),
                Random.Range(0f, 10f), Random.Range(-10f, 10f));

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.AddTorque(torqueVector, ForceMode.Impulse);
            rb.AddForce(throwVector, ForceMode.Impulse);
        }
        Debug.Log("Кубики подброшены");

        scoreManager?.StartScoringAfterThrow();
    }
}
