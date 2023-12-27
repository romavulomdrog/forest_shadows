using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Grass : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public Mesh GrassMesh;
    public int grassCount = 50;
    public int dellay = 7;
    public int distance = 400;
    [Header("----------------------------------------------")]
    public Vector2 highGras = new Vector2(1, 1);
    public Vector2 widthGras = new Vector2(1, 1);
    public int randNakl = 60;
    [Header("----------------------------------------------")]
    public bool triangles = false;
    public bool normalRecalc = false;

    //--------------

    Transform tr;
    Vector3[] gNormals, vertMesh;
    Vector2[] gUv, gUv2;
    List<Vector3> verticesGrass, gListNormals;
    List<Vector2> gListUv, gListUv2;
    Vector3 treePos, vdf3, ltwV, locScale;
    int vertMeshLenght;
    static WaitForSeconds dellay0 = new WaitForSeconds(0.5f);


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Awake()
    {
        //--------------

        tr = transform;
        Vector3 scl = tr.localScale;
        locScale = new Vector3(1 / scl.x, 1, 1 / scl.z);

        verticesGrass = new List<Vector3>();
        gListNormals = new List<Vector3>();
        gNormals = GrassMesh.normals;
        gListUv = new List<Vector2>();
        gUv = GrassMesh.uv;
        gListUv2 = new List<Vector2>();
        gUv2 = GrassMesh.uv2;
        vertMesh = GrassMesh.vertices;
        vertMeshLenght = vertMesh.Length;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    private void OnEnable()
    {
        //--------------

        StartCoroutine(GrassGen());

        //--------------
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    IEnumerator GrassGen()
    {
        //--------------

        while (GlVAR.DistanceToPlayer(tr.position) > distance) yield return dellay0;

        for (int v = 0; v < grassCount; v++)
        {
            if (v % dellay == 0) yield return null;

            Vector3 vertPos = new Vector3(Random.Range(-5.1f, 5.1f), tr.position.y - 0.05f, Random.Range(-5.1f, 5.1f));
            float sclHGras = Random.Range(highGras.x, highGras.y);
            float sclWGras = Random.Range(widthGras.x, widthGras.y);
            Vector3 roteGras = new Vector3(Random.Range(2, randNakl), Random.Range(-720, 720), Random.Range(2, randNakl));
            float randUv = Random.value;

            for (var m = 0; m < vertMeshLenght; m++)
            {
                verticesGrass.Add(GenGrassClass.Vm(locScale, vertPos, vertMesh, m, roteGras, sclWGras, sclHGras));
                gListUv.Add(new Vector2(randUv, gUv[m].y));
                gListUv2.Add(gUv2[m]);
                gListNormals.Add(gNormals[m]);
            }
        }

        GetComponent<MeshFilter>().mesh = GenGrassClass.GrassMesh(verticesGrass, gListNormals, gListUv, gListUv2, normalRecalc, triangles);

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}