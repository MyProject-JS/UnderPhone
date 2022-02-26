using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    sbyte ButtonCount, Value = 0;
    CrateCube _CrateCube;
    ColorBackGround puzzle1;

    public bool blackout = false , Clear= false, Connect = false;
    public Animator door;

    [SerializeField]
    int Time = 10;
    [SerializeField]
    Transform start, play, end;
    [SerializeField]
    GameObject HINT;
    [SerializeField]
    GameObject Light;
    [SerializeField]
    GameObject[] G;
    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        _CrateCube = GameObject.FindWithTag("Q2").GetComponent<CrateCube>();
        puzzle1 = GameObject.FindWithTag("BackGround").GetComponent<ColorBackGround>();
        GameObject.FindWithTag("Player").transform.position = start.position;
    }
    void Update()
    {
        if (Connect) G[0].SetActive(false);
        else G[0].SetActive(true);
    }
    public void Button(string A)
    {
        switch (A)
        {
            case "SaveButton":
                SaveButton();
                break;
            case "StartButton":
                GameObject.FindWithTag("Player").transform.position = play.position;
                G[1].SetActive(false);
                start.transform.gameObject.SetActive(false);
                break;
            case "Exit":
                Application.Quit();
                break;
            case "Clear":
                end.transform.gameObject.SetActive(true);
                G[1].SetActive(true);
                G[2].SetActive(false);
                G[3].SetActive(false);
                GameObject.FindWithTag("Player").transform.position = end.position;
                break;
        }
    }
    private void Reset()
    {
        blackout = false;
        Light.SetActive(true);
        HINT.SetActive(false);
        ButtonCount = 0;
        Value = 0;
        puzzle1.Reset();
    }
    public void Rotate(GameObject Puzzle)
    {
        Vector3 Ros = Puzzle.transform.rotation.eulerAngles;
        float R, G, B;

        B = Ros.x % 90;
        G = Ros.y % 90;
        R = Ros.z % 90;
        if (B > 40 && B < 50 && R > 40 && R < 50 && G > 40 && G < 50 && !Clear)
        {
            door.transform.gameObject.GetComponent<AudioSource>().Play();
            door.SetTrigger("Open");
            Clear = true;
        }

        Debug.Log("R: "+R + "G: " + G + "B: " + B);
    }
    void SaveButton()
    {
        sbyte _Color = puzzle1.Save();
        ButtonCount++;
        Value += _Color;
        Debug.Log(ButtonCount);
        switch (Value)
        {
            case 5:
                if (ButtonCount < 2)
                    _CrateCube.Crate(0);
                break;
            case 6:
                //P
                _CrateCube.Crate(1);
                break;
            case 7:
                //Y
                _CrateCube.Crate(2);
                break;
        }
        if (ButtonCount > 1)
        {
            StartCoroutine("BlackOut");
            return;
        }
    }
    IEnumerator BlackOut()
    {
        HINT.SetActive(true);
        Light.SetActive(false);
        blackout = true;
        for (int i=0; i < Time; i++)
        {

            yield return new WaitForSeconds(1f);
        }
        Reset();
    }
}
