using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    public GameObject finpanel;
    public int colichestvo = 6;
    public GameObject selectedObject; // ��������� ���� ������
    public GameObject selectedTrigger; // ��������� ���� �������

    void LateUpdate()
    {
        if (Mushroom.colGribov >= colichestvo && selectedObject.GetComponent<Collider>().bounds.Intersects(selectedTrigger.GetComponent<Collider>().bounds))
        {
            Debug.Log("�� �������, ��������� ��������");
            finpanel.SetActive(true);
            GameObject music = GameObject.Find("music");
            if (music != null)
            {
                music.SetActive(false);
            }
        }
    }
}
