using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{

    public AudioClip sobral;
    public GameObject particli;
    public GameObject mesh;
    AudioSource audioSource;
    bool triger;

    public static int colGribov;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        mesh.SetActive(true);
        triger = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (triger == false)
            {
                triger = true;
                audioSource.PlayOneShot(sobral, 0.7F);
                particli.SetActive(true);
                mesh.SetActive(false);
                Invoke("inpool", 4.0f);
                colGribov += 1;
                Debug.Log("Собрал гриб");
            }
        }
    }


    void inpool()
    {
        particli.SetActive(false);
        SimplePool.Takeobj(gameObject);
    }



}
