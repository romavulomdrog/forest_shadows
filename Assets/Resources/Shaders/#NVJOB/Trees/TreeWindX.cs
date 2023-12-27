// Copyright (c) 2016 Unity Technologies. MIT license - license_unity.txt
// Copyright (c) 2016 #NVJOB.
// Copyright (c) 2016 NVGen.
// #NVJOB Nicholas Veselov - https://nvjob.github.io


using UnityEngine;


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


public class TreeWindX : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public float globalSpeed = 1;
    public float windSpeed = 0.13f;
    public float windDirectionSpeed = 10;
    public float worldScale = 55;
    public float chWorldScale = 0.3f;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void LateUpdate()
    {
        //--------------

        float timeTime = 100 + Mathf.PingPong(Time.time * globalSpeed, 1000);
        Vector2 gwVector = Quaternion.AngleAxis(timeTime * windSpeed, Vector3.forward) * Vector2.one * windDirectionSpeed;
        Shader.SetGlobalFloat("_TreeWindDirX", gwVector.x);
        Shader.SetGlobalFloat("_TreeWindDirZ", gwVector.y);
        float df = Mathf.PingPong(timeTime * chWorldScale, worldScale * chWorldScale);
        Shader.SetGlobalFloat("_TreeWindWorld", worldScale + df);

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
