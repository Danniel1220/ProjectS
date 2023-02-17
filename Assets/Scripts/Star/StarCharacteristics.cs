using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class StarCharacteristics : MonoBehaviour
{
    public float diameter; // 1 unit = 1 million km

    private Transform starSphere;

    private ParticleSystem surfaceParticleSystem;
    private ParticleSystem coronaParticleSystem;
    private ParticleSystem coronalMassEjectionParticleSystem;
    private ParticleSystem flaresParticleSystem;
    private ParticleSystem lensFlareParticleSystem;

    private Gradient surfaceGradient = new Gradient();
    private Gradient coronaGradient = new Gradient();
    private Gradient coronalMassEjectionGradient = new Gradient();
    private Gradient farFlaresGradient = new Gradient();
    private Gradient lensFlareGradient = new Gradient();

    public Color surfaceColor;
    public Color coronaColor;
    public Color coronalMassEjectionColor;
    public Color flaresColor;
    public Color lensFlareColor;

    // Start is called before the first frame update
    void Start()
    { 
        starSphere = this.transform.Find("StarSphere").gameObject.GetComponent<Transform>();

        // fetching the gameobjects for all the particle systems
        surfaceParticleSystem = this.transform.Find("SurfacePS").gameObject.GetComponent<ParticleSystem>();
        coronaParticleSystem = surfaceParticleSystem.gameObject.transform.Find("CoronaPS").gameObject.GetComponent<ParticleSystem>();
        coronalMassEjectionParticleSystem = surfaceParticleSystem.gameObject.transform.Find("CoronalMassEjectionPS").gameObject.GetComponent<ParticleSystem>();
        flaresParticleSystem = this.transform.Find("FlaresPS").gameObject.GetComponent<ParticleSystem>();
        lensFlareParticleSystem = flaresParticleSystem.gameObject.transform.Find("LensFlarePS").gameObject.GetComponent<ParticleSystem>();

        // fetching refferences for all color gradients inside the particle systems
        ColorOverLifetimeModule surfaceColorOverLifeTimeGradient = surfaceParticleSystem.colorOverLifetime;
        ColorOverLifetimeModule coronaColorOverLifeTimeGradient = coronaParticleSystem.colorOverLifetime;
        ColorOverLifetimeModule coronalMassEjectionColorOverLifeTimeGradient = coronalMassEjectionParticleSystem.colorOverLifetime;
        ColorOverLifetimeModule farFlaresColorOverLifeTimeGradient = flaresParticleSystem.colorOverLifetime;
        ColorOverLifetimeModule lensFlareLifeTimeGradient = lensFlareParticleSystem.colorOverLifetime;
        // the reason why i first grab a refference to the value i want to change is because unity cannot directly change the property inside a struct
        // this is actually not really a grab by refference, it is still by value but the struct seems to hold a refference regardless, so it works!
        // see here: https://stackoverflow.com/questions/74245785/why-cant-i-change-value-without-first-assigning-a-variable

        // parsing the colors of each particle system to the gradient it requires to operate
        ParseColorIntoColorGradient(surfaceGradient, surfaceColor);
        ParseColorIntoColorGradient(coronaGradient, coronaColor);
        ParseColorIntoColorGradient(coronalMassEjectionGradient, coronalMassEjectionColor);
        ParseColorIntoColorGradient(farFlaresGradient, flaresColor);
        ParseColorIntoColorGradient(lensFlareGradient, lensFlareColor);

        // assigning colors to particle systems
        surfaceColorOverLifeTimeGradient.color = surfaceGradient;
        coronaColorOverLifeTimeGradient.color = coronaGradient;
        coronalMassEjectionColorOverLifeTimeGradient.color = coronalMassEjectionGradient;

        starSphere.localScale = new Vector3(diameter, diameter, diameter);

        ParticleSystem.ShapeModule shapeModuleSurfaceParticleSystem = surfaceParticleSystem.shape;
        shapeModuleSurfaceParticleSystem.radius = diameter * 0.5f;

        ParticleSystem.ShapeModule shapeModuleCoronaParticleSystem = coronaParticleSystem.shape;
        shapeModuleCoronaParticleSystem.radius = diameter * 0.5f - 5f;

        ParticleSystem.ShapeModule shapeModuleCoronalMassEjectionParticleSystem = coronalMassEjectionParticleSystem.shape;
        shapeModuleCoronalMassEjectionParticleSystem.radius = diameter * 0.5f - 10f;

        ParticleSystem.ShapeModule shapeModuleLensFlareParticleSystem = lensFlareParticleSystem.shape;
        shapeModuleLensFlareParticleSystem.radius = diameter * 0.5f - 10f;
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
