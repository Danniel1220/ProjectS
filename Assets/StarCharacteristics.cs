using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class StarCharacteristics : MonoBehaviour
{
    public float diameter;

    private ParticleSystem surfacePS;
    private ParticleSystem coronaPS;
    private ParticleSystem coronalMassEjectionPS;

    private Gradient surfaceGradient;
    private Gradient coronaGradient;
    private Gradient coronalMassEjectionGradient;

    public Color surfaceColor;
    public Color coronaColor;
    public Color coronalMassEjectionColor;

    // Start is called before the first frame update
    void Start()
    {
        surfacePS = this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        coronaPS = surfacePS.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        coronalMassEjectionPS = surfacePS.gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

        ColorOverLifetimeModule surfaceColorOverLifeTimeGradient = surfacePS.colorOverLifetime;
        ColorOverLifetimeModule coronaColorOverLifeTimeGradient = coronaPS.colorOverLifetime;
        ColorOverLifetimeModule coronalMassEjectionColorOverLifeTimeGradient = coronalMassEjectionPS.colorOverLifetime;

        surfaceGradient.SetKeys(
            new GradientColorKey[] { 
                new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.red, 1.0f) }, 
            new GradientAlphaKey[] { 
                new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

        coronaGradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

        coronalMassEjectionGradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

        // the reason why i first grab a refference to the value i want to change is because unity cannot directly change the property inside a struct
        // this is actually not really a grab by refference, it is still by value but the struct seems to hold a refference regardless, so it works!
        // see here: https://stackoverflow.com/questions/74245785/why-cant-i-change-value-without-first-assigning-a-variable

        surfaceColorOverLifeTimeGradient.color = surfaceGradient;
        coronaColorOverLifeTimeGradient.color = coronaGradient;
        coronalMassEjectionColorOverLifeTimeGradient.color = coronalMassEjectionGradient;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
