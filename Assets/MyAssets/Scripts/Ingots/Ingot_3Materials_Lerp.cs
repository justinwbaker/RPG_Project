using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingot_3Materials_Lerp : MonoBehaviour {

    // Blends between two materials

    public Material material1;
    public Material material2;
    public Material material3;
    public Light pointLight;
    public float transferRate = 1;

    public Gradient lightColor;

    [Range(0f, 1f)]
    public float TemperaturePercent = 0.0f;

    private Renderer rend;
    private bool isInHeatSource = false;
    private HeatSource heatSource;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // At start, use the first material
        rend.material = material1;
    }

    void Update()
    {
        if (TemperaturePercent < 1 && isInHeatSource)
        {
            TemperaturePercent += Time.deltaTime*(heatSource.temperature/100)*transferRate / 10;
        }
        else if(TemperaturePercent > 0 && !isInHeatSource)
        {
            TemperaturePercent -= Time.deltaTime*transferRate / 10;
        }

        if (TemperaturePercent > 1) TemperaturePercent = 1;
        if (TemperaturePercent < 0) TemperaturePercent = 0;
        // ping-pong between the materials over the duration
        if (TemperaturePercent < 0.5)
        {
            pointLight.intensity = 0;
            rend.material.Lerp(material1, material2, (TemperaturePercent) * 2);
        }
        else
        {
            rend.material.Lerp(material2, material3, (TemperaturePercent - 0.5f) * 2);
            pointLight.intensity = Mathf.Lerp(0, 5f, TemperaturePercent);
        }
        pointLight.color = lightColor.Evaluate(TemperaturePercent);
        isInHeatSource = false;
        heatSource = null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "HeatSource")
        {
            isInHeatSource = true;
            heatSource = other.gameObject.GetComponent<HeatSource>();
        }
    }
}
