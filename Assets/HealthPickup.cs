using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 50;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    AudioSource pickupSource;

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damageable = collision.GetComponent<Damage>();

        if (damageable && damageable.Health < damageable.MaxHealth)
        {
            bool wasHealed = damageable.Heal(healthRestore);

            if (wasHealed)
            {
               Destroy(gameObject);
            }

        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
