using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followeobj : MonoBehaviour
{
    public GameObject player;
    // Update is called once per frame
    public bool rot;
    void Update()
    {
        this.transform.position = player.transform.position;
        if(rot)
            this.transform.rotation = player.transform.rotation;
    }
}
