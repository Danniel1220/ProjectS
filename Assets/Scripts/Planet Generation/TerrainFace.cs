using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
    PlanetShapeGenerator shapeGenerator;
    Mesh mesh;
    int resolution;
    Vector3 localUp; // local up is axis Y, locally
    Vector3 axisX;
    Vector3 axisZ;

    public TerrainFace(PlanetShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp)
    {
        this.shapeGenerator = shapeGenerator;
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;
        axisX = new Vector3(localUp.y, localUp.z, localUp.x);
        axisZ = Vector3.Cross(localUp, axisX);
    }

    public void constructMesh()
    {
        // this stores the verts of the mesh
        Vector3[] vertices = new Vector3[resolution * resolution];
        // this stores the verts that form triangles, so basically it stores the triangles as such:
        // i = 0,1,2 is triangle 1, i = 3,4,5 is triangle 2, and so on 
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int triIndex = 0;

        // i is basically the number of total iterations both loops have gone through in total

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;

                // basically how close we are to completion for each loop
                // percent.x is how close the inner loop is to completion
                // percent.y is how close the outer loop is to completion
                // so for percent = (0.3, 0,5) the outer loop is 50% complete while the inner loop is 30% complete
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                // compute where the current vertex should be located on the initial cube
                Vector3 pointOnUnitCube = localUp + (percent.x - 0.5f) * 2 * axisX + (percent.y - 0.5f) * 2 * axisZ;
                // compute where the current vertex should be located on the final sphere
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                vertices[i] = shapeGenerator.calculatePointOnPlanet(pointOnUnitSphere);

                // we only want to calculate tris for the verts that wouldnt generate a tri outside the mesh
                // so the last column and last row of the verts array are not included in the calculation
                if (x != resolution - 1 && y != resolution - 1)
                {
                    // triangle 1
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i + resolution + 1;
                    triangles[triIndex + 2] = i + resolution;

                    // triangle 2
                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;

                    triIndex += 6;
                }
            }
        }
        // reconstruct mesh
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
