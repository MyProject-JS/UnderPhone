using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskOBJ : MonoBehaviour
{
    ColorBackGround puzzle1;
    GameObject item;
    AudioSource audio;

    [SerializeField]
    Transform OutPoint;
    void Start()
    {
        puzzle1 = GameObject.FindWithTag("BackGround").GetComponent<ColorBackGround>();
        audio = transform.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "SOMA")
        {
            GameManager.Instance.blackout = true;
            if (item != null && item.tag != other.tag)
            {
                audio.Play();
                item.transform.position = OutPoint.position;
                item = other.gameObject;
                GameManager.Instance.blackout = false;
            }
            else
            {
                item = other.gameObject;
            }
        }     
        if (other.tag == "puzzle1")
        {

            if (item != null && item.tag != other.tag)
            {
                audio.Play();
                item.transform.position = OutPoint.position;
                item = other.gameObject;
            }
            else
            {
                item = other.gameObject;
            }
        }
    }
    void OnTriggerStay(Collider other)
    { 
        if (other.tag == "SOMA")
        {
            Debug.Log(other.tag);
            GameManager.Instance.Rotate(other.gameObject);
        }
        if (other.tag == "puzzle1")
        {
            puzzle1.RotateColor();
        }
    }
}
