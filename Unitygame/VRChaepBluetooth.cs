using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityAndroidBluetooth
{
    public class VRChaepBluetooth : MonoBehaviour
    {
        private static VRChaepBluetooth _instance;
        private BluetoothServer server;
        //[SerializeField]
        //List<Button> buttons;

        public int j_A, j_S;
        public bool B1;
        public float x, y, z;

        public static VRChaepBluetooth Instance
        {
            get { return _instance; }
        }
        public static BluetoothServer Server
        {
            get { return _instance.server; }
        }

        public void StartServer()
        {
            server.Start();
        }

        public void StopServer()
        {
            server.Stop();
        }
        void Awake()
        {
            if (Application.platform != RuntimePlatform.Android) return;

            if (_instance != null && _instance != this)
            {
                DestroyImmediate(this.gameObject);
            }
            else
            {
                _instance = this;
                _instance.server = new BluetoothServer();
                DontDestroyOnLoad(this.gameObject);
            }
        }
        void Start()
        {
            Server.MessageReceived += MessageReceivedHandler;
        }
        void MessageReceivedHandler(object sender, MessageReceivedEventArgs e)
        {
            Debug.Log(e.Message);
            //J2:23:23      //조이 스틱
            //B:1:           //버튼
            //T:1:0          //트리거
            //Z:0:0:0            //자이로 센서
            //S:vlue         // sounde;

            string[] _message = e.Message.Split(',');

            for (int i = 0; i<_message.Length; i++)
            {
                string[] message = _message[i].Split(':');
                switch (message[0])
                {
                    case "J":
                        j_A = int.Parse(message[1]);
                        j_S = int.Parse(message[2]);
                        break;
                    case "B1":
                        if (message[1] == "1")
                        {
                            B1 = true;
                        }
                        else
                            B1 = false;
                        break;
                    case "Z":
                        x = float.Parse(message[1]);
                        y = float.Parse(message[2]);
                        z = float.Parse(message[3]);
                        break;
                }
            }

            #region ButtonClass
            //foreach (Button btn in buttons)
            //{
            //    if (message[0] == btn.SymbolicName)
            //    {
            //        if (message[1] == "1")
            //        {
            //            btn.IsClicked = true;
            //            btn.IsPressed = true;
            //            StartCoroutine(ToggleClick(btn));
            //            break;
            //        }
            //        else
            //        {
            //            btn.IsPressed = false;
            //        }
            //        if (btn is Joystick)
            //        {
            //            Joystick joystick = btn as Joystick;
            //            joystick.DeltaX = double.Parse(message[2]);
            //            joystick.DeltaY = double.Parse(message[3]);
            //        }
            //        else if (btn is Trigger)
            //        {
            //            Trigger trigger = btn as Trigger;
            //            trigger.Value = double.Parse(message[2]);
            //        }
            //    }
            //}
            //IEnumerator ToggleClick(Button btn)
            //{
            //    if (btn.IsClicked)
            //    {
            //        yield return null;
            //        btn.IsClicked = false;
            //    }
            //}
            #endregion
        }
    }

    #region ButtonClass
    //public class Button
    //{
    //    public string Name { get; set; }
    //    public string SymbolicName { get; set; }
    //    public bool IsPressed { get; set; }
    //    public bool IsClicked { get; set; }

    //    public Button(string name, string symbolicName)
    //    {
    //        Name = name;
    //        SymbolicName = symbolicName;
    //        IsPressed = false;
    //        IsClicked = false;
    //    }
    //    public Button(string name) : this(name, name) { }
    //}
    //public class Joystick : Button
    //{
    //    public Joystick(string name) : this(name, name) { }
    //    public Joystick(string name, string symbolicName) : base(name, symbolicName)
    //    {
    //        DeltaX = 0;
    //        DeltaY = 0;
    //    }
    //    public double DeltaX { get; set; }
    //    public double DeltaY { get; set; }
    //}

    //public class Trigger : Button
    //{
    //    public Trigger(string name) : this(name, name) { }
    //    public Trigger(string name, string symbolicName) : base(name, symbolicName)
    //    {
    //        Value = 0;
    //    }
    //    public double Value { get; set; }
    //}
    #endregion
}

