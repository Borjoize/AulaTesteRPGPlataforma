using UnityEngine;

public class SidewaysTrap : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField]private float damage;
    private bool movingLeft;
    private float leftLimit;
    private float rightLimit;

    private void Awake()
    {
        leftLimit = transform.position.x - movementDistance;
        rightLimit = transform.position.x + movementDistance;
    }

    private void Update()
    {
        if(movingLeft)
        {
            if(transform.position.x > leftLimit)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if(transform.position.x < rightLimit)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D _collision)
    {
        _collision.GetComponent<Health>().TakeDamage(damage);
    }
}
