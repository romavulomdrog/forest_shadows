using UnityEngine;


public class RoadSignDistance : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public TextMesh nameText;
    public TextMesh distanceText;

    //--------------

    Transform tr;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Awake()
    {
        //--------------

        tr = transform;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void OnEnable()
    {
        //--------------

        if (tr.position.z < 0)
        {
            nameText.text = "Inception";
            distanceText.text = Mathf.RoundToInt(0 - tr.position.z / 3).ToString();
        }
        else
        {
            nameText.text = "Exodus";
            distanceText.text = Mathf.RoundToInt(tr.position.z / 3).ToString();
        }

        Random.InitState(Mathf.RoundToInt(Mathf.Sin(GlVAR.worldSeed + tr.position.z + tr.position.x) * 10000));
        tr.rotation = Quaternion.Euler(Random.Range(-5.0f, 5.0f), Random.Range(-160.0f, -180.0f), Random.Range(-5.0f, 5.0f));

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
