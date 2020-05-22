using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstructiveSolidGeometry;
using GK;

public class IntersectionCSGTry : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public GameObject newObjectPrefab;
    //public GameObject newGo;

    void Start()
    {

        Transform[] children = new Transform[2];
        if (a == null && b == null)
        {
            int i = 0;
            foreach (Transform t in transform)
            {
                if (i > 2) break;
                children[i] = t;
                i++;
            }
        }
        else
        {
            children[0] = a.transform;
            children[1] = b.transform;
        }

        CSG A = CSG.fromMesh(children[0].GetComponent<MeshFilter>().mesh, children[0]);
        CSG B = CSG.fromMesh(children[1].GetComponent<MeshFilter>().mesh, children[1]);

        CSG result = null;
    
        result = A.intersect(B);

        /*
         * Debug.Log(A.polygons.Count + ", " + B.polygons.Count + ", " + result.polygons.Count);
        foreach (Polygon p in result.polygons) {
            Debug.Log("Result: " + p.vertices[0].pos+", "+p.vertices[1].pos+", "+p.vertices[2].pos);
            if (p.vertices.Length > 3) Debug.Log("!!! " + p.vertices.Length);
        }
        */

        GameObject newGo = Instantiate(newObjectPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        if (result != null) newGo.GetComponent<MeshFilter>().mesh = result.toMesh();
        children[0].gameObject.SetActive(false);
        children[1].gameObject.SetActive(false);
        newObjectPrefab.SetActive(false);

        Debug.Log("Volume A: " + VolumeOfMesh(children[0].gameObject.GetComponent<MeshFilter>().mesh) * 1000000);

        Debug.Log("Result Volume: " + VolumeOfMesh(newGo.GetComponent<MeshFilter>().mesh)*1000000);

        Debug.Log("A verts, normals, tris: " + children[0].gameObject.GetComponent<MeshFilter>().mesh.vertices.Length + ", " + children[0].gameObject.GetComponent<MeshFilter>().mesh.normals.Length + ", " + children[0].gameObject.GetComponent<MeshFilter>().mesh.triangles.Length);

    }

    float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float v321 = p3.x * p2.y * p1.z;
        float v231 = p2.x * p3.y * p1.z;
        float v312 = p3.x * p1.y * p2.z;
        float v132 = p1.x * p3.y * p2.z;
        float v213 = p2.x * p1.y * p3.z;
        float v123 = p1.x * p2.y * p3.z;
        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }

    float VolumeOfMesh(Mesh mesh)
    {
        float volume = 0;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            volume += SignedVolumeOfTriangle(p1, p2, p3);
        }
        return Mathf.Abs(volume);
    }
}
