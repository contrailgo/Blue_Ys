using UnityEngine;

public class BossFire : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float shootInterval = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            timer = 0f;
            ShootAtPlayer();
        }
    }

    void ShootAtPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        // 파이어볼 생성
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

        // 목표 위치만 넘겨줌
        Fireball fb = fireball.GetComponent<Fireball>();
        fb.Initialize(player.transform.position); // ✅ 속도는 파이어볼이 자체 설정 사용
    }
}
