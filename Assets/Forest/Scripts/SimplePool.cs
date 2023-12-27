﻿// Copyright (c) 2016 Unity Technologies. MIT license - license_unity.txt
// #NVJOB Simple Pool. MIT license - license_nvjob.txt
// #NVJOB Nicholas Veselov - https://nvjob.github.io
// #NVJOB Simple Pool v1.2.1 - https://nvjob.github.io/unity/nvjob-simple-pool


using UnityEngine;
using System.Collections.Generic;


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


public class SimplePool : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public List<ObjectsPool> ObjectsList = new List<ObjectsPool>();

    //--------------

    static Transform thisTransform;
    static int[] numberObjects;
    static GameObject[][] stObjects;
    static public int numObjectsList;
    static int[] curentObjects;  // Debug


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Awake()
    {
        //--------------

        thisTransform = transform;
        numObjectsList = ObjectsList.Count;
        AddObjectsToPool();

        //--------------
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void LateUpdate() 
    {
        //--------------

        for (int i = 0; i < numObjectsList; i++) ObjectsList[i].numberDebug = curentObjects[i];  // Debug

        //--------------
    }
       

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void AddObjectsToPool()
    {
        //--------------

        numberObjects = new int[numObjectsList];
        stObjects = new GameObject[numObjectsList][];
        curentObjects = new int[numObjectsList];  // Debug

        //--------------

        for (int num = 0; num < numObjectsList; num++)
        {
            numberObjects[num] = ObjectsList[num].numberObjects;
            stObjects[num] = new GameObject[numberObjects[num]];
            InstanInPool(ObjectsList[num].obj, stObjects[num]);
        }

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    static void InstanInPool(GameObject obj, GameObject[] objs)
    {
        //--------------

        for (int i = 0; i < objs.Length; i++)
        {
            objs[i] = Instantiate(obj);
            objs[i].SetActive(false);
            objs[i].transform.parent = thisTransform;
        }

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    static public GameObject GiveObj(int numElement)
    {
        //--------------

        for (int i = 0; i < numberObjects[numElement]; i++)
        {
            if (!stObjects[numElement][i].activeSelf)
            {
                curentObjects[numElement] += 1;  // Debug
                return stObjects[numElement][i];
            }
        }

        return null;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    static public void Takeobj(GameObject obj)
    {
        //--------------

        if (obj.activeSelf) obj.SetActive(false);
        if (obj.transform.parent != thisTransform) obj.transform.parent = thisTransform;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


[System.Serializable]

public class ObjectsPool
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public GameObject obj;
    public int numberObjects = 100;
    public int numberDebug; // Debug


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}