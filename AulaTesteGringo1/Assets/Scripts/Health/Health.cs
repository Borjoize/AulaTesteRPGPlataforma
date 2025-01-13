using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header ("Health")] 
    [SerializeField] private float _startingHealth;
    public float _currentHealth { get; private set; }
    public float _maxHealth;
    private Animator anima;
    private bool dead;

    [Header ("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;

    [Header ("Components")]
    [SerializeField] private Behaviour[] components;

    //public Action OnDeath;
    UnityEvent OnDeath;


    private void Awake()
    {
        _maxHealth = _startingHealth;
        _currentHealth = _startingHealth;
        anima = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Ele tenta puxar o Status e se conseguir joga no status
        if (TryGetComponent<Status>(out Status status))
        {
            //status.OnVitalityUp += ()=>{_startingHealth++;};
            status.OnVitalityUp += (Status status)=>
            {_maxHealth = _startingHealth + status.Vitality;
            _currentHealth = _maxHealth;
            };
        }
    }

    /*public void TakeDamage(float _damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - _damage, 0, _startingHealth);
        
        if(_currentHealth == _maxHealth)
        {
            anima.SetTrigger("Hurt");
            //Healthy();
        }
        else if(_currentHealth > 0 && _currentHealth < _maxHealth)
        {
            anima.SetTrigger("Hurt");
        }
        else
        {
            if(!dead)
            {
                anima.SetTrigger("Die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
            
        }
    }*/

    public void TakeDamage(float _damage)
    {
        Debug.Log($"Minha vida atual: {_currentHealth}");
        _currentHealth = Mathf.Clamp(_currentHealth - _damage, 0, _maxHealth);

        if(_currentHealth > 0)
        {
            anima.SetTrigger("Hurt");
            StartCoroutine(Invulnerability());
                    Debug.Log($"Agora eu tenho {_currentHealth} de vida");
        }
        else
        {
            if(!dead)
            {
                anima.SetTrigger("Die");

                //foreach vai checar cada obj que a gente colocou no Behavior lá em cima
                //e vai executar a função entre os colchetes, no caso, desativar
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }

                dead = true;
                OnDeath?.Invoke();
            }
        }
    }

    //Usei pra testar o dano e tal
    /*private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }*/

    public void Heal(float _healValue)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + _healValue, 0, _maxHealth);
    }

    //A gente tá usando um IEnumerator pra facilitar as piscadas dos iFrames
    //Com ele a gente pode usar um wield WaitForSeconds pra controlar o tempo
    private IEnumerator Invulnerability()
    {
        //Aqui a gente vai deixar ele invulnerável deixando ele sem colisão com a Layer de Inimigos
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            //Dentro de Color a ordem é RGBA
            //Mas em vez de ser de 0 à 255 é de 0 a 1, ou seja, nós escolhemos em fração a porcentagem da cor
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            //Ou seja ele executa a função de cima e espera 1 segundo pra executar a de baixo
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            //A gente colocou 1, 1, 1, 1 pra fazer "Branco" que faz as coisas voltarem a cor original delas
            //É como se mudasse a cor do papel embaixo da layer da pintura, mas o Alpha controla a pintura e não o papel
            spriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            //Esse iFramesDuration / (numberOfFlashes * 2) é uma maneira de garantir que ele faça o loop de mudança de cores
            //o numero de vezes que a gente quer dentro do tempo previsto
            //Tavez não funcione tão bem, mas funciona bem com 2 e 3 XD
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}
