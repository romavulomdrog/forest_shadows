using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBillboard : MonoBehaviour
{

    public GameObject objectToRender;
    public Camera cam;
    public int imageSize = 512;

    void Start()
    {
        StartCoroutine(StartBillboard());
    }


    IEnumerator StartBillboard()
    {
        for (int q = 0; q < 2; q++)
        {
            if (q > 0) objectToRender.transform.rotation = Quaternion.Euler(0, -90, 0);


            cam.orthographic = true;
            float rw = imageSize;
            rw /= Screen.width;
            float rh = imageSize;
            rh /= Screen.height;
            cam.rect = new Rect(0, 0, rw, rh);
            Bounds bb = objectToRender.GetComponent<Renderer>().bounds;

            cam.transform.position = bb.center;
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.z - (1.0f + (bb.min.z * 2.0f)), cam.transform.position.y);
            cam.nearClipPlane = (cam.transform.position.z + 10.0f + bb.max.z) * -1;
            cam.farClipPlane = -cam.transform.position.z + 10.0f + bb.max.z;
            cam.orthographicSize = 1.01f * Mathf.Max((bb.max.y - bb.min.y) / 2.0f, (bb.max.x - bb.min.x) / 2.0f);
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.z, cam.transform.position.y + (cam.orthographicSize * 0.05f));

            yield return new WaitForEndOfFrame();
            var tex = new Texture2D(imageSize, imageSize, TextureFormat.ARGB32, false);
            tex.ReadPixels(new Rect(0, 0, imageSize, imageSize), 0, 0);
            tex.Apply();
            Color bCol = cam.backgroundColor;
            var alpha = bCol;
            alpha.a = 0.0f;
            for (int y = 0; y < imageSize; y++)
            {
                for (int x = 0; x < imageSize; x++)
                {
                    Color c = tex.GetPixel(x, y);

                    if (c.r == bCol.r || c.a != 1)
                        tex.SetPixel(x, y, alpha);
                }
            }
            tex.Apply();
            var bytes = tex.EncodeToPNG();
            Destroy(tex);
            System.IO.File.WriteAllBytes(Application.dataPath + "/../billboard/billboard_" + q.ToString() + ".png", bytes);
            Debug.Log("tex saved!");
        }
    }
}
