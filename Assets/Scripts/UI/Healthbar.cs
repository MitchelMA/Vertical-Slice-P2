using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    
    [SerializeField] private Slider healthSlider;
    
    
    public void UpdateBars(HealthChangeData data)
    {
        healthSlider.value = (float)data.Current / data.MaxHealth;
        
    }
}
