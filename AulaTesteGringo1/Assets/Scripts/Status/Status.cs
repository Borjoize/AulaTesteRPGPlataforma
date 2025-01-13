using System;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Status : MonoBehaviour
{
    public int xp =0;

    private int xpValue =1;

    public int maxXP =10;

    public int playerLvl =1;

    public int lvlPoints =0;

    public float playerAtk =1;

    public float atkSpd;

    public Action<Status>OnVitalityUp;

    public Action<Status>OnAttackUp;

    private int vitality;
     private int attack;

    //UnityEvent m_OnDeath = new UnityEvent();

    [SerializeField] private GameObject panel;

    public int Vitality
    {
        set
        {
            vitality = value;

            OnVitalityUp?.Invoke(this);
            Debug.Log("Setei");
        }
        get
        {
            return vitality;
        }
    }

        public int Attack
    {
        set
        {
            attack = value;

            OnAttackUp?.Invoke(this);
        }
        get
        {
            return attack;
        }
    }

    /*private void Start()
    {
        m_OnDeath.AddListener(OnDeath);
    }*/

    /*private void OnEnable()
    {
        Debug.Log("OnEnable funcionou");
        Health health = GetComponent<Health>();
        if (health != null)
        {
            health.OnDeath += AddXp;

            Debug.Log("Tentei te dar XP");
        }   
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable funcionou");
        Health health = GetComponent<Health>();
        if (health != null)
        {
            health.OnDeath -= AddXp;
        }
    }*/


    //Usei pra testar o dano e ta
    [ContextMenu("AddXP")]
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            AddXp();
        }
    }

    public void AddXp()
    {
        xp += xpValue + 5;
        if (xp >= maxXP)
        {
            LvlUp();
            xp -= maxXP;
        }
        //onDeath?.Invoke();
    }

    public void LvlUp()
    {
        playerLvl++;
        lvlPoints++;
        panel.SetActive(true);

    }

    public void VitalityUp()
    {
        if (lvlPoints > 0)
        {
            Vitality++;
            lvlPoints--;
        }
    }

        public void AttackUp()
    {
        if (lvlPoints > 0)
        {
            playerAtk++;
            lvlPoints--;
        }
    }

        public void AtkSpdUp()
    {
        if (lvlPoints > 0)
        {
            atkSpd++;
            lvlPoints--;
        }
    }

}
