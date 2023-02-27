using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetHunger(float hunger)
    {
        slider.value = hunger;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHunger(float hunger)
    {
        slider.maxValue = hunger;
        slider.value = hunger;

        fill.color = gradient.Evaluate(1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
