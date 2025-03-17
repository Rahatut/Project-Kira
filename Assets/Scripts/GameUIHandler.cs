using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    private Label healthLabel;
    private VisualElement healthBarMask;
    private Damage kiraDamage;
    private int previousHealth = -1; // Used to check if health changed

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        healthLabel = root.Q<Label>("healthLabel");
        if (healthLabel == null) Debug.LogError("GameUIHandler: 'healthLabel' not found in UI!");

        healthBarMask = root.Q<VisualElement>("healthBarMask");
        if (healthBarMask == null) Debug.LogError("GameUIHandler: 'HealthBarMask' not found in UI!");

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("GameUIHandler: No GameObject found with tag 'Player'!");
        }
        else
        {
            kiraDamage = player.GetComponent<Damage>();
            if (kiraDamage == null)
                Debug.LogError("GameUIHandler: 'Player' GameObject does not have a Damage component!");
        }

        if (kiraDamage != null)
        {
            Debug.Log($"GameUIHandler: Kira appeared! Initial Health: {kiraDamage.Health}/{kiraDamage.MaxHealth}");
            UpdateHealthUI();
            previousHealth = kiraDamage.Health; // Set initial health tracking
        }
    }

    private void Update()
    {
        if (kiraDamage == null) return;

        if (kiraDamage.Health != previousHealth) // ✅ Only update UI when health changes
        {
            UpdateHealthUI();
            previousHealth = kiraDamage.Health;
        }
    }

    private void UpdateHealthUI()
    {
        if (kiraDamage == null) return;
        float healthPercentage = (float)kiraDamage.Health / kiraDamage.MaxHealth * 100;
        Debug.Log($"GameUIHandler: Health percentage: {healthPercentage}%");

        if (healthLabel != null)
        {
            healthLabel.text = $"{kiraDamage.Health}/{kiraDamage.MaxHealth}";
            Debug.Log($"GameUIHandler: Health label updated to {healthLabel.text}");
        }
        if (healthBarMask != null)
        {
            healthBarMask.style.width = Length.Percent(healthPercentage);
            Debug.Log($"GameUIHandler: HealthBarMask width updated to {healthPercentage}%");
        }
    }
}
