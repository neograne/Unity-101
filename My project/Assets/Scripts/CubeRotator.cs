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
            Debug.LogError("�� ����� ������ ���� ��� ����� ������!");
            return;
        }

        float angleStep = 360f / cubeCount;

        for (int i = 0; i < cubeCount; i++) // �������� ������ �� �������
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

        for (int i = 0; i < spawnedCubes.Count; i++) // ����������� �����
        {
            float initialAngle = (360f / cubeCount) * i; // ��� ������� ����
            float totalAngle = currentAngle + initialAngle;

            Vector3 newPos = GetPositionOnCircle(totalAngle, radius);
            spawnedCubes[i].localPosition = newPos;

            // ������ �� ����� ���
            spawnedCubes[i].Rotate(Vector3.up, selfRotationSpeed * Time.deltaTime);
        }
    }

    private Vector3 GetPositionOnCircle(float angleDegrees, float radius) // ������� �� ����������
    {
        float rad = angleDegrees * Mathf.Deg2Rad;
        float x = Mathf.Cos(rad) * radius;
        float z = Mathf.Sin(rad) * radius;
        return new Vector3(x, 0, z);
    }
}
