using UnityEngine;


public class CarColor : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public Material[] colorMaterial;
    public int numberMaterial;

    //--------------

    Transform tr;
    Renderer carRenderer;
    Material[] allMaterials;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Awake()
    {
        //--------------

        tr = transform;
        carRenderer = GetComponent<Renderer>();
        allMaterials = carRenderer.materials;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void OnEnable()
    {
        //--------------

        Random.InitState(Mathf.RoundToInt(Mathf.Cos(GlVAR.worldSeed * (tr.position.z + tr.position.x + tr.rotation.x + tr.rotation.y + tr.rotation.z)) * 10000));
        int var0 = Random.Range(0, colorMaterial.Length);
        allMaterials[numberMaterial] = colorMaterial[var0];
        carRenderer.materials = allMaterials;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
