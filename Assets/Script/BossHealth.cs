using UnityEngine;
using System.Collections.Generic;

public class BossHealth : MonoBehaviour
{
    [Header("보스 체력 설정")]
    public int maxHP = 100;
    private int currentHP;
    private bool isDead = false;

    [Header("헤일로 체력 UI")]
    public GameObject haloUnitPrefab;           // HaloIcon 프리팹
    public Transform haloUIContainer;           // BossHaloContainer (UI 위치)

    private List<GameObject> haloUnits = new List<GameObject>();

    public SwordSpawner swordSpawner;

    void Start()
    {
        currentHP = maxHP;
        CreateHaloUI(); // ✅ Halo UI 생성
    }

    public void TakeDamage(int amount, GameObject attacker)
    {
        Debug.Log("💥 보스가 데미지를 입었다!");

        if (isDead) return;

        PlayerWeapon weapon = attacker.GetComponent<PlayerWeapon>();
        if (weapon == null || !weapon.IsArmed()) return;

        currentHP -= amount;
        currentHP = Mathf.Max(currentHP, 0);

        Debug.Log($"🗡️ 보스 피해: {amount} → 남은 HP: {currentHP}");

        weapon.OnAttackSuccess(); // 무장 해제 + 전체 무적 시작

        UpdateHaloUI(); // ✅ Halo UI 업데이트

        if (swordSpawner != null)
            swordSpawner.RequestRespawn();

        if (currentHP <= 0)
            Die();
    }

    void UpdateHaloUI()
    {
        // 뒤에서부터 Halo 제거
        for (int i = maxHP - 1; i >= currentHP; i--)
        {
            if (i < haloUnits.Count && haloUnits[i] != null)
            {
                Destroy(haloUnits[i]);
                haloUnits[i] = null;
            }
        }
    }

    void CreateHaloUI()
    {
        // 기존 Halo 제거
        foreach (Transform child in haloUIContainer)
            Destroy(child.gameObject);

        haloUnits.Clear();

        // Halo 생성
        for (int i = 0; i < maxHP; i++)
        {
            GameObject unit = Instantiate(haloUnitPrefab, haloUIContainer);
            haloUnits.Add(unit);
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("💀 보스 사망!");
        gameObject.SetActive(false);
    }

    // ✅ 플레이어와 충돌 시 데미지 시도
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamageFromBoss(1); // 무조건 시도
            }
        }
    }
}









