using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeSlider : MonoBehaviour
{
    Slider slider;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        player = Player.instance;
        slider.maxValue = player.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        slider.value = player.CurrentHealth;
    }
}
