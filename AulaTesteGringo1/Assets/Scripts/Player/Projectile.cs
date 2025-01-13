using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private float playerDamage =1;

    private float direction;

    private bool _hit;

    private BoxCollider2D _boxCollider2D;

    private Animator _anima;

    private float _lifetime;

    [SerializeField] private float _lifetimeLimit;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();

        _anima = GetComponent<Animator>();

        if (_boxCollider2D.tag == "Enemy")
        {
            _boxCollider2D.GetComponent<Health>().TakeDamage(playerDamage);
        }
    }

    private void Update()
    {
        if(_hit)
        {
            return;
        }

        float _movementSpeed = _speed * Time.deltaTime * direction;

        transform.Translate(_movementSpeed, 0 ,0);

        _lifetime += Time.deltaTime;

        if (_lifetime > _lifetimeLimit)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        _hit = true;

        _boxCollider2D.enabled = false;

        _anima.SetTrigger("Explode");
    }

    public void SetDirection(float _direction)
    {
        _lifetime = 0;
        
        direction = _direction;

        gameObject.SetActive(true);

        _hit = false;

        _boxCollider2D.enabled = true;

        float localScaleX = transform.localScale.x;

        if(Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    
}
