using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Monster_AI_Custom : MonoBehaviour {
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        


    public int countMin = 10;
    public int countMax = 10;
    public float size = 2;
    public Vector2 scale = new Vector2(0.7f, 1.5f);
    public int typeCustom = 1;

    Vector3[] points;
    List<Vector3> pointsList;
    WaitForSeconds delay_0 = new WaitForSeconds(0.75f);
    GameObject typeCustomGO;
    List<GameObject> myCustoms = new List<GameObject>();

    int randCount;




    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




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

        pointsList = new List<Vector3>();
        points = UniformPointsOnSphere(randCount, size);

        for (int i = 0; i < randCount; i++)
        {
            if (typeCustom <= 1 && MAiPool.getCustoms1() != null) typeCustomGO = MAiPool.getCustoms1();
            else if(typeCustom == 2 && MAiPool.getCustoms2() != null) typeCustomGO = MAiPool.getCustoms2();
            else if (typeCustom == 3 && MAiPool.getCustoms3() != null) typeCustomGO = MAiPool.getCustoms3();
            else if (typeCustom == 4 && MAiPool.getCustoms4() != null) typeCustomGO = MAiPool.getCustoms4();
            else if (typeCustom >= 5 && MAiPool.getCustoms5() != null) typeCustomGO = MAiPool.getCustoms5();

            if (typeCustomGO != null)
            {
                Transform trl = typeCustomGO.transform;
                trl.position = transform.position + points[i];
                trl.rotation = Random.rotation;
                trl.localScale = Vector3.one * Random.Range(scale.x, scale.y);
                trl.parent = transform;
                typeCustomGO.SetActive(true);
                myCustoms.Add(typeCustomGO);
            }

            yield return delay_0;
        }

        //=======================
    }

    



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////





    Vector3[] UniformPointsOnSphere(float N, float scale)
    {        
        float o = 2 / N;

        for (int k = 0; k < N; k++)
        {
            float y = k * o - 1 + (o / 2);
            float r = Mathf.Sqrt(1 - y * y);
            float phi = k * Mathf.PI * (3 - Mathf.Sqrt(5));
            pointsList.Add(new Vector3(Mathf.Cos(phi) * r, y, Mathf.Sin(phi) * r) * scale);
        }
        return pointsList.ToArray();
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
