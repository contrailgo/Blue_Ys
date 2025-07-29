using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 10;
    private int currentHP;

    public TextMeshProUGUI hpText;
    private PlayerWeapon weapon;
    private DamageEffect damageEffect;

    void Start()
    {
        currentHP = maxHP;
        UpdateHPUI();

        weapon = GetComponent<PlayerWeapon>();
        damageEffect = FindFirstObjectByType<DamageEffect>(); // ✅ 여기서 찾는다!

        if (weapon == null)
            Debug.LogError("🚨 PlayerWeapon 못 찾음!");

        if (damageEffect == null)
            Debug.LogWarning("⚠️ DamageEffect 못 찾음! BloodFlash 연결 안 됐을 수 있음.");
    }

    public void TakeDamageFromBoss(int amount)
    {
        if (weapon == null) return;

        if (weapon.IsArmed())
        {
            Debug.Log("🛡️ 무장 상태 → 보스 피해 무시");
            return;
        }

        if (weapon.IsFullyInvincible())
        {
            Debug.Log("🛡️ 전체 무적 → 보스 피해 무시");
            return;
        }

        Debug.Log("💢 보스 데미지 적용됨!");
        ApplyDamage(amount);
    }

    public void TakeDamage(int amount)
    {
        if (weapon == null) return;

        if (weapon.IsArmed())
        {
            Debug.Log("🛡️ 무장 상태 → 일반 피해 무시");
            return;
        }

        if (weapon.IsFullyInvincible())
        {
            Debug.Log("🛡️ 전체 무적 상태 → 일반 피해 무시");
            return;
        }

        ApplyDamage(amount);
    }

    public void ApplyDamage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Max(currentHP, 0);

        Debug.Log($"💥 피해: {amount} | 현재 체력: {currentHP}");

        // ✅ 체력 깎였을 때만 붉은 이펙트 실행
        if (damageEffect != null)
            damageEffect.Flash();

        UpdateHPUI();

        if (currentHP <= 0)
            Die();
    }

    void UpdateHPUI()
    {
        if (hpText != null)
            hpText.text = $"HP {currentHP}";
    }

    void Die()
    {
        Debug.Log("💀 플레이어 사망!");
        gameObject.SetActive(false);
    }
}




