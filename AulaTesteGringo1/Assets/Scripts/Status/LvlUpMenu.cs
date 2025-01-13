using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LvlUpMenu : MonoBehaviour
{
    [SerializeField] private Status playerStatus;
    [SerializeField] private Image totalXPBar;
    [SerializeField] private Image currentXPBar;
    [SerializeField] private TMP_Text lelvelText;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text atkSpeedText;

   // private Status status;


    private void Awake()
    {
    //    status = GetComponent<Status>();
    }

    private void Update()
    {
        //Teve que botar o f no 10 porque ele tava dividindo int por int, então o f no 10 converte pra float que é divisível
        totalXPBar.fillAmount = playerStatus.maxXP / 10f;
        currentXPBar.fillAmount = playerStatus.xp / 10f;

        lelvelText.text = "Level: " + playerStatus.playerLvl;
        attackText.text = "Attack: " + playerStatus.playerAtk;
        atkSpeedText.text = "AtkSpd: " + playerStatus.atkSpd;
    }
}

