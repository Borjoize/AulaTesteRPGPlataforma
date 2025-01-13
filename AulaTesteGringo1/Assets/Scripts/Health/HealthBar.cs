using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
//Pra funcionar eu tive que tirar uma classe q tava antes e adicionei o "using UnityEngine.UI;"


public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        //totalHealthBar.fillAmount = playerHealth._maxHealth / 10;
    }

    private void Update()
    {
        totalHealthBar.fillAmount = playerHealth._maxHealth / 10;
        currentHealthBar.fillAmount = playerHealth._currentHealth / 10;
    }
}

