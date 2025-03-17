using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EnemyDamage))] // Attach EnemyDamage instead of Damage

public class Scientist : MonoBehaviour
{
    public DetectionZone playerDetection;
    public float runSpeed = 12f;
    public float moveDistance = 20f;

    private Rigidbody2D rb;
    private Animator animator;
    Collider2D col;
    EnemyDamage damageable;  // Changed to EnemyDamage

    private bool playerDetected = false;
    private bool isMoving = false;
    private Vector2 startPosition;
    private bool movingRight = true;

    public bool player_detected
    {
        get { return playerDetected; }
        private set
        {
            playerDetected = value;
            animator.SetBool(AnimationStrings.player_detected, value);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        damageable = GetComponent<EnemyDamage>();  // Get the EnemyDamage component

        if (playerDetection == null)
        {
            playerDetection = GetComponentInChildren<DetectionZone>();
        }

        startPosition = transform.position;
    }

    void Update()
    {
        if (!isMoving) // Only check for player when not moving
        {
            player_detected = playerDetection.detected.Count > 0;
            if (player_detected)
            {
                FlipDirection(); // Flip before moving
                isMoving = true;
                startPosition = transform.position; // Reset position tracking
            }
        }
    }

    private void FixedUpdate()
    {
        if (!damageable.LockVelocity) // Check if scientist can move
        {
            if (isMoving)
            {
                MoveScientist();
            }
        }
    }

    private void MoveScientist()
    {
        float direction = movingRight ? -1 : 1;
        rb.linearVelocity = new Vector2(runSpeed * direction, rb.linearVelocity.y);  // Fixed typo: linearVelocity -> velocity

        // Stop after moving moveDistance
        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveDistance)
        {
            rb.linearVelocity = Vector2.zero; // Stop moving
            isMoving = false; // Wait for next detection
        }
    }

    private void FlipDirection()
    {
        movingRight = !movingRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    // OnHit method is now integrated with the Damage system to handle knockback and damage
    public void OnHit(int damage, Vector2 knockback)
    {
        // Call the Hit method of the EnemyDamage class, this will handle health and invincibility
        damageable.Hit(damage, knockback);
    }
}
