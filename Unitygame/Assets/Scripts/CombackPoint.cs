using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombackPoint : MonoBehaviour
{
    [SerializeField]
    Transform Point;
    [SerializeField]
    string TAG = "";
    Rigidbody _Rigidbody;

    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == TAG)
            if (!Test_Controle.Instance.B1)
            {
                transform.position = Point.position;

            }
        _Rigidbody.isKinematic = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _Rigidbody.isKinematic = false;
    }
}
