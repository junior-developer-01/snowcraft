using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 5;
    int hp;

    void Awake() => hp = maxHP;

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Destroy(gameObject); // 필요하면 여기서 게임오버 처리 :contentReference[oaicite:12]{index=12}
            GameEndManager.Instance?.Lose();
        }
    }
}
