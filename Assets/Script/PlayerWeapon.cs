using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public bool isArmed = false;
    public float maxArmedDuration = 2f;             // 인스펙터에서 조절 가능: 무장 지속 시간
    public int selfDamageOnAutoDisarm = 1;          // 무장 실패 시 자해 데미지 (인스펙터 조절)

    private float armedTimer = 0f;
    private bool isFullyInvincible = false;

    private SwordSpawner swordSpawner;
    private PlayerHealth playerHealth;

    void Start()
    {
        swordSpawner = FindFirstObjectByType<SwordSpawner>();   // 최신 버전 대응
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (isArmed)
        {
            armedTimer += Time.deltaTime;

            if (armedTimer >= maxArmedDuration)
            {
                SetArmed(false);
                Debug.Log("⏱ 공격 없이 시간 초과 → 무장 자동 해제");

                // ✅ 검 재소환
                if (swordSpawner != null)
                {
                    swordSpawner.RequestRespawn();
                    Debug.Log("🗡️ 검 재소환 요청");
                }

                // ✅ 자해 적용
                if (playerHealth != null)
                {
                    Debug.Log($"💢 무장 실패로 자해 {selfDamageOnAutoDisarm} 데미지");
                    playerHealth.ApplyDamage(selfDamageOnAutoDisarm);
                }
                else
                {
                    Debug.LogWarning("⚠️ PlayerHealth 못 찾음 → 자해 실패");
                }
            }
        }
    }

    public void SetArmed(bool state)
    {
        isArmed = state;

        if (state)
        {
            armedTimer = 0f;
            Debug.Log("🗡️ 무장 상태 ON");
        }
        else
        {
            Debug.Log("❌ 무장 해제");
        }
    }

    public bool IsArmed() => isArmed;
    public bool IsFullyInvincible() => isFullyInvincible;

    public void SetFullInvincible(float duration)
    {
        StartCoroutine(InvincibilityRoutine(duration));
    }

    private System.Collections.IEnumerator InvincibilityRoutine(float duration)
    {
        isFullyInvincible = true;
        Debug.Log("🛡️ 전체 무적 ON");
        yield return new WaitForSeconds(duration);
        isFullyInvincible = false;
        Debug.Log("🛡️ 전체 무적 OFF");
    }

    public void OnAttackSuccess()
    {
        if (isArmed)
        {
            SetArmed(false);
            Debug.Log("💥 공격 성공 → 무장 해제");

            if (swordSpawner != null)
            {
                swordSpawner.RequestRespawn();
                Debug.Log("🗡️ 공격 성공으로 검 재소환");
            }
        }

        SetFullInvincible(1.0f);  // 공격 성공 시 1초 무적
    }
}





