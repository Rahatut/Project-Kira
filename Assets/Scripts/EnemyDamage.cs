using UnityEngine;

public class EnemyDamage : Damage
{

    public int AttackDamage = 10;
    protected override void Die()
    {
        Debug.Log($"[Death] {gameObject.name} has died.");

        animator.SetBool(AnimationStrings.isAlive, false);

       

        // Disable enemy components to prevent further interactions
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;

        // Optionally, destroy the enemy after a delay
        //Destroy(gameObject, 2f);
    }
}
