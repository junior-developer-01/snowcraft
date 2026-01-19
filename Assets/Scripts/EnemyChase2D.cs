using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase2D : MonoBehaviour
{
    public Transform target;      // 플레이어 Transform 연결
    public float moveSpeed = 2.5f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 current = rb.position;
        Vector2 dir = ((Vector2)target.position - current);

        if (dir.sqrMagnitude < 0.0001f) return;

        Vector2 next = current + dir.normalized * moveSpeed * Time.fixedDeltaTime;

        // 물리 이동은 FixedUpdate + MovePosition 권장 :contentReference[oaicite:6]{index=6}
        rb.MovePosition(next);
    }
}
