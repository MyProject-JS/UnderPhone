using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eixt : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            if(GameManager.Instance.Clear == true)
            {
                GameManager.Instance.Button("Clear");
            }
    }
}
