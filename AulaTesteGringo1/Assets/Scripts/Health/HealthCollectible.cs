using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healValue;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        _collision.GetComponent<Health>().Heal(healValue);
        gameObject.SetActive(false);
    }
}
