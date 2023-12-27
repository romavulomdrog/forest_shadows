using UnityEngine;
using System.Collections.Generic;


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


public static class GenGrassClass
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public static Vector3 Vm(Vector3 locScale, Vector3 vertPos, Vector3[] VertMesh, int m, Vector3 roteGras, float sclWGras, float sclHGras)
    {
        //--------------

        Vector3 vm = VertMesh[m];
        vm = Quaternion.Euler(roteGras.x, roteGras.y, roteGras.z) * vm;
        vm.x = (vm.x * sclWGras * locScale.x) + vertPos.x;
        vm.y = (vm.y * sclHGras) + vertPos.y;
        vm.z = (vm.z * sclWGras * locScale.z) + vertPos.z;
        return vm;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public static Vector3 horizontalPos(Vector3 vertices, float scatter, float scale)
    {
        //--------------

        Vector3 vertPos = Vector3.zero;
        Vector2 rndSp = Random.insideUnitCircle * scatter;
        vertPos.z = ((vertices.z + rndSp.y) * scale) - rndSp.y * 0.5f;
        vertPos.x = ((vertices.x + rndSp.x) * scale) - rndSp.x * 0.5f;
        return vertPos;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public static Mesh GrassMesh(List<Vector3> verticesGrass, List<Vector3> gListNormals, List<Vector2> gListUv, List<Vector2> gListUv2, bool normalRecalc, bool triangles)
    {
        //--------------

        Mesh mGrass = new Mesh();
        mGrass.SetVertices(verticesGrass);
        mGrass.SetNormals(gListNormals);
        mGrass.SetUVs(0, gListUv);
        mGrass.SetUVs(1, gListUv2);
        int[] triangesZ = new int[verticesGrass.Count];
        for (int g = 0; g < verticesGrass.Count; g++) triangesZ[g] = g;
        if (!triangles) mGrass.SetIndices(triangesZ, MeshTopology.Quads, 0);
        else mGrass.SetIndices(triangesZ, MeshTopology.Triangles, 0);
        mGrass.RecalculateBounds();
        if (normalRecalc == true) mGrass.RecalculateNormals();
        return mGrass;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}