using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingot_Material_Lerp : MonoBehaviour {

    // Blends between two materials

    public Material material1;
    public Material material2;
    public Light pointLight;

    public Gradient lightColor;

    [Range(0f, 1f)]
    public float TemperaturePercent = 0.0f;

    private Renderer rend;
    private bool isInHeatSource = false;

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
            TemperaturePercent += Time.deltaTime/10;
        }
        // ping-pong between the materials over the duration
        pointLight.intensity = TemperaturePercent * 10;
        rend.material.Lerp(material1, material2, TemperaturePercent);
        pointLight.color = lightColor.Evaluate(TemperaturePercent);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "HeatSource")
        {
            isInHeatSource = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "HeatSource")
        {
            isInHeatSource = false;
        }
    }
}
