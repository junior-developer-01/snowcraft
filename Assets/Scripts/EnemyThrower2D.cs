using UnityEngine;

public class EnemyThrower2D : MonoBehaviour
{
    [Header("Target")]
    public Transform target;               // 인스펙터에 Player Transform 연결(권장)
    public float range = 6f;

    [Header("Throw")]
    public GameObject enemySnowballPrefab; // Project의 프리팹을 넣어야 함
    public float fireInterval = 1.0f;
    public float throwSpeed = 8f;
    public float spawnOffset = 0.5f;
    public float snowballLifeTime = 4f;

    float cooldown;

    void Awake()
    {
        cooldown = 0f;
    }

    void Update()
    {
        // 안전장치: 프리팹 참조가 비었거나(또는 파괴된 참조)면 중단
        if (!enemySnowballPrefab) return;

        if (target == null)
        {
            // (선택) 자동으로 Player 태그 찾기
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) target = p.transform;
            else return;
        }

        // 쿨다운 업데이트 :contentReference[oaicite:7]{index=7}
        cooldown -= Time.deltaTime;
        if (cooldown > 0f) return;

        Vector2 from = transform.position;
        Vector2 to = target.position;
        Vector2 diff = to - from;

        // 사거리 밖이면 발사 안 함
        if (diff.sqrMagnitude > range * range) return;

        Vector2 dir = diff.normalized;
        Throw(dir);

        cooldown = fireInterval;
    }

    void Throw(Vector2 dir)
    {
        Vector2 spawnPos = (Vector2)transform.position + dir * spawnOffset;

        // 투사체 생성 :contentReference[oaicite:8]{index=8}
        GameObject snow = Instantiate(enemySnowballPrefab, spawnPos, Quaternion.identity);

        // 속도 부여(유니티6: linearVelocity) :contentReference[oaicite:9]{index=9}
        var rb = snow.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = dir * throwSpeed;

        // 일정 시간 후 제거 :contentReference[oaicite:10]{index=10}
        Destroy(snow, snowballLifeTime);
    }
}
