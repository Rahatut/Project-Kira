using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EnemyDamage))] 
public class Scientist2 : MonoBehaviour
{
    public DetectionZone playerDetection;
    public float runSpeed = 12f;
    public float moveDistance = 20f;

    private Rigidbody2D rb;
    private Animator animator;
    private EnemyDamage enemyDamage;
    private Vector2 startPosition;
    private bool isMoving = false;
    private bool movingRight = true;

    public bool player_detected
    {
        get { return playerDetection.detected.Count > 0; }
        private set
        {
            animator.SetBool(AnimationStrings.player_detected, value);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyDamage = GetComponent<EnemyDamage>(); 

        if (playerDetection == null)
        {
            playerDetection = GetComponentInChildren<DetectionZone>();
        }

        startPosition = transform.position;
    }

    void Update()
    {
        if (!isMoving)
        {
            player_detected = playerDetection.detected.Count > 0;
            if (player_detected)
            {
                isMoving = true;
                startPosition = transform.position; 
            }
        }
    }

    private void FixedUpdate()
    {
        if (!enemyDamage.LockVelocity)
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
        rb.linearVelocity = new Vector2(runSpeed * direction, rb.linearVelocity.y);

       
        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveDistance)
        {
            rb.linearVelocity = Vector2.zero; 
            isMoving = false; 
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        enemyDamage.Hit(damage, knockback); 
    }
}
