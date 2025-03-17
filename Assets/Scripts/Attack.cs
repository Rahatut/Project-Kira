using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;
    private Collider2D attackCollider;
    public DetectionZone attackDetection;
    private Animator animator;

    public bool attackDetected = false;
    public bool attack_detected
    {
        get { return attackDetected; }
        private set
        {
            attackDetected = value;
            if (animator != null)
            {
                animator.SetBool(AnimationStrings.attack_detected, value);
            }
            else
            {
                Debug.LogError("Animator is null when trying to set attack_detected.");
            }
        }
    }

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        if (attackDetection == null)
        {
            attackDetection = GetComponentInChildren<DetectionZone>();
        }
    }

    void Update()
    {
        // attack_detected = attackDetection.detected.Count > 0;

        if (attack_detected)
        {
            HandleAttackReaction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object has a Damage component
        Damage damage = collision.GetComponent<Damage>();
        if (damage != null)
        {
            // If parent is facing the left by localscale, our knockback x flips its value to face the left as well
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            // Hit the target (pass damage and knockback to Damage class)
            bool gotHit = damage.Hit(attackDamage, deliveredKnockback);
            if (gotHit)
                Debug.Log(collision.name + " hit for " + attackDamage);
        }
    }

    private void HandleAttackReaction()
    {
        // Trigger the attack animation
        animator.SetTrigger(AnimationStrings.attackTrigger);

        // Add logic for damage, knockback, or any reaction here (e.g., visual effects, sound effects)
        // This part can be extended to handle additional behavior like particle effects, etc.
    }
}
