using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if(_collision.tag == "Player")
        {
            _collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
