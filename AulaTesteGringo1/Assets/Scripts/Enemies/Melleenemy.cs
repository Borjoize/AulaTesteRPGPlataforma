using UnityEngine;

public class Melleenemy : MonoBehaviour
{
    [Header("Parametros Atk")]
    [SerializeField] private float attackCD;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Parametros Collider")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    private float cDTimer = Mathf.Infinity;

//References
    private Animator anima;
    private Health playerHealth;

    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anima = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cDTimer += Time.deltaTime;

        if(PlayerInSight())
        {
            //Pra atacar só quando o cooldown passou
            if(cDTimer >= attackCD)
            {
                cDTimer = 0;
                anima.SetTrigger("MeleeAttack");

            }
        }

        //Isso é pra garantir q ele bate enquanto anda
        //Assim, n exatamente, isso garante que quando tem um inimigo ele para de andar pra bater
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        //Isso é pra detectar se o player está na frente do inimigo
        //Vamos lançar um quadrado na frente dele com BoxCast
        //BoxCast tem 6 parâmetros (origem, tamanho, angulo, direção, distância e uma layerMask)
        //Se o player entrar nesse quadrado, quer dizer que já dá pra bater nele
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0, Vector2.left, 0, playerLayer);
        //Mano esse bando de coisa dentro do primeiro parâmetro e do segunto vai ser difícil de explicar ahuiahaiuhaiu
        //No angulo colocamos 0 porque é um jogo 2D e não queremos que fique torto XD

        if(hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    //Isso aqui é pra conseguirmos enxergar o nosso BoxCast no Editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        //Se ele tá dentro do BoxCast (ou "InSight) ele toma dano
        if(PlayerInSight())
        {
            if(PlayerInSight())
            {
                playerHealth.TakeDamage(damage);
            }

        }
    }
}
