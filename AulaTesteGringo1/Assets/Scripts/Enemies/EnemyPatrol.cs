using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("PatrolPoints")]
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

     [Header("Parametros Movimento")]
    [SerializeField] private float speed;
    //Decidir qual lado ele tá virado quando nasce
    private Vector3 initialScale;
    private bool movingLeft;

    [Header("Parametros Idle")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anima;

    private void Awake()
    {
        initialScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anima.SetBool("Moving", false);                
    }

    private void Update()
    {
        if(movingLeft)
        {
            if(enemy.position.x >= leftLimit.position.x)
            {
            MoveTo(-1);
            Debug.Log($"Mexi pra esquerda. Eu estou em {enemy.position.x} e o limite está em {leftLimit.position.x}");
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if(enemy.position.x <= rightLimit.position.x)
            {
                MoveTo(1);
                Debug.Log($"Mexi pra direita. Eu estou em {enemy.position.x} e o limite está em {rightLimit.position.x}");
            }
            else
            {
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        idleTimer += Time.deltaTime;
        anima.SetBool("Moving", false);

        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }

    }


    private void MoveTo(int _direction)
    {
        idleTimer = 0;
        anima.SetBool("Moving", true);
        //Olhar para a direção certa
        //A função matemática Abs volta o valor absoluto de initialScale.x
        enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * -_direction, initialScale.y, initialScale.z);

        //Se mexer nela
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
        enemy.position.y, enemy.position.z);
    }
}
