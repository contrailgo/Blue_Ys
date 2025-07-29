using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerWeapon weapon = other.GetComponent<PlayerWeapon>();
            if (weapon != null)
            {
                weapon.SetArmed(true);
                Debug.Log("⚔️ 검 획득!");

                // 검 오브젝트 제거 or 비활성화
                gameObject.SetActive(false); // or Destroy(gameObject);
            }
        }
    }
}

