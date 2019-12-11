using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorRegion : MonoBehaviour
{
    public float height;
    public float width;
    public int numberOfPoint;
    public Material material;
    private float heightmin;

    private float sizeMax;
    public Vector3[] vertice;
    private int trig1 = 0, trig2 = 1, trig3 = 1;
    private GameObject game;

    private void Start()
    {
        game = new GameObject();
        game.AddComponent<MeshFilter>();
        game.AddComponent<MeshRenderer>();
        DoMesh();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DoMesh();
        }
    }

    public void DoMesh()
    {
        game.GetComponent<MeshFilter>().mesh = GenerateMesh();
        game.GetComponent<MeshRenderer>().material = new Material(material);
        
        vertice = game.GetComponent<MeshFilter>().mesh.vertices;
    }

    Mesh GenerateMesh()
    {
        trig1 = 0;
        trig2 = 1;
        trig3 = 1;
        sizeMax = 0;
        heightmin = 0;
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[numberOfPoint];
        for (int i = 0; i < vertices.Length; i++)
        {


            if (i % 2 == 0)
            {
                if (i == 0)
                {
                    vertices[i] = new Vector3(0, 0, 0);
                }
                else
                {
                    vertices[i] = new Vector3(Random.Range(-width / 2, width / 2), Random.Range(0, height / 2) + heightmin, 0);

                }
            }
            if (i % 2 == 1)
            {
                vertices[i] = new Vector3((width / 2) + Random.Range(0, width / 2), Random.Range(0, height / 2) + heightmin, 0);
                heightmin += (height / 2);
            }
        }
       
        mesh.vertices = vertices;


        int numbCurrent = numberOfPoint;
        if (numberOfPoint % 2 == 1)
        {
            numbCurrent++;
        }

        int[] tris = new int[(numbCurrent / 2) * 3];


        for (int i = 0; i < tris.Length; i++)
        {
            if (i % 6 == 0)
            {
                tris[i] = trig1;
            }
            if (i % 6 == 1 || i % 6 == 4)
            {
                trig2++;
                tris[i] = trig2;
            }
            if (i % 6 == 2)
            {

                tris[i] = trig3;
            }
            if (i % 6 == 3)
            {
                trig1 += 2;
                tris[i] = trig1;
            }
            if (i % 6 == 5)
            {
                tris[i] = trig3;
                trig3 += 2;
            }
        }

       
        mesh.triangles = tris;

        Vector3[] normals = new Vector3[numberOfPoint];
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -Vector3.forward;
        }
       
        mesh.normals = normals;
        float sizePositif = 0;
        float sizeNegatif = 0;
        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i].x > sizePositif)
            {
                sizePositif = vertices[i].x;
            }
            if (vertices[i].y > sizePositif)
            {
                sizePositif = vertices[i].y;
            }
            if (vertices[i].x < sizeNegatif)
            {
                sizeNegatif = vertices[i].x;
            }
            if (vertices[i].y < sizeNegatif)
            {
                sizeNegatif = vertices[i].y;
            }


        }

        sizeMax = sizePositif + Mathf.Abs(sizeNegatif);
        Vector2[] uv = new Vector2[numberOfPoint];

        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = new Vector2(vertices[i].x / sizeMax, vertices[i].y / sizeMax);
        }

    
        mesh.uv = uv;
        return mesh;
    }

}
