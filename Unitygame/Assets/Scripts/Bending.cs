using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bending : MonoBehaviour
{
    bool A= true;
    [SerializeField]
    Transform[] Point;

    public List<GameObject> itemlist;
    //sbyte Index = 0;
    Rigidbody _Rigidbody;

    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Cube1":
            case "Cube2":
            case "Cube3":
       
                itemlist.Add(other.gameObject);
                A = true;
                break;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!Test_Controle.Instance.B1 && A)
        {
            for(int i = 0; i < itemlist.Count; i++)
            {
                itemlist[i].transform.position = Point[i].position;
                A = false;
            }
        }
        if (Prss.Prass && itemlist != null)
        {
            bool a, b, c;
            a = b = c = false;
            for (int i = 0; i < itemlist.Count; i++)
            {
                Debug.Log(itemlist[i].tag);
                switch (itemlist[i].tag)
                {
                    case "Cube1":
                        a = true;
                        break;
                    case "Cube2":
                        b = true;
                        break;
                    case "Cube3":
                        c = true;
                        break;
                }
                Destroy(itemlist[i]);
            }
            if (a && b && c)
            {
                transform.GetComponent<CrateCube>().Crate(0);
            }
            itemlist = new List<GameObject>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        itemlist.Remove(other.gameObject);
    }
}
