using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Throw")]
    public GameObject snowballPrefab;
    public float throwSpeed = 12f;
    public float spawnOffset = 0.4f;
    public float snowballLifeTime = 2.5f;

    Rigidbody2D rb;
    Camera cam;

    Vector2 mouseWorld;
    Vector2 prevMouseWorld;
    Vector2 lastMoveDir = Vector2.right; // 마지막으로 유효했던 커서 이동 방향

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        if (Mouse.current == null || cam == null) return;

        // 1) 마우스 스크린 좌표 -> 월드 좌표 :contentReference[oaicite:2]{index=2}
        Vector2 mouseScreen = Mouse.current.position.ReadValue();

        // 2D(대개 z=0 평면) 기준: 카메라와의 거리(z)가 필요 :contentReference[oaicite:3]{index=3}
        float zDistFromCamera = -cam.transform.position.z;
        mouseWorld = cam.ScreenToWorldPoint(new Vector3(mouseScreen.x, mouseScreen.y, zDistFromCamera));

        // 2) 커서 이동 방향(현재-이전)으로 던질 방향 갱신 :contentReference[oaicite:4]{index=4}
        Vector2 delta = mouseWorld - prevMouseWorld;
        if (delta.sqrMagnitude > 0.000001f)
            lastMoveDir = delta.normalized;

        prevMouseWorld = mouseWorld;

        // 3) 좌클릭하면 눈 생성/발사
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 spawnPos = rb.position + lastMoveDir * spawnOffset; // rb.position은 2D 좌표 :contentReference[oaicite:5]{index=5}
            GameObject snow = Instantiate(snowballPrefab, spawnPos, Quaternion.identity);

            var snowRb = snow.GetComponent<Rigidbody2D>();
            if (snowRb != null)
                snowRb.linearVelocity = lastMoveDir * throwSpeed; // Unity 6: linearVelocity :contentReference[oaicite:6]{index=6}

            Destroy(snow, snowballLifeTime); // :contentReference[oaicite:7]{index=7}
        }
    }

    void FixedUpdate()
    {
        // 플레이어 위치를 커서 위치로 “일치”시키기(물리 방식) :contentReference[oaicite:8]{index=8}
        rb.MovePosition(mouseWorld);
    }
}
