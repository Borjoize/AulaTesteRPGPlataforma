using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] public float _attackCD;

    //Esse aqui serve pra definir a posição de onde vai sair o tiro
    [SerializeField] private Transform _firePoint;

    //Esse aqui serve pra gente depositar os tiros
    //Ele é um SerializeFiel pra gente n precsar ficar declarando cada tiro separadamente
    //O "[]" depois do GameObject serve podermos colocar diversas coisas dentro dele (array)
    [SerializeField] private GameObject[] _projeteis;
    //Pra facilitar colocar vários objetos dentro desse array, você seleciona o objeto onde ele está
    //Daí vc dá lock nele (cadeado no topo) e aí vc pode arrastar eles no array
    //Sem o lock ele troca de objeto na hora q vc seleciona o outro

    //Tem esse Matf.Infinity pra já poder nascer atacando
    private float _cDTimer = Mathf.Infinity;
    private Animator _anima;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _anima = GetComponent<Animator>();

        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && _cDTimer > _attackCD && _playerMovement.canAttack())
        {
            Attack();
        }

        _cDTimer += Time.deltaTime;
    }

    private void Attack()
    {
        _anima.SetTrigger("Attack");
        _cDTimer = 0;

        //Agente vai "pool" os projéteis, que é um dos métodos pra se criar projéteis
        //O outro mais famoso é Instantiate e Destroy, que é mais fácil de implementar, mas pesa muito no processamento
        //A grande diferença é que o segundo método toda hora "cria" um projétil novo
        //Enquanto o outro faz uma "pool" de projéteis prontos que ele só ativa/desativa conforme a necessidade
        //É um pouquinho mais complicado, mas vale a pena
        //Deus no comando, Jesus no bb conforto

        //Isso aqui bota os projéteis na posição de tiro
        _projeteis[FindProjectile()].transform.position = _firePoint.position;

        //Isso aqui pega o projétil e ativa ele na direção certa
        _projeteis[FindProjectile()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }

    private int FindProjectile()
    {
        for (int i = 0; i < _projeteis.Length; i++)
        {
            //Isso aqui verifica se o projétil está ou não ativo, que é bom pra não usarmos ele
            //Se ele estiver ativo ele procura na próxima posição do array
            if(!_projeteis[i].activeInHierarchy)
            {
                return i;
            }
        }
        
        return 0;
    }

}
