using UnityEngine;

public class XRayAttackComponent : MonoBehaviour, ITowerAttackComponent
{
    private ParticleSystem pulseEffect;  // Эффект пульса
    [SerializeField] private float pulseRadius = 5f;  // Радиус волны
    [SerializeField] private float waveSpeed = 2f;    // Скорость распространения волны
    [SerializeField] private float waveAngle = 60f;   // Угол сектора волны (например, 60 градусов)
    [SerializeField] private float teleportDistance = 4f; // Расстояние притягивания врагов
    [SerializeField] private Transform firePoint;     // Точка откуда идет волна

    private float currentRadius = 0f; // Текущий радиус волны

    void Awake()
    {
        // Попытка найти компонент ParticleSystem на том же объекте
        pulseEffect = GetComponent<ParticleSystem>();

        if (pulseEffect == null)
        {
            Debug.LogError("Не найден компонент ParticleSystem на объекте " + gameObject.name);
        }
    }

    public void Attack(Transform firePoint, float range, float damage)
    {
        // Расширяем радиус волны
        currentRadius += waveSpeed * Time.deltaTime;
        if (currentRadius > pulseRadius)
        {
            currentRadius = pulseRadius;  // Останавливаем расширение волны, когда она достигнет максимального радиуса
        }

        // Запускаем эффект пульса, если он есть
        if (pulseEffect != null)
        {
            pulseEffect.transform.position = firePoint.position;
            pulseEffect.Play();
        }

        // Находим врагов в радиусе действия волны
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firePoint.position, currentRadius, LayerMask.GetMask("Enemy"));
        foreach (var enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                // Получаем направление к врагу
                Vector3 directionToTower = (firePoint.position - enemy.transform.position).normalized;

                // Проверяем, попадает ли враг в угол волны
                float angleToEnemy = Vector2.Angle(firePoint.right, directionToTower);

                if (angleToEnemy <= waveAngle / 2)  // Если враг находится в пределах сектора волны
                {
                    // Притягиваем врага на заданное расстояние
                    enemy.transform.position += directionToTower * teleportDistance;

                    // Отладочная информация
                    Debug.Log($"Enemy {enemy.name} is pulled towards the tower.");
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Отображаем радиус волны и угол в редакторе
        Gizmos.color = Color.blue;

        // Рисуем круг для радиуса
        Gizmos.DrawWireSphere(transform.position, currentRadius);

        // Рисуем сектор
        Vector3 angleStart = Quaternion.Euler(0, 0, -waveAngle / 2) * firePoint.right * currentRadius;
        Vector3 angleEnd = Quaternion.Euler(0, 0, waveAngle / 2) * firePoint.right * currentRadius;

        Gizmos.DrawLine(firePoint.position, firePoint.position + angleStart);
        Gizmos.DrawLine(firePoint.position, firePoint.position + angleEnd);
        Gizmos.DrawLine(firePoint.position + angleStart, firePoint.position + angleEnd);
    }
}
