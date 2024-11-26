using UnityEngine;

public class WaveProjectile : MonoBehaviour
{
    public float maxRadius = 1f; 
    public float minRadius = 0.1f; 
    public float growthSpeed = 0.5f;
    public Sprite[] waveSprites; 
    public float frameRate = 0.1f; 

    private float currentRadius = 0f;
    private int currentFrame = 0;
    private float frameTimer = 0f;
    private SpriteRenderer spriteRenderer;
    public Vector3 towerPosition;
    public float pullForce;
    public float damage;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("На объекте волны отсутствует SpriteRenderer!");
        }
    }

    private void Start()
    {
        currentRadius = minRadius;
        transform.localScale = new Vector3(currentRadius, currentRadius, 1f);
    }

    private void Update()
    {
        currentRadius += growthSpeed * Time.deltaTime;
        transform.localScale = new Vector3(currentRadius, currentRadius, 1f);

        if (currentRadius >= maxRadius/15)
        {
            Destroy(gameObject);
        }

        if (waveSprites.Length > 0)
        {
            frameTimer += Time.deltaTime;
            if (frameTimer >= frameRate)
            {
                frameTimer = 0f;
                currentFrame = (currentFrame + 1) % waveSprites.Length;
                spriteRenderer.sprite = waveSprites[currentFrame];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRigidbody = collision.GetComponent<Rigidbody2D>();
            if (enemyRigidbody != null)
            {
                Vector2 pullDirection = (towerPosition - collision.transform.position).normalized;
                enemyRigidbody.AddForce(pullDirection * pullForce, ForceMode2D.Impulse);
            }
            if (collision.TryGetComponent(out Hero hero))
            {
                hero.TakeDamage(damage);
            }
        }
    }
}