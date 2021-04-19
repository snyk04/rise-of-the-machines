using Classes;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicBarController : MonoBehaviour
{
    public enum Type
    {
        Health,
        Armor
    }

    [SerializeField] private Type type;
    [SerializeField] private Image bar;

    private float maxValue;
    private float currentValue;

    private delegate void Change();

    private void Start()
    {
        switch(type)
        {
            case Type.Health:
                maxValue = Player.Instance.Health.MaxHP;
                Player.Instance.Health.OnHpChange += ChangeHp;
                break;
            case Type.Armor:
                // maxValue = Player.Instance.Armor.MaxArmor;
                // Player.Instance.Armor.OnArmorChange += ChangeArmor;
                break;
        }
    }

    private void ChangeHp()
    {
        currentValue = Player.Instance.Health.HP;
        bar.fillAmount = currentValue / maxValue;
    }
    private void ChangeArmor()
    {
        // TODO: connect to ArmorCharateristic.cs
        // currentValue = Player.Instance.Armor.Armor; ???
        // bar.fillAmount = currentValue / maxValue;
    }
}
