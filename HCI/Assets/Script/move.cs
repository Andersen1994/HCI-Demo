using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class move : MonoBehaviour {
    public SerialPort sp = new SerialPort("COM3", 115200);
    bool first = true;
    int count = 0;
    int[] serialArray = new int[2];
    
    void Start () {
        sp.Open();
        sp.ReadTimeout = 20;
    }

    // Update is called once per frame
    void Update () {
        if (sp.IsOpen)
        {
            serialArray[count] = sp.ReadByte();
            count++;
            if (first)
            {
                sp.Write("A");
                first = false;
            }
            else
            {           
                if (count > 1)
                {
                    MoveObject(serialArray[0], 0);
                    MoveObject(serialArray[1], 1);
                    sp.Write("A");
                    count = 0;
                 }

            }

        }
	}

    void MoveObject(int direction,int mode)
    {
        if (direction != 0 && mode == 1)
        {
            transform.Translate(0.03f, 0, 0);
        }
        if (direction != 0 && mode == 0)
        {
            transform.Translate(-0.03f, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.tag)
        {
            case "fire":
                Debug.Log("ENTER FIRE");
                HeatUp();
                break;
            case "ice":
                Debug.Log("ENTER ICE");
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
                Debug.Log("LEAVE FIRE");
                CoolDown();
                break;
            case "ice":
                Debug.Log("LEAVE ICE");
                HeatUp();
                break;
        }
        Debug.Log("trigger exit");
    }

    public void HeatUp()
    {
        sp.Write("H");
    }
    public void CoolDown()
    {
        sp.Write("C");
    }
}
