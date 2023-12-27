using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Monster_AI_Custom_2 : MonoBehaviour {
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        


    public int countMin = 10;
    public int countMax = 10;
    public Vector2 size = new Vector2(1, 1);
    public Vector2 randScale = new Vector2(0.7f, 1.5f);
    public int rotMin = 10;
    public int rotMax = 10;
    public int typeCustom = 1;


    WaitForSeconds delay_0 = new WaitForSeconds(0.75f);
    List<GameObject> myCustoms = new List<GameObject>();
    int randCount;
    Transform tr;
    GameObject typeCustomGO;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    private void Awake()
    {
        tr = transform;
    }


    //=======================

    void OnEnable()
    {
        StartCoroutine("Gen_Custm");
    }


    //=======================


    void OnDisable()
    {
        StopCoroutine("Gen_Custm");
        Invoke("ObjInPull", 0.1f);
    }
    



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        


    IEnumerator Gen_Custm()
    {
        //=======================
                
        yield return delay_0;

        randCount = Random.Range(countMin, countMax);
        
        for (int z = 0; z < randCount; z++)
        {
            for (int x = 0; x < randCount; x++)
            {
                if (typeCustom <= 1 && MAiPool.getCustoms1() != null) typeCustomGO = MAiPool.getCustoms1();
                else if (typeCustom == 2 && MAiPool.getCustoms2() != null) typeCustomGO = MAiPool.getCustoms2();
                else if (typeCustom == 3 && MAiPool.getCustoms3() != null) typeCustomGO = MAiPool.getCustoms3();
                else if (typeCustom == 4 && MAiPool.getCustoms4() != null) typeCustomGO = MAiPool.getCustoms4();
                else if (typeCustom >= 5 && MAiPool.getCustoms5() != null) typeCustomGO = MAiPool.getCustoms5();

                Transform trl = typeCustomGO.transform;
                trl.parent = tr;
                trl.localScale = Vector3.one * Random.Range(randScale.x, randScale.y);
                trl.rotation = tr.rotation * Quaternion.Euler(Random.Range(rotMin, rotMax), Random.Range(-360, 360), Random.Range(rotMin, rotMax));
                trl.localPosition = new Vector3(Random.Range(-size.x * 0.5f, size.x * 0.5f), 0, Random.Range(-size.y * 0.5f, size.y * 0.5f));
                myCustoms.Add(typeCustomGO);
                typeCustomGO.SetActive(true);

                yield return delay_0;
            }
        }


        //=======================
    }





    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




    void OnDestroy() { ObjInPull(); }

    void OnApplicationQuit() { ObjInPull(); }

    


    void ObjInPull()
    {
        //------

        for (int i = 0; i < myCustoms.Count; i++)
        {
            if (myCustoms[i] != null) MAiPool.Destroy(myCustoms[i]);
        }

        myCustoms.Clear();

        //------
    }








    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
