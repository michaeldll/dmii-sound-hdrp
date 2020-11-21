using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class SkyboxSphere : MonoBehaviour
{
    // Private
    private void ResetDoubleSided()
    {
        // Todo : Find out why double sided false not working
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material material = meshRenderer.material;
        material.doubleSidedGI = false;

        // Material newMaterial = new Material(material);
        // newMaterial.name = "newMat";
        // meshRenderer.material = newMaterial;
    }

    private void InvertNormals()
    {
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] normals = mesh.normals;

        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -1 * normals[i];
        }

        mesh.normals = normals;

        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            int[] tris = mesh.GetTriangles(i);

            for (int j = 0; j < tris.Length; j += 3)
            {
                //swap order of tri vertices
                int temp = tris[j];
                tris[j] = tris[j + 1];
                tris[j + 1] = temp;
            }

            mesh.SetTriangles(tris, i);
        }
    }

    // Hooks
    void Start()
    {
        ResetDoubleSided();
        InvertNormals();
    }
}