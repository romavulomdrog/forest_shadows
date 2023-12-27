using UnityEngine;



public class MAiPool : MonoBehaviour {
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    



    static int numCustom1 = 150;
    static int numCustom2 = 150;
    static int numCustom3 = 150;
    static int numCustom4 = 150;
    static int numCustom5 = 150;
    

    static GameObject[] customs1;
    static GameObject[] customs2;
    static GameObject[] customs3;
    static GameObject[] customs4;
    static GameObject[] customs5;
    

    //-----------

    public GameObject goCustoms1;
    public GameObject goCustoms2;
    public GameObject goCustoms3;
    public GameObject goCustoms4;
    public GameObject goCustoms5;

    //-----------


    static Transform trMAi;




    //********************************************************



    void Awake()
    {
        //------

        trMAi = transform;

        //------

        if (goCustoms1 != null)
        {
            customs1 = new GameObject[numCustom1];
            InstanInPoolMAi(goCustoms1, customs1);
        }
        //------

        if (goCustoms2 != null)
        {
            customs2 = new GameObject[numCustom2];
            InstanInPoolMAi(goCustoms2, customs2);
        }

        //------

        if (goCustoms3 != null)
        {
            customs3 = new GameObject[numCustom3];
            InstanInPoolMAi(goCustoms3, customs3);
        }

        //------

        if (goCustoms4 != null)
        {
            customs4 = new GameObject[numCustom4];
            InstanInPoolMAi(goCustoms4, customs4);
        }

        //------


        if (goCustoms5 != null)
        {
            customs5 = new GameObject[numCustom5];
            InstanInPoolMAi(goCustoms5, customs5);
        }

        //------
    }




    //********************************************************



    static public GameObject getCustoms1()
    {
        //-----------

        for (int i = 0; i < numCustom1; i++) if (!customs1[i].activeSelf) return customs1[i];
        return null;

        //-----------
    }




    //*****************



    static public GameObject getCustoms2()
    {
        //-----------

        for (int i = 0; i < numCustom2; i++) if (!customs2[i].activeSelf) return customs2[i];
        return null;

        //-----------
    }




    //*****************



    static public GameObject getCustoms3()
    {
        //-----------

        for (int i = 0; i < numCustom3; i++) if (!customs3[i].activeSelf) return customs3[i];
        return null;

        //-----------
    }




    //*****************



    static public GameObject getCustoms4()
    {
        //-----------

        for (int i = 0; i < numCustom4; i++) if (!customs4[i].activeSelf) return customs4[i];
        return null;

        //-----------
    }




    //*****************



    static public GameObject getCustoms5()
    {
        //-----------

        for (int i = 0; i < numCustom5; i++) if (!customs5[i].activeSelf) return customs5[i];
        return null;

        //-----------
    }

    

    //********************************************************



    static private void InstanInPoolMAi(GameObject obj, GameObject[] objs)
    {
        //------

        for (int i = 0; i < objs.Length; i++)
        {
            objs[i] = (GameObject)Instantiate(obj, Vector3.zero, Quaternion.identity);
            objs[i].SetActive(false);
            objs[i].transform.parent = trMAi;
        }

        //------
    }





    //********************************************************



    static public void Destroy(GameObject objMDestroy)
    {
        //-----------

        objMDestroy.SetActive(false);
        objMDestroy.transform.parent = trMAi;

        //-----------
    }













    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
