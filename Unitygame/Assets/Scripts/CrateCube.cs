using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateCube : MonoBehaviour
{
    [SerializeField]
    Transform Point;
    public GameObject[] cubeList;
    public void Crate(sbyte Cube)
    {
        transform.GetComponent<AudioSource>().Play();
        Debug.Log(Cube);
        GameObject G = Instantiate(cubeList[Cube]);
        G.SetActive(true);
        G.transform.position = Point.position;
    }
}
