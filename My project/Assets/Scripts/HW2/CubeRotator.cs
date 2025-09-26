using System.Collections.Generic;
using UnityEngine;
using System;
public class CubeRotator : MonoBehaviour
{
    [SerializeField] private GameObject cubeToCopy;
    [SerializeField] private Transform spawnPlace;
    [SerializeField] private float radius = 3f;
    [SerializeField] private float orbitRotationSpeed = 45f;
    [SerializeField] private float selfRotationSpeed = 45f;
    [SerializeField] private float rotateDirection = -1f;
    [SerializeField] private int cubeCount = 4;

    private float currentAngle = 0f;
    private List<Transform> spawnedCubes = new List<Transform>();

    private void Awake()
    {
        if (cubeToCopy == null || spawnPlace == null)
        {
            Debug.LogError("Не задан префаб куба или точка спавна!");
            return;
        }

        float angleStep = 360f / cubeCount;

        for (int i = 0; i < cubeCount; i++) // Создание клонов из префаба
        {
            GameObject cube = Instantiate(cubeToCopy, spawnPlace);
            Transform cubeTransform = cube.transform;

            float angle = i * angleStep;
            Vector3 offset = GetPositionOnCircle(angle, radius);
            cubeTransform.localPosition = offset;

            spawnedCubes.Add(cubeTransform);
        }
    }

    void Update()
    {
        if (spawnedCubes.Count == 0) return;

        currentAngle += orbitRotationSpeed * Time.deltaTime * rotateDirection;

        for (int i = 0; i < spawnedCubes.Count; i++) // Расстановка кубов
        {
            float initialAngle = (360f / cubeCount) * i; // Нач позиция куба
            float totalAngle = currentAngle + initialAngle;

            Vector3 newPos = GetPositionOnCircle(totalAngle, radius);
            spawnedCubes[i].localPosition = newPos;

            // Крутим по своей оси
            spawnedCubes[i].Rotate(Vector3.up, selfRotationSpeed * Time.deltaTime);
        }
    }

    private Vector3 GetPositionOnCircle(float angleDegrees, float radius) // Позиция на окружности
    {
        float rad = angleDegrees * Mathf.Deg2Rad;
        float x = Mathf.Cos(rad) * radius;
        float z = Mathf.Sin(rad) * radius;
        return new Vector3(x, 0, z);
    }
}
