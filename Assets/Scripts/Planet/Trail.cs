using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    private TrailRenderer trailRenderer;

    public Color trailColor;
    public float trailTime;
    // Start is called before the first frame update
    void Start()
    {
        trailColor = Color.red;
        trailTime = 5f;
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.colorGradient = ParseColorIntoColorGradient(trailColor);
        trailRenderer.time = trailTime;

        // this line below just grabs the material Default-Line, see here why
        // https://stackoverflow.com/questions/72240485/how-to-add-the-default-line-material-back-to-the-linerenderer-material
        trailRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Gradient ParseColorIntoColorGradient(Color color)
    {
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f) },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(1f, 0.3f), new GradientAlphaKey(1f, 0.5f), new GradientAlphaKey(0.8f, 0.6f), new GradientAlphaKey(0f, 1f) });
        return gradient;
    }
}
