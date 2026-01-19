using UnityEngine;

public class EnemySnowballHit2D : MonoBehaviour
{
    public int damage = 1;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag("Player")) return;

        // col.collider.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        Destroy(gameObject);
    }
}





