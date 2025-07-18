using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private HeroSkill heroSkill;
    [SerializeField] private Image cooldownFill;

    private float maxCooldown;

    private void Start()
    {
        if (heroSkill != null)
            maxCooldown = heroSkill.cooldown;
    }

    private void Update()
    {
        cooldownFill.fillAmount = 1 - heroSkill.CooldownRemaining / maxCooldown;
    }
}
