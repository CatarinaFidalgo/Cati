﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstructiveSolidGeometry;
using GK;
using Oculus.Platform.Samples.VrHoops;
using System.Linq;
using System;

[System.Serializable]
public class IntersectionCalculator : MonoBehaviour
{
    public ConvexHullTryRemote chremote;
    public ConvexHullTry chlocal;

    public GameObject localVolumesParent;
    public GameObject remoteVolumesParent;
    //public List<Vector3> intersectionPoints = new List<Vector3>();
    //public float volumeOfUnion;
    public float volumeOfIntersection;
    //public Transform ball;
    public GameObject initialMeshCSG;
    //public List<Vector3> ballsList = new List<Vector3>();
    //public Transform POINTS;
    public int child;
    public float percentageOfIntersection;
    //public bool intersectionDone = false;
    //public HashSet<Vector3> unionHash = new HashSet<Vector3>();
    //public List<Vector3> unionList = new List<Vector3>();
    public bool writeResult;
    public string percentageString;

    public Evaluation eval;
    public StartEndLogs startEnd;

    private Mesh intersectionMesh;


    void Update()
    {
        //writeResult = false;

        if (chremote.nrCHremote == chlocal.nrCHlocal && chremote.nrCHremote != 0 && chlocal.nrCHlocal != 0 && chlocal.readyForIntersectionLocal && chremote.readyForIntersectionRemote && eval.localIsDemonstrator)
        {
            //child = chremote.nrCHremote - 1;
            Debug.Log("Initiate Intersection");
            try
            {

                child = chremote.nrCHremote - 1;

                CSG A = CSG.fromMesh(localVolumesParent.transform.GetChild(child).GetComponent<MeshFilter>().mesh, localVolumesParent.transform.GetChild(child));
                CSG B = CSG.fromMesh(remoteVolumesParent.transform.GetChild(child).GetComponent<MeshFilter>().mesh, remoteVolumesParent.transform.GetChild(child));

                CSG result = null;
                result = A.intersect(B);

                //Debug.Log(A.polygons.Count + ", " + B.polygons.Count + ", " + result.polygons.Count);


                if (result != null)
                {
                    GameObject newGo = Instantiate(initialMeshCSG, Vector3.zero, Quaternion.identity) as GameObject;
                    intersectionMesh = result.toMesh();
                    newGo.GetComponent<MeshFilter>().mesh = intersectionMesh;

                    //Calcular o volume da mesh
                    volumeOfIntersection = VolumeOfMesh(intersectionMesh) * 1000000; //convert to cm3  

                    //Compare the volume of intersection with the volume that the local pointed

                    percentageOfIntersection = volumeOfIntersection / chlocal.volume * 100.0f;
                }

                else
                {
                    percentageOfIntersection = 0.0f;
                }

                Debug.Log("Percentage of Intersection: " + percentageOfIntersection.ToString("F0") + "%");

                percentageString = chlocal.volume.ToString() + '#' + chremote.volume.ToString() + '#' + volumeOfIntersection.ToString() + '#' + percentageOfIntersection.ToString("F1");
                writeResult = true;


                initialMeshCSG.SetActive(false);
                localVolumesParent.transform.GetChild(child).gameObject.SetActive(false);
                remoteVolumesParent.transform.GetChild(child).gameObject.SetActive(false);



                chlocal.readyForIntersectionLocal = false;
                chremote.readyForIntersectionRemote = false;

                Debug.Log("Intersection write = " + writeResult);

            }

            catch (System.ArgumentException)
            {


                initialMeshCSG.SetActive(false);
                localVolumesParent.transform.GetChild(child).gameObject.SetActive(false);
                remoteVolumesParent.transform.GetChild(child).gameObject.SetActive(false);


                chlocal.readyForIntersectionLocal = false;
                chremote.readyForIntersectionRemote = false;

                /*foreach (Transform child in POINTS)
                {
                    Destroy(child.gameObject);
                }*/

                startEnd.getStartTime = true;

            }

            catch (UnityEngine.Assertions.AssertionException)
            {
                initialMeshCSG.SetActive(false);
                localVolumesParent.transform.GetChild(child).gameObject.SetActive(false);
                remoteVolumesParent.transform.GetChild(child).gameObject.SetActive(false);


                chlocal.readyForIntersectionLocal = false;
                chremote.readyForIntersectionRemote = false;

                /*foreach (Transform child in POINTS)
                {
                    Destroy(child.gameObject);
                }*/

                startEnd.getStartTime = true;
            }

        }

    }

    float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float v321 = p3.x * p2.y * p1.z;
        float v231 = p2.x * p3.y * p1.z;
        float v312 = p3.x * p1.y * p2.z;
        float v132 = p1.x * p3.y * p2.z;
        float v213 = p2.x * p1.y * p3.z;
        float v123 = p1.x * p2.y * p3.z;
        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123); //Volume do tetraedro
    }

    float VolumeOfMesh(Mesh mesh)
    {
        float volume = 0;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        //Debug.Log("nr vertices in function: " + vertices.Length);
        //Debug.Log("nr triangles in function: " + triangles.Length);

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