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

        if (TryGetComponent<Status>(out Status status))
            {
                status.OnAttackUp += (Status status) => {playerDamage++;};
                Debug.Log("Supostamente eu funcionei");
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
            Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        _hit = true;

        //_collision.enabled = false;

        _anima.SetTrigger("Explode");

        if (_collision.tag == "Enemy")
        {
            _collision.GetComponent<Health>().TakeDamage(playerDamage);
            Debug.Log("Eu sou o inimigo e tomei dano");
        }
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
