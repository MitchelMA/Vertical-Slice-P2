using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image frontImage;
    [SerializeField] private Slider healthSlider;
    
    [SerializeField] private Color fullColour;
    [SerializeField] private Color depletedColour;

    public void UpdateBars(HealthChangeData data)
    {
        healthSlider.value = (float)data.Current / data.MaxHealth;
        frontImage.color = Color.Lerp(depletedColour, fullColour, healthSlider.value);
    }
}
