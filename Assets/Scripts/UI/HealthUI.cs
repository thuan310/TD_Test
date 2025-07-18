using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private Stats stats;

    private void Awake()
    {
        stats = GetComponentInParent<Stats>();
    }

    private void OnEnable()
    {
        if (stats != null) stats.OnHealthChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        if (stats != null) stats.OnHealthChanged -= UpdateHealth;
    }

    void UpdateHealth(int current, int max)
    {
        fillImage.fillAmount = (float)current / max;
    }
}
