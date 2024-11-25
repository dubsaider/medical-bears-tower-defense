using UnityEngine;
using Code.Enums;

public class Tower : MonoBehaviour
{
    public TowerType TowerType;
    public float Range;
    public float Damage;
    public float AttackSpeed;
    public Transform FirePoint;

    public ITowerAttackComponent AttackComponent { get; private set; }
    public bool IsBuilded { get; private set; }

    private float nextTimeToAttack = 0f;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.green;

        AttackComponent = GetComponent<ITowerAttackComponent>();
        if (AttackComponent == null)
        {
            Debug.LogError($"No attack component found implementing ITowerAttackComponent on Tower: {TowerType}", gameObject);
        }
    }

    void Update()
    {
        if (IsBuilded && Time.time >= nextTimeToAttack)
        {
            AttackComponent?.Attack(FirePoint, Range, Damage);
            nextTimeToAttack = Time.time + 1f / AttackSpeed;
        }
    }

    public void SetBuildStatus(bool buildStatus)
    {
        IsBuilded = buildStatus;
        spriteRenderer.color = buildStatus ? Color.white : Color.green;
        if (IsBuilded)
        {
            gameObject.tag = "Tower";
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
