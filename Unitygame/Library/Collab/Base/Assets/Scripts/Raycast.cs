using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    [SerializeField]
    Image Loding;
    [SerializeField]
    LayerMask LayerMask;

    public float speed = 0.5f;

    private Vector3 ScreenConter;

    void Start()
    {
        ScreenConter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(ScreenConter);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 5f);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5f, LayerMask))
        {
            Loding.fillAmount += speed * Time.deltaTime;
            if (Loding.fillAmount == 1)
            {
                string Tag = hit.collider.tag;
                switch (Tag)
                {
                    case "Connect":

                        break;
                }
                Loding.fillAmount = 0;
            }
        }
        else
        {
            Loding.fillAmount = 0;
        }
    }
}
