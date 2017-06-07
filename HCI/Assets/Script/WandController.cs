using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class WandController : MonoBehaviour {
    public SerialPort sp = new SerialPort("COM3", 115200);
    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        sp.Open();
        sp.ReadTimeout = 12;
    }
	
	
	void Update () {
		if(controller == null)
        {
            Debug.Log("controller not initialized");
            return;
        }
        if (controller.GetPressDown(gripButton))
        {
            Debug.Log("girp button pressed");
        }
        if (controller.GetPressUp(gripButton))
        {
            Debug.Log("grip button unpressed");
        }
	}
    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.tag)
        {
            case "fire":
                HeatUp();
                break;
            case "ice":
                CoolDown();
                break;
        }

        Debug.Log("trigger entered");
    }
 
     private void OnTriggerExit(Collider collider)
    {
        switch (collider.tag)
        {
            case "fire":
                CoolDown();
                break;
            case "ice":
                HeatUp();
                break;
        }
        Debug.Log("trigger exit");
    }

    public void HeatUp()
    {
        sp.Write("1");
    }
    public void CoolDown()
    {
        sp.Write("0");
    }
}
