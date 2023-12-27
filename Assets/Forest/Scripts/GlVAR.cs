using UnityEngine;

[SerializeField]
public static class GlVAR
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public static int worldSeed;

    public static Vector3 playerPosition;
    public static int qtObj;
    public static bool debug;

    public static int debNumNxLisoe;
    public static int debNumNxElka;
    public static int debNumNxListven;
    public static int debNumNxListvenShar;
    public static int debNumkamenNx1;
    public static int debNumkamenNx2;
    public static int debNumkamenNx3;
    public static int debNumkamenNx4;

    public static bool bereza1;
    public static bool bereza2;
    public static bool elka;
    public static bool lisoe;
    public static bool kamen1;
    public static bool kamen2;
    public static bool kamen3;
    public static bool kamen4;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public static int ToLayer(int bitmask)
    {
        //--------------

        int result = bitmask > 0 ? 0 : 31;

        while (bitmask > 1)
        {
            bitmask = bitmask >> 1;
            result++;
        }

        return result;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public static float Distance(Vector3 a, Vector3 b)
    {
        //--------------

        Vector3 vector = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);

        //--------------
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public static float DistanceToPlayer(Vector3 a)
    {
        //--------------

        Vector3 vector = new Vector3(a.x - playerPosition.x, a.y - playerPosition.y, a.z - playerPosition.z);
        return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}


