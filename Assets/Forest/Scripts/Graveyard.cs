using System.Collections;
using UnityEngine;

public class Graveyard : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

    public TextMesh curName;
    public TextMesh years;
    public bool transfer;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void OnEnable()
    {
        //--------------

        string name = NVJOBNameGen.GiveAName(Random.Range(1, 5), transfer);
        curName.text = NVJOBNameGen.Uppercase(name);

        //--------------

        int var0 = Random.Range(1800, 1920);
        int var1 = var0 + Random.Range(13, 35);
        string var2 = var0.ToString() + "-" + var1.ToString();
        years.text = var2;

        //--------------
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
