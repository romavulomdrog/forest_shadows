using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoadTile : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public int roadSignDistance;
    public List<CarSelection> carSelection = new List<CarSelection>();


    //--------------

    Transform tr;
    GameObject roadSignDistanceObject, carObject;
    int numObjectsList;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Awake()
    {
        //--------------

        tr = transform;
        numObjectsList = carSelection.Count;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void OnEnable()
    {
        //--------------

        RoadSignDistance();
        CarCurent();

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void RoadSignDistance()
    {
        //--------------

        if (Mathf.Abs(tr.position.z) > 10 && tr.position.z % 300 == 0)
        {
            roadSignDistanceObject = SimplePool.GiveObj(roadSignDistance);
            if (roadSignDistanceObject != null)
            {
                roadSignDistanceObject.transform.position = new Vector3(28, 0, tr.position.z);
                roadSignDistanceObject.SetActive(true);
            }
        }

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

    void CarCurent()
    {
        //--------------

        if (Mathf.Abs(tr.position.z) > 200)
        {
            for (int cs = 0; cs < numObjectsList; cs++)
            {
                Random.InitState(Mathf.RoundToInt(Mathf.Cos(GlVAR.worldSeed + tr.position.x + tr.position.z) * 10000));
                float var0 = Random.value;

                if (var0 > carSelection[cs].chance.x && var0 < carSelection[cs].chance.y)
                {
                    carObject = SimplePool.GiveObj(carSelection[cs].element);

                    if (carObject != null)
                    {
                        Transform carObjectTransform = carObject.transform;

                        Random.InitState(Mathf.RoundToInt(Mathf.Sin(GlVAR.worldSeed + tr.position.x + tr.position.z) * 10000));
                        float var1 = Random.Range(carSelection[cs].offset.x, carSelection[cs].offset.y);
                        carObjectTransform.position = new Vector3(tr.position.x + var1, 0, tr.position.z);

                        Random.InitState(Mathf.RoundToInt(Mathf.Sin(GlVAR.worldSeed + tr.position.x + tr.position.z + var1 + carObjectTransform.position.x + carObjectTransform.position.z) * 10000));
                        float var2 = Random.Range(-360, 360);
                        carObjectTransform.rotation = Quaternion.Euler(0, var2, 0);

                        carObject.SetActive(true);
                    }
                }
            }
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

        if (roadSignDistanceObject != null) SimplePool.Takeobj(roadSignDistanceObject);
        if (carObject != null) SimplePool.Takeobj(carObject);

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


[System.Serializable]

public class CarSelection
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public int element;
    public Vector2 chance;
    public Vector2 offset;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}