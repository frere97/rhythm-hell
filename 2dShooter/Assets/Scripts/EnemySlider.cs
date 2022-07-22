using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlider : MonoBehaviour
{
    Slider slider;
    Enemy enemy;
    void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        slider = GetComponent<Slider>();
        slider.maxValue = enemy.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = enemy.CurrentHealth;
    }
}
