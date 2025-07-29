using UnityEngine;

public class SwordSpawner : MonoBehaviour
{
    public GameObject swordPrefab;
    public float respawnDelay = 5f;

    private bool isWaiting = false;

    void Start()
    {
        SpawnSword();
    }

    public void SpawnSword()
    {
        GameObject[] groundList = GameObject.FindGameObjectsWithTag("Ground");
        if (groundList.Length == 0)
        {
            Debug.LogWarning("❗ Ground 태그 오브젝트가 없음. 검 소환 실패");
            return;
        }

        // 랜덤 Ground 하나 선택
        GameObject selectedGround = groundList[Random.Range(0, groundList.Length)];

        // 그 위치 위에 소환 (y축 살짝 띄움)
        Vector3 spawnPos = selectedGround.transform.position + Vector3.up * 0.5f;

        Instantiate(swordPrefab, spawnPos, Quaternion.identity);
        isWaiting = false;
    }

    public void RequestRespawn()
    {
        if (!isWaiting)
        {
            isWaiting = true;
            StartCoroutine(RespawnAfterDelay());
        }
    }

    private System.Collections.IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnSword();
    }
}

