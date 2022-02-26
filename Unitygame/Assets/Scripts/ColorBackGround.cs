using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBackGround : MonoBehaviour
{
    public bool Red = false, Green = false, Blue = false ;
    bool _R = false, _G = false, _B = false;
    public float Light = 0.5f;
    [SerializeField]
    GameObject Puzzle;
    // Start is called before the first frame update
    void Update()
    {
        if (GameManager.Instance.blackout)
        {
            transform.GetComponent<Renderer>().material.SetFloat("_R", 0);
            transform.GetComponent<Renderer>().material.SetFloat("_G", 0);
            transform.GetComponent<Renderer>().material.SetFloat("_B", 0);
            return;
        }
    }

    public sbyte Save()
    {
        sbyte C = 0;
        if (_B)
        {
            Blue = true;
            C = 1;
        }
        if (_G) 
        {
            Green = true;
            C = 2;
        }
        if (_R)
        { 
            Red = true;
            C = 5;
        }
        return C;
    }
    public void Reset()
    {
        Red = Blue = Green = false;
        Puzzle.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void RotateColor()
    {
        Vector3 Ros = Puzzle.transform.rotation.eulerAngles;
        float R, G, B;
        float r = 0, g = 0, b = 0;

        B = Ros.x % 180 * 0.0055555555555556f;
        G = Ros.y % 180 * 0.0055555555555556f;
        R = Ros.z % 180 * 0.0055555555555556f;

        //Debug.Log("R: "+R + "G: " + G + "B: " + B);
        if (B < 0.6 && B > 0.4)
        {
            _B = true;
            b = 1.5f;
            g = 0;
            r = 0;
        }
        else if (R < 0.6 && R > 0.4 && B<0.2 && G<0.2)
        {
            _R = true;
            r = 1.5f;
            b = 0;
            g = 0;
        }
        else if (B<0.3&&R<0.3)
        {
            if (G < 0.4 && G > 0.2 || G < 0.9 && G > 0.6)
            {
                _G = true;
                g = 1.5f;
                b = 0;
                r = 0;
            }
        }
        else
        {
            r = R;
            g = G;
            b = B;
            _R = _G = _B = false;
        }

        if (Red) r = 1.5f;
        if (Green) g = 1.5f;
        if (Blue) b = 1.5f;

        r += Light;
        g += Light;
        b += Light;
  
        transform.GetComponent<Renderer>().material.SetFloat("_R", r);
        transform.GetComponent<Renderer>().material.SetFloat("_G", g);
        transform.GetComponent<Renderer>().material.SetFloat("_B", b);
    }
}
