using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _body;

    //O SerializeField deixa a gente ver e editar os valores no editor
    //Importante para coisas que vão precisar de "balanço"
    [SerializeField] private float _speed;

    [SerializeField] private float _jumpPower;

    //Esse aqui aponta para uma layer "groundLayer" que você tem que criar no Unity Editor
    [SerializeField] private LayerMask groundLayer;
    //Como é serializado, você vai escolher a Layer nesse componente (MovementScript) no Unity Editor

    [SerializeField] private LayerMask wallLayer;

    private Animator _anima;

    private BoxCollider2D _boxCollider;

    private float _wallJumpCooldown;

    private float _horizontalInput;

    [SerializeField] private float _wallJumpCooldownLimit;

    private void Awake()
    {
        //Pega a referência do Rigidbody2D e do Animator do objeto que você colocou no Unity Editor
        _body = GetComponent<Rigidbody2D>();

        _anima = GetComponent<Animator>();

        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");

        //Movimento linear do boneco
        _body.linearVelocity = new Vector2(_horizontalInput * _speed, _body.linearVelocity.y);

        //Flipar o boneco de acordo com a direção que ele está indo
        if(_horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if(_horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        //Setar os controles de animação
        //Entre aspas vc coloca o mesmo nome que declarou no Animator no Unity Editor
        _anima.SetBool("Running", _horizontalInput != 0);
        //!= verifica se as duas variáveis que você declarou antes (nesse caso "Running" e "_horizontalInput") são diferentes
        //Se eu usasse ==, ele verificaria se são iguais, como nós queremos qualquer valor diferente de 0, != é melhor
        //Se não forem iguais o valor horizontal é 0, portanto "Running = False"
        //Basicamente pergunta é "Is X ("Running") different from Y (_horizontalInput)?"
        
        //Aqui é pra o Animator saber quando ele está "grounded"
        //Como a definição de "true" e "false" está lá no Jump e no OnCollisionEnter2D, eu n preciso declarar aqui
        _anima.SetBool("Grounded", isGrounded());

        //Se ele estiver no cooldown ele n vai poder pular dnv
        if(_wallJumpCooldown > _wallJumpCooldownLimit)
        {
            //Isso aqui permite que ele "cole" na parede
            //Pra isso ele precisa estar onWall() (ou seja, encostado numa parede) e não estar isGrounded()
            //Esse && verifica, adicionalmente se o player NÃO está "grounded"
            //O "NÃO" é por causa da eclamação "!" antes do isGrounded()
            if(onWall() && !isGrounded())
            {
                //Se as condições if estiverem ok, a gravidade vai pra 0, ou seja ele "flutua" coladinho na parede
                _body.gravityScale = 0;
                //Se ele estiver colado a velocidade ele é 0
                _body.linearVelocity = Vector2.zero;
            }
            //O problema do "if" acima é que ele anula a gravidade "pra sempre"
            //Então esse else é pra quando ele descolar da parede a gravidade voltar ao normal
            //Nesse caso o "normal" é 3 (pq foi o que eu defini no rigidbody2D do player no Unity Editor)
            else
            {
                _body.gravityScale = 3;
            }

            //Isso aqui é pra pular. Se espaço está apertado, todas as funções de Jump() são chamadas
            if(Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
            
        }
        //Esse aqui eu n sei, mas parece importante XD
        else
        {
            _wallJumpCooldown += Time.deltaTime;
        }

    }

    private void Jump()
    {
        if(isGrounded())
        {
        //Salto do boneco
        _body.linearVelocity = new Vector2(_body.linearVelocity.x, _jumpPower);
        _anima.SetTrigger("Jump");
        }
        else if(onWall() && !isGrounded())
        {
            float _jumpDirection = -Mathf.Sign(transform.localScale.x);

            if(_horizontalInput == 0)
            {
            //Esse aqui agora é pra ele pular numa direção contra a parede e pra cima
            //O Mathf.Sign serve pra transformar qq numero dentro dos parenteses em 1 ou -1
            //Nesse caso, seria 1 pra ele virado pra direita e -1 pro contrário
            //MAS ele tem um - antes, porque queremos inverter a direção da força que ele vai pular
            //Afinal, é pra ele pular em uma direção oposta à parede
            _body.linearVelocity = new Vector2(_jumpDirection * _speed * 20, _jumpPower);
            //O * 3 é a fora que ele vai pular contra a parede, afinal só 1 ou -1 n ia fazer quase anda
            //Na segunda várável eu coloquei o _jumpPower sendi dividido por 2 porque quero que ele pule menos do muro
            Debug.Log($"Wall Jump: Direction = {_jumpDirection}, Speed = {_speed}, JumpPower = {_jumpPower}");

            //Mas por algum motivo não funciona direito, não consigo fazer ele se afastar da parede hauihauhaiuhiua
            //Tentei trocar o linearVelocity por ApplyForce mas deu no mesmo

            transform.localScale = new Vector3(_jumpDirection, transform.localScale.y, transform.localScale.z);
            }
            else
            //Reseta o cooldown do wallJump
            _wallJumpCooldown = 0;


        }
    }

    private bool isGrounded()
    {
        //Essa função verifica se o player está tocando no chão (colision)
        //Só dar um hover no BoxCast pra ver e entender cada uma das atribuições já que né, tem 6 dessa porra
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0,
        Vector2.down, 0.1f, groundLayer);
        //O RayCast funciona jogando um "raio" (vetor) pra uma direção pra verificar se bateu em algo ou não
        //Nesse caso nosso raio só tem 0.1f de distância e vai pra baixo (Vector2.down)

        //Esse aqui é só pra saber se o Raycast atingiu algo na no chão (groundLayer)
        //com uma distância menor que 0.1f (que a gente declarou ali em cima)
        return raycastHit.collider != null;
        //Se o raycastHit atingiu, ele retorna True (porque o player vai estar a 0.1f do chão)
        //Se não, False porque o raio (que mede só 0.1f) não atingiu nada 
    }

        private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0,
        new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        //Basicamente eu troco o Vector2.down por um que pode detectar coisas na direita e na esquerda
        //O Vector2(transform.LocalScale.x, 0) vai soltar o raio (RayCast) pra direita ou esquerda
        //Que depende de x ser 1 ou -1
        
        return raycastHit.collider != null;
    }

    //Aqui vamos só criar uma condição que permite que o player ataque
    public bool canAttack()
    {
        //Se ele não estiver andando (_horizontalInput == 0), estiver no chçao (!isGrounded())
        //e não estiver no muro (!inWall()), tá ok atacar
        return _horizontalInput == 0 && isGrounded() && !onWall();
    }
}
