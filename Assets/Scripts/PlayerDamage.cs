using UnityEngine;

public class PlayerDamage : Damage
{
    protected override void Die()
    {
        Debug.Log($"[Death] {gameObject.name} has died.");
        animator.SetBool(AnimationStrings.isAlive, false);

        // Disable player controls (if applicable)
        GetComponent<PlayerController>().enabled = false;

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider object has the 'EnemyDamage' component
        EnemyDamage enemy = collision.GetComponent<EnemyDamage>();
        if (enemy != null)
        {
            // If enemy is found, apply damage and knockback
            Vector2 knockback = (transform.position - collision.transform.position).normalized * 5f;
            Hit(enemy.AttackDamage, knockback);
        }
    }
}
