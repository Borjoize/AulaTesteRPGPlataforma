using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Status : MonoBehaviour
{
    public int xp =0;

    private int xpValue =1;

    public int maxXP =10;

    public int playerLvl =1;

    public int lvlPoints =0;

    public float playerAtk;

    public float atkSpd;

    public Action<Status>OnVitalityUp;

    private int vitality;

    [SerializeField] private GameObject panel;

    public int Vitality
    {
        set
        {
            vitality = value;

            OnVitalityUp?.Invoke(this);
        }
        get
        {
            return vitality;
        }
    }

    [ContextMenu("AddXP")]
    public void AddXp()
    {
        xp += xpValue + 5;
        if (xp >= maxXP)
        {
            LvlUp();
            xp -= maxXP;
        }
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
