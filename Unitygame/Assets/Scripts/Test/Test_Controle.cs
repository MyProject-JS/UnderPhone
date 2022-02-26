using System.Collections;
using System.Collections.Generic;
using UnityAndroidBluetooth;
using UnityEngine;
using UnityEngine.UI;

public class Test_Controle : MonoBehaviour
{
    private static Test_Controle _instance;
    public Vector3 _pos;
    public int Angle, strong;
    public bool B1 = false, isClick = false, isPress = true;
    public float x, y, z;

    string Vlue;
    GameObject item;
    GameManager _gameManager;
    [SerializeField]
    AudioSource audio;
    [SerializeField]
    Text text;
    [SerializeField]
    GameObject _transform;

    public static Test_Controle Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        if (Application.platform != RuntimePlatform.Android) return;
        VRChaepBluetooth.Instance.StartServer();
    }
    private void Start()
    {

        _gameManager = GameObject.FindWithTag("MGR").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            B1 = Input.GetMouseButton(0);
            x = y = z = 1;
        }
        else
        {
            Angle = VRChaepBluetooth.Instance.j_A;
            strong = VRChaepBluetooth.Instance.j_S;

            B1 = VRChaepBluetooth.Instance.B1;

            x = VRChaepBluetooth.Instance.x;
            y = VRChaepBluetooth.Instance.y;
            z = VRChaepBluetooth.Instance.z;

            Vlue = string.Format("JA{0}:JS{1},{2},Z:{3},{4},{5}", Angle, strong, B1, x, y, z);
            text.text = Vlue;
        }
        if (item != null)
        {
            if (B1 && isPress)
            {
                _Interaction(item.gameObject.tag);
            }
            else
            {
                Reset(item.gameObject.tag);
            }
        }
        else
        {
            item = Raycast.Instance._gameObject;
        }

    }

    void _Interaction(string tag)
    {
        
      
        switch (tag)
        {
            case "Exit":
            case "StartButton":
                if (isClick) return;
                _gameManager.Button(tag);
                isClick = true;
                break;
            case "Prass":
            case "SaveButton":
                if (isClick) return;
                item.GetComponent<AudioSource>().Play();
                item.GetComponent<Animator>().SetTrigger("Down");
                _gameManager.Button(tag);
                isClick = true;
                break;
            case "Cabinet":
                if (isClick) return;
                item.GetComponent<AudioSource>().Play();
                bool Open = item.GetComponent<Animator>().GetBool("Open");
                item.GetComponent<Animator>().SetBool("Open", !Open);
                isClick = true;
                break;
            case "SOMA":
            case "puzzle1":
                if (!isClick)
                {
                    audio.Play();
                    _transform.transform.position = item.transform.position;
                    item.transform.parent = _transform.transform;
                    isClick = true;
                }
                item.transform.position = _transform.transform.position;
                _transform.transform.Rotate(y, -z, -x);
                break;
            default:
                if (!isClick) audio.Play();
                    item.transform.parent = _transform.transform;
                item.transform.position = _transform.transform.position;
                _transform.transform.Rotate(y, -z, -x);
                isClick = true;
                break;
        }
    }
    void Reset(string tag)
    {
        isPress = true;
        isClick = false;
        //Debug.Log(tag);
        switch (tag)
        {
            case "Connect":
            case "Exit":
            case "StartButton":
            case "SaveButton":
            case "Prass":
            case "Cabinet":
                break;
            case "SOMA":
            case "puzzle1":
            default:
                item.transform.parent = null;
                break;
        }
        _transform.transform.localPosition = _pos;
        _transform.transform.rotation = Quaternion.Euler(0, 0, 0);
        item = null;
    }
}

