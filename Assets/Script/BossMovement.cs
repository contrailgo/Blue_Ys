using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Vector2 moveDirection;
    private Rigidbody2D rb;

    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float halfWidth;
    private float halfHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 🎯 대각선 방향만 선택!
        Vector2[] directions = new Vector2[]
        {
            new Vector2(1, 1),
            new Vector2(-1, 1),
            new Vector2(-1, -1),
            new Vector2(1, -1)
        };
        moveDirection = directions[Random.Range(0, directions.Length)].normalized;

        Camera cam = Camera.main;
        minBounds = cam.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = cam.ViewportToWorldPoint(new Vector2(1, 1));

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            halfWidth = sr.bounds.extents.x;
            halfHeight = sr.bounds.extents.y;
        }
    }

    void FixedUpdate()
    {
        Vector2 newPos = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;

        // X축 반사
        if (newPos.x - halfWidth <= minBounds.x)
        {
            moveDirection = Vector2.Reflect(moveDirection, Vector2.right);
            newPos.x = minBounds.x + halfWidth;
        }
        else if (newPos.x + halfWidth >= maxBounds.x)
        {
            moveDirection = Vector2.Reflect(moveDirection, Vector2.right);
            newPos.x = maxBounds.x - halfWidth;
        }

        // Y축 반사
        if (newPos.y - halfHeight <= minBounds.y)
        {
            moveDirection = Vector2.Reflect(moveDirection, Vector2.up);
            newPos.y = minBounds.y + halfHeight;
        }
        else if (newPos.y + halfHeight >= maxBounds.y)
        {
            moveDirection = Vector2.Reflect(moveDirection, Vector2.up);
            newPos.y = maxBounds.y - halfHeight;
        }

        rb.MovePosition(newPos);
    }
}

