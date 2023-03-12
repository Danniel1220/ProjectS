using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back }
    public FaceRenderMask faceRenderMask;

    [HideInInspector] public bool shapeSettingsFoldout;
    [HideInInspector] public bool colorSettingsFoldout;

    PlanetShapeGenerator shapeGenerator = new PlanetShapeGenerator();
    PlanetColorGenerator colorGenerator = new PlanetColorGenerator();

    public PlanetShapeSettings shapeSettings;
    public PlanetColorSettings colorSettings;

    [SerializeField, HideInInspector] MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    void initializeMesh()
    {
        shapeGenerator.updateSettings(shapeSettings);
        colorGenerator.updateSettings(colorSettings);

        // a cube has 6 faces, so 6 meshes
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;

            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }
    }

    public void generatePlanet()
    {
        initializeMesh();
        generateMesh();
        generateColors();
    }

    public void onShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            initializeMesh();
            generateMesh();
        }
    }

    public void onColorSettingsUpdated()
    {
        if (autoUpdate)
        {
            initializeMesh();
            generateColors();
        }
    }

    void generateMesh()
    {
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].constructMesh();
            }
        }

        colorGenerator.updateElevation(shapeGenerator.elevationMinMax);
    }

    void generateColors()
    {
        colorGenerator.updateColors();
    }
}
