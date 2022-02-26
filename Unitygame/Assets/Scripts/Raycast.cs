using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    public static Raycast _instance;
    [SerializeField]
    Image Loding;
    [SerializeField]
    LayerMask LayerMask;

    public float runge = 5;
    public float speed = 0.5f;
    public GameObject _gameObject;

    private Vector3 ScreenConter;

    public static Raycast Instance
    {
        get { return _instance; }
    }
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        ScreenConter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(ScreenConter);
        Debug.DrawRay(ray.origin, ray.direction * runge, Color.red, 5f);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, runge, LayerMask))
        {
            _gameObject = hit.collider.gameObject;
            //_gameObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.3f);

            Loding.fillAmount = 1;
            //Loding.fillAmount += speed * Time.deltaTime;
            //if (Loding.fillAmount == 1)
            //{
            //    string Tag = hit.collider.tag;            
            //    Loding.fillAmount = 0;
            //}
        }
        else
        {
            //if (_gameObject != null)
            //    _gameObject.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1f);
            _gameObject = null;
            Loding.fillAmount = 0;
        }
    }
}