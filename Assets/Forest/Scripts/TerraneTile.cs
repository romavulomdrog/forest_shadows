using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TerraneTile : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public Mesh verticesMesh;
    public List<ObjectSpawn> objectSpawnList = new List<ObjectSpawn>();

    //--------------

    Transform tr;
    Vector3[] verticesPosition;
    Vector3 currentPosition;
    List<GameObject> objects = new List<GameObject>();
    float tileLocalScale;
    int numObjectsList;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Awake()
    {
        //--------------

        tr = transform;
        verticesPosition = verticesMesh.vertices;
        tileLocalScale = tr.localScale.x;
        numObjectsList = objectSpawnList.Count;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Start()
    {
        //--------------

        StartCoroutine(SpawnObjects());

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    IEnumerator SpawnObjects()
    {
        //--------------

        currentPosition = tr.position;

        for (int v = 0; v < verticesPosition.Length; v++)
        {
            float var0 = Mathf.Cos(GlVAR.worldSeed + currentPosition.x + verticesPosition[v].x + v);
            float var1 = Mathf.Cos(GlVAR.worldSeed + currentPosition.z + verticesPosition[v].z - v);
            float perlinNoise = Mathf.PerlinNoise(var0 * 100, var1 * 100);

            for (int os = 0; os < numObjectsList; os++)
            {
                if (objectSpawnList[os].enable == true)
                {
                    if (v % objectSpawnList[os].positionGeneration.z == 0 && perlinNoise > objectSpawnList[os].positionGeneration.x && perlinNoise < objectSpawnList[os].positionGeneration.y)
                    {
                        GameObject objectPool = SimplePool.GiveObj(objectSpawnList[os].element);

                        if (objectPool != null)
                        {
                            Transform objectPoolTransform = objectPool.transform;
                            objectPoolTransform.position = (verticesPosition[v] * tileLocalScale) + currentPosition + (new Vector3(var0, 0, var1) * objectSpawnList[os].positionGeneration.w);
                            Random.InitState(Mathf.RoundToInt(Mathf.Sin(var0 + var1 + verticesPosition[v].x + verticesPosition[v].z + v) * 10000));
                            float var2 = Random.value;
                            objectPoolTransform.localScale = Vector3.one * Mathf.Lerp(objectSpawnList[os].scale.x, objectSpawnList[os].scale.y, var2);
                            Random.InitState(Mathf.RoundToInt(Mathf.Sin(var1 + verticesPosition[v].x + v) * 10000));
                            float var3 = Mathf.Lerp(objectSpawnList[os].rotation.x, objectSpawnList[os].rotation.y, Random.value);
                            Random.InitState(Mathf.RoundToInt(Mathf.Sin(var0 + verticesPosition[v].z + v) * 10000));
                            float var4 = Mathf.Lerp(objectSpawnList[os].rotation.x, objectSpawnList[os].rotation.y, Random.value);
                            Random.InitState(Mathf.RoundToInt(Mathf.Sin(var2 + var0 + var1 + v) * 10000));
                            float var5 = Random.value * 720;
                            objectPoolTransform.rotation = Quaternion.Euler(var3, var5, var4);
                            objectPool.SetActive(true);
                            objects.Add(objectPool);
                        }
                    }
                }
            }

            yield return null;
        }

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void OnDestroy() { ObjInPull(); }
    void OnApplicationQuit() { ObjInPull(); }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void ObjInPull()
    {
        //--------------

        for (int i = 0; i < objects.Count; i++) if (objects[i] != null) SimplePool.Takeobj(objects[i]);
        objects.Clear();
        objectSpawnList.Clear();

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


[System.Serializable]

public class ObjectSpawn
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public bool enable;
    public int element;
    public Vector4 positionGeneration;
    public Vector2 scale;
    public Vector2 rotation;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}