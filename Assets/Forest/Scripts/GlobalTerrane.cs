using UnityEngine;
using System.Collections;


public class GlobalTerrane : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public int seed = 78945;
    [Space]
    public int tileSize = 60;
    public int tileAmount = 9;
    [Space]
    public GameObject roadTile;
    public GameObject forestTile;
    public GameObject gladeTile;
    public float gladeChance;

    //--------------

    Hashtable tileHash, newTileHash;
    Transform tr;
    Vector3 startPos, plPos;
    Vector2 offs;
    GameObject tObj;
    float updateTime;
    int xMove, zMove, playerX, playerZ;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Awake()
    {
        //--------------

        tileHash = new Hashtable();
        newTileHash = new Hashtable();

        tr = transform;
        startPos = Vector3.one * 999999999;

        GlVAR.worldSeed = 50000 + Mathf.FloorToInt(Mathf.PerlinNoise(Mathf.Cos(seed) * 951, Mathf.Sin(seed) * 1111) * 150000);

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Start()
    {
        //--------------

        updateTime = Time.realtimeSinceStartup;
        StartCoroutine(UpdateTileGen());

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    IEnumerator UpdateTileGen()
    {
        //--------------

        while (true)
        {
            plPos = GlVAR.playerPosition;
            xMove = Mathf.Abs((int)(plPos.x - startPos.x));
            zMove = Mathf.Abs((int)(plPos.z - startPos.z));

            if (xMove >= tileSize || zMove >= tileSize)
            {
                updateTime = Time.realtimeSinceStartup;

                playerX = (int)(Mathf.Floor(plPos.x / tileSize) * tileSize);
                playerZ = (int)(Mathf.Floor(plPos.z / tileSize) * tileSize);

                for (int x = -tileAmount + 1; x < tileAmount + 1; x++)
                {
                    yield return null;

                    for (int z = -tileAmount + 1; z < tileAmount + 1; z++)
                    {
                        Vector3 pos = new Vector3(x * tileSize + playerX, 0, z * tileSize + playerZ);
                        string tilename = "Tile_" + ((int)pos.x).ToString() + "_" + ((int)pos.z).ToString();
                        if (!tileHash.ContainsKey(tilename))
                        {
                            if (pos.x > 2 || pos.x < -2)
                            {
                                float var0 = Mathf.Cos(GlVAR.worldSeed + pos.x);
                                float var1 = Mathf.Cos(GlVAR.worldSeed + pos.z);
                                float perlinNoise = Mathf.PerlinNoise(var0 * 100, var1 * 100);
                                if (perlinNoise > gladeChance) tObj = Instantiate(forestTile, pos, Quaternion.identity);
                                else tObj = Instantiate(gladeTile, pos, Quaternion.identity);
                            }
                            else tObj = Instantiate(roadTile, pos, Quaternion.identity);
                            tObj.transform.parent = tr;
                            tObj.name = tilename;
                            Tile tile = new Tile(tObj, updateTime);
                            tileHash.Add(tilename, tile);
                        }
                        else (tileHash[tilename] as Tile).creationTime = updateTime;
                    }
                }

                newTileHash = new Hashtable();
                foreach (Tile tls in tileHash.Values)
                {
                    if (tls.creationTime != updateTime) Destroy(tls.theTile);
                    else newTileHash.Add(tls.theTile.name, tls);
                }
                tileHash = newTileHash;
                startPos = GlVAR.playerPosition;
            }

            yield return null;
        }

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


class Tile
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public GameObject theTile;
    public float creationTime;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public Tile(GameObject tObj, float cTime)
    {
        //--------------

        theTile = tObj;
        creationTime = cTime;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}