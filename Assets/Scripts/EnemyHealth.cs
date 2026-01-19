using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 3;
    int hp;

    void Awake()
    {
        hp = maxHP;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("PlayerSnowball")) return; // :contentReference[oaicite:2]{index=2}
        hp -= 1;
        Destroy(other.gameObject);
        if (hp <= 0) Destroy(gameObject);
    }

}
