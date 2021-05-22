using System;
using Classes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class CharacteristicBar : MonoBehaviour
    {
        public enum Type
        {
            Health,
            Armor
        }

        [SerializeField] private Type type;
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI text;

        private float maxValue;
        private float currentValue;

        private void Start()
        {
            switch (type)
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
            if (slider == null)
            {
                return;
            }
            currentValue = Player.Instance.Health.HP;

            var hp = currentValue / maxValue;
            slider.value = hp;
            text.text = $"{Mathf.Round(hp * 100)}";
        }
        private void ChangeArmor()
        {
            // TODO: connect to ArmorCharateristic.cs
            // currentValue = Player.Instance.Armor.Armor; ???
            // bar.fillAmount = currentValue / maxValue;
        }
    }
}
