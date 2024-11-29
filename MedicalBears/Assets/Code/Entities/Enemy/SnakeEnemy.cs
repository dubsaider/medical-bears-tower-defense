using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEnemy : Enemy
{
    [Header("Snake Movement Settings")]
    public float segmentDistance = 0.5f; // Расстояние между сегментами
    public float bodyFollowSpeed = 10f;  // Скорость следования сегментов

    private Transform[] bodyParts; // Массив сегментов тела
    private List<Vector3> targetPositions; // Целевые позиции для сегментов

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        InvokeRepeating("FindNearestTower", 1f, 1f);
        DealCorruption();
        InitializeBodyParts();

        // Инициализация целевых позиций
        targetPositions = new List<Vector3>();
        foreach (var bodyPart in bodyParts)
        {
            targetPositions.Add(bodyPart.position);
        }
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            UpdateTargetPositions();
            MoveBodyParts();
        }
    }

    // Обновляем целевые позиции для сегментов
    void UpdateTargetPositions()
    {
        // Перемещаем позицию головы
        targetPositions[0] = transform.position;

        // Для каждого сегмента устанавливаем новую целевую позицию
        for (int i = 1; i < bodyParts.Length; i++)
        {
            Vector3 previousTarget = targetPositions[i - 1]; // Целевая позиция предыдущего сегмента
            Vector3 direction = (previousTarget - targetPositions[i]).normalized;

            // Новая целевая позиция с учетом расстояния между сегментами
            targetPositions[i] = previousTarget - direction * segmentDistance;
        }
    }

    // Перемещаем сегменты к их целевым позициям
    void MoveBodyParts()
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            // Двигаем сегмент к целевой позиции
            bodyParts[i].position = Vector3.MoveTowards(
                bodyParts[i].position,
                targetPositions[i],
                bodyFollowSpeed * Time.fixedDeltaTime
            );

            // Поворачиваем сегмент в сторону движения
            if (i > 0)
            {
                Vector3 direction = targetPositions[i - 1] - bodyParts[i].position;
                if (direction != Vector3.zero)
                {
                    bodyParts[i].up = direction.normalized;
                }
            }
        }
    }

    // Инициализация сегментов тела
    void InitializeBodyParts()
    {
        List<Transform> bones = new List<Transform>();
        CollectBones(transform, bones);
        bodyParts = bones.ToArray();
    }

    void CollectBones(Transform parent, List<Transform> bones)
    {
        foreach (Transform child in parent)
        {
            if (child.name.StartsWith("bone_"))
            {
                bones.Add(child);
            }
            CollectBones(child, bones);
        }
    }
}