using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 targetPosition;

    public GameObject miniFireballPrefab; // ✅ 미니 탄 프리팹

    public void Initialize(Vector3 target)
    {
        targetPosition = target;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SplitIntoMiniFireballs(); // ✅ 여기서 갈라지기 실행
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // 플레이어 맞으면 분열 없이 그냥 제거
        }
    }

    // ✅ 여기다 너가 만든 함수 붙이면 됨!
    void SplitIntoMiniFireballs()
    {
        int count = 6;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

            GameObject mini = Instantiate(miniFireballPrefab, transform.position, Quaternion.identity);
            mini.GetComponent<MiniFireball>().SetDirection(dir);
        }
    }
}
