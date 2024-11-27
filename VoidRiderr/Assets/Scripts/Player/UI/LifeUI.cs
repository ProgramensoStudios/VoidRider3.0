using System;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
   [SerializeField] private PlayerHealth playerHealth;
   private Slider _slider;
   [SerializeField] private Image img;

   private void Awake()
   {
      _slider = GetComponent<Slider>();
   }

   private void OnEnable()
   {
      playerHealth.OnReceiveDamage += UpdateUI;
   }
   private void OnDisable()
   {
      playerHealth.OnReceiveDamage -= UpdateUI;
   }

   private void UpdateUI(int health)
   {
      _slider.value = health * 0.01f;
      img.color = health switch
      {
         >= 60 => Color.green,
         >= 30 => Color.yellow,
         _ => Color.red
      };
   }
}
