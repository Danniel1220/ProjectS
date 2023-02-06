using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class StarCharacteristics : MonoBehaviour
{
    public float diameter;

    private Transform starSphere;

    private ParticleSystem surfacePS;
    private ParticleSystem coronaPS;
    private ParticleSystem coronalMassEjectionPS;
    private ParticleSystem farFlaresPS;
    private ParticleSystem lensFlarePS;

    private Gradient surfaceGradient = new Gradient();
    private Gradient coronaGradient = new Gradient();
    private Gradient coronalMassEjectionGradient = new Gradient();
    private Gradient farFlaresGradient = new Gradient();
    private Gradient lensFlareGradient = new Gradient();

    public Color starSphereColor;
    public Color surfaceColor;
    public Color coronaColor;
    public Color coronalMassEjectionColor;
    public Color farFlaresColor;
    public Color lensFlareColor;

    // Start is called before the first frame update
    void Start()
    {
        diameter = 1.4f; // 1 unit = 1 million km

        starSphere = this.transform.GetChild(2).gameObject.GetComponent<Transform>();

        // fetching the gameobjects for all the particle systems
        surfacePS = this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        coronaPS = surfacePS.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        coronalMassEjectionPS = surfacePS.gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        farFlaresPS = this.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        lensFlarePS = farFlaresPS.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();

        // fetching refferences for all color gradients inside the particle systems
        ColorOverLifetimeModule surfaceColorOverLifeTimeGradient = surfacePS.colorOverLifetime;
        ColorOverLifetimeModule coronaColorOverLifeTimeGradient = coronaPS.colorOverLifetime;
        ColorOverLifetimeModule coronalMassEjectionColorOverLifeTimeGradient = coronalMassEjectionPS.colorOverLifetime;
        ColorOverLifetimeModule farFlaresColorOverLifeTimeGradient = farFlaresPS.colorOverLifetime;
        ColorOverLifetimeModule lensFlareLifeTimeGradient = lensFlarePS.colorOverLifetime;
        // the reason why i first grab a refference to the value i want to change is because unity cannot directly change the property inside a struct
        // this is actually not really a grab by refference, it is still by value but the struct seems to hold a refference regardless, so it works!
        // see here: https://stackoverflow.com/questions/74245785/why-cant-i-change-value-without-first-assigning-a-variable

        starSphereColor = Color.red;

        surfaceColor = Color.red;
        coronaColor = Color.yellow;
        coronalMassEjectionColor = Color.yellow;
        farFlaresColor = Color.yellow;
        lensFlareColor = Color.yellow;

        // parsing the colors of each particle system to the gradient it requires to operate
        ParseColorIntoColorGradient(surfaceGradient, surfaceColor);
        ParseColorIntoColorGradient(coronaGradient, coronaColor);
        ParseColorIntoColorGradient(coronalMassEjectionGradient, coronalMassEjectionColor);
        ParseColorIntoColorGradient(farFlaresGradient, farFlaresColor);
        ParseColorIntoColorGradient(lensFlareGradient, lensFlareColor);

        

        starSphere.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

        surfaceColorOverLifeTimeGradient.color = surfaceGradient;
        coronaColorOverLifeTimeGradient.color = coronaGradient;
        coronalMassEjectionColorOverLifeTimeGradient.color = coronalMassEjectionGradient;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ParseColorIntoColorGradient(Gradient gradient, Color color)
    {
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f) },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(1f, 0.2f), new GradientAlphaKey(1f, 0.8f), new GradientAlphaKey(0.0f, 1.0f) });
    }
}
