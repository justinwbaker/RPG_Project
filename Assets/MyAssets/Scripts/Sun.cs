using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

    private Light sun;
    private float secondsInDay = 300f;

    private  float currentTimeOfDay = 0.5f;
    private float timeMultiplier = 1f;
    private float sunInitialIntensity;

	// Use this for initialization
	void Start () {
        sun = GetComponent<Light>();
        sunInitialIntensity = sun.intensity;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateSun();

        currentTimeOfDay += (Time.deltaTime / secondsInDay) * timeMultiplier;

        if(currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
        }

	}

    private void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay*360)-90, 170, 0);

        float intensityMultiplier = 1;

        if(currentTimeOfDay <= 0.23 || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }else if (currentTimeOfDay <= 0.25)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay-0.23f) * (1/0.02f));
        }else if (currentTimeOfDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1-((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}
