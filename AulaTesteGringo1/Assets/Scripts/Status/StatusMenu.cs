using NUnit.Framework.Internal;
using UnityEngine;

public class StatusMenu : MonoBehaviour
{
    [SerializeField] Status status;

    public void VitalityButton()
    {
        status.VitalityUp();
        gameObject.SetActive(false);
    }

    public void AttackButton()
    {
        status.AttackUp();
        gameObject.SetActive(false);
    }

    public void AtkSpdButton()
    {
        status.AtkSpdUp();
        gameObject.SetActive(false);
    }
}
