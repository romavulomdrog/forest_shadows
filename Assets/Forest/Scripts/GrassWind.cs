// Copyright (c) 2016 Unity Technologies. MIT license - license_unity.txt
// Copyright (c) 2016 #NVJOB.
// Copyright (c) 2016 NVGen.
// #NVJOB Nicholas Veselov - https://nvjob.github.io


using UnityEngine;


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


public class GrassWind : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public float globalSpeed = 1;
    public float windSpeed = 1;
    public float windDirectionSpeed = 10;
    public float windPower = 80;
    public float windVibration = 15;
    public float windWorld = 250;
    public float windWorldSpeed = 1;
    public float windWorldCh = 1;
    public float grassWindFlexibility = 2;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void LateUpdate()
    {
        //--------------

        float timeTime = 100 + Mathf.PingPong(Time.time * globalSpeed, 200);
        float gwpMP = windPower + (Mathf.Sin(timeTime) * windVibration);
        Shader.SetGlobalFloat("_GlobalWindPower", gwpMP);
        float gwpsin = Mathf.Cos(timeTime * windWorldSpeed) * windWorldCh;
        Shader.SetGlobalFloat("_GlobalWindWorld", windWorld + gwpsin);               
        Vector2 gwVector = Quaternion.AngleAxis(timeTime * windSpeed, Vector3.forward) * Vector2.one * windDirectionSpeed;
        Shader.SetGlobalFloat("_GlobalWindDirX", gwVector.x);
        Shader.SetGlobalFloat("_GlobalWindDirZ", gwVector.y);
        Shader.SetGlobalFloat("_GlobalWindGrassFlex", 0.1f * grassWindFlexibility);

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}