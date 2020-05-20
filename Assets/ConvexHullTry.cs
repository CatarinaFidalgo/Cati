using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

namespace GK
{
	public class ConvexHullTry : MonoBehaviour
	{

		public GameObject initialMesh;
		//public Transform trail;
		public int i = 0;		
		public bool generateHullDone = true;
		public SaveTrailPoints saveTrailPoints;

		IEnumerator Start()
		{
			//Parametros necessarios para o algoritmo de Convex Hull

			var calc = new ConvexHullCalculator();
			var verts = new List<Vector3>();
			var tris = new List<int>();
			var normals = new List<Vector3>();
			
			while (true)
			{
				if (saveTrailPoints.pressed == false && generateHullDone == false)
				{
					
					calc.GenerateHull(saveTrailPoints.pointsTrail, true, ref verts, ref tris, ref normals);

					//Create an initial transform that will evolve into our Convex Hull when altering the mesh

					var initialHull = Instantiate(initialMesh);

					initialHull.transform.SetParent(transform, false);
					initialHull.transform.position = Vector3.zero;
					initialHull.transform.rotation = Quaternion.identity;
					initialHull.transform.localScale = Vector3.one;					

					//Independentemente do tipo de mesh com que se começa (cubo, esfera..) 
					//a mesh é redefenida com as definiçoes abaixo

					var mesh = new Mesh();
					mesh.SetVertices(verts);
					mesh.SetTriangles(tris, 0);
					mesh.SetNormals(normals);

					initialHull.GetComponent<MeshFilter>().sharedMesh = mesh;
					initialHull.GetComponent<MeshCollider>().sharedMesh = mesh;	
					
					//Limpar os pontos antigos da lista para o proximo convex hull e
					//informar o programa de que já realizou esta função 

					saveTrailPoints.pointsTrail.Clear();
					generateHullDone = true;
					//yield return new WaitForSeconds(1);
					
				}



				yield return new WaitForSeconds(0.5f);
				
			}
		}
				
	}
}
