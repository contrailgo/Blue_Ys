using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerHealth health;
    private PlayerWeapon weapon;

    void Start()
    {
        health = GetComponent<PlayerHealth>();
        weapon = GetComponent<PlayerWeapon>();

        if (health == null)
            Debug.LogError("🚨 PlayerHealth 컴포넌트를 찾을 수 없습니다!");

        if (weapon == null)
            Debug.LogError("🚨 PlayerWeapon 컴포넌트를 찾을 수 없습니다!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        int damage = 0;

        // ✅ 무장 상태일 때 보스에게 공격 가능
        if (weapon != null && weapon.IsArmed())
        {
            if (other.CompareTag("Boss"))
            {
                Debug.Log("⚔️ 무장 상태 → 보스 공격 시도");

                BossHealth boss = other.GetComponent<BossHealth>();
                if (boss != null)
                {
                    boss.TakeDamage(1, gameObject);
                    Debug.Log("✅ 보스에게 데미지 1 적용됨");
                }
                else
                {
                    Debug.LogWarning("❗ 보스에 BossHealth 컴포넌트 없음");
                }

                // 공격 성공 처리 (무장 해제 + 무적 + 검 리스폰)
                weapon.OnAttackSuccess();
                return;
            }

            // 보스 아니면 무장 상태로 피해 무시
            Debug.Log("🛡️ 무장 상태 → 보스 아님 → 피해 무시");
            return;
        }

        // ✅ 비무장 상태에서 피격 처리
        if (other.CompareTag("Boss"))
            damage = 3;
        else if (other.CompareTag("Fireball"))
            damage = 2;
        else if (other.CompareTag("MiniFireball"))
            damage = 1;

        if (damage > 0 && health != null)
        {
            health.TakeDamage(damage);

            if (other.CompareTag("Fireball") || other.CompareTag("MiniFireball"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}








