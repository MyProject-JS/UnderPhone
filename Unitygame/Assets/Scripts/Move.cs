using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float speed = 0.4f;

    GameObject cam;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
    }

    void Update()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            rb.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed));
        }
        else
        {
            Vector2 pos = AngltoVector(Test_Controle.Instance.Angle);
            float GetS = Test_Controle.Instance.strong * 0.01f * speed;
            rb.transform.Translate(new Vector3(pos.x * GetS, 0, pos.y * GetS));
        }
        rb.transform.eulerAngles = new Vector3(0, cam.transform.rotation.eulerAngles.y, 0);
    }

    Vector2 AngltoVector(float _deg)
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}
