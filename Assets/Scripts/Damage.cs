using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public abstract class Damage : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    public int MaxHealth => maxHealth;

    [SerializeField] protected int health = 100;
    public int Health
    {
        get => health;
        protected set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
            Debug.Log($"[Health Update] {gameObject.name} - Health: {health}/{maxHealth}");

            if (health <= 0)
            {
                IsAlive = false;
                Die();

                if (gameObject.name == "Player")
                {
                    StartCoroutine(DelayedGameOver(2f));
                }
            }
        }
    }

    [SerializeField] private bool isAlive = true;
    public bool IsAlive
    {
        get => isAlive;
        protected set
        {
            isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log($"[Status] {gameObject.name} IsAlive set to: {value}");
        }
    }

    [SerializeField] private bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public DetectionZone DamageDetection;
    protected Animator animator;
    private bool damageDetected = false;
    public bool DamageDetected
    {
        get => damageDetected;
        protected set
        {
            damageDetected = value;
            animator.SetBool(AnimationStrings.damage_detected, value);
        }
    }

    private float lastHitTime = 0;
    private float hitCooldown = 0.1f;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        Debug.Log($"[System] {gameObject.name} Damage initialized.");
    }

    public bool LockVelocity
    {
        get => animator.GetBool(AnimationStrings.lockVelocity);
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
            Debug.Log($"[Animation] {gameObject.name} LockVelocity set to {value}");
        }
    }

    protected virtual void Update()
    {
        if (isInvincible)
        {
            timeSinceHit += Time.deltaTime;
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
                Debug.Log($"[Invincibility] {gameObject.name} invincibility ended.");
            }
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (Time.time - lastHitTime < hitCooldown)
        {
            Debug.Log($"[Hit Detection] {gameObject.name} hit ignored (cooldown). Last hit: {lastHitTime}, Current time: {Time.time}");
            return false;
        }

        lastHitTime = Time.time;
        Debug.Log($"[Hit Detection] {gameObject.name} took {damage} damage.");

        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            Debug.Log($"[Invincibility] {gameObject.name} is now invincible.");

            LockVelocity = true;
            animator.SetTrigger(AnimationStrings.hitTrigger);
            return true;
        }

        Debug.LogWarning($"[Hit Detection] {gameObject.name} ignored hit (dead/invincible).");
        return false;
    }

    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int actualHeal = Mathf.Min(MaxHealth - Health, healthRestore);
            Health += actualHeal;
            Debug.Log($"[Healing] {gameObject.name} restored {actualHeal} HP.");
            return true;
        }

        Debug.LogWarning($"[Healing] {gameObject.name} heal failed (full health or dead).");
        return false;
    }

    protected abstract void Die();

    private IEnumerator DelayedGameOver(float delay)
    {
        Debug.Log($"[Game Over] {gameObject.name} has died. Waiting for {delay} seconds...");
        yield return new WaitForSeconds(delay);
        Debug.Log($"[Game Over] Loading 'GameOverScene'...");
        SceneManager.LoadScene("GameOverScene");
    }
}
