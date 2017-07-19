using UnityEngine;
using System.Collections;
using libStreamSDK;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using libStreamSDK;

public class Teste : MonoBehaviour {

    // Use this for initialization
    void Start () {
        NativeThinkgear native = new NativeThinkgear();    

        // generate a handle to a ThinkGear connection
        int handleID = NativeThinkgear.TG_GetNewConnectionId();
        // perform the actual connection
        int connectStatus = NativeThinkgear.TG_Connect(handleID,
                "/dev/tty.MindSet",
                NativeThinkgear.Baudrate.TG_BAUD_57600,
                NativeThinkgear.SerialDataFormat.TG_STREAM_PACKETS);


        StartCoroutine(TryConnection(connectStatus, handleID));

    }

    IEnumerator TryConnection(int connectStatus, int handleID)
    {
        //while (true)
        //{
            Debug.Log("Try Connection");
            if (connectStatus >= 0)
            {
                // sleep for 1.5 seconds
                yield return new WaitForSeconds(1.5f);

                // read all of the data in the buffer
                int packetCount = NativeThinkgear.TG_ReadPackets(handleID, -1);

                // we've received some data, thus we've connected to a valid headset
                if (packetCount > 0)
                {
                    // implement some behavior here
                    Debug.Log("Connect");
                }
                // no valid headset data received, so close the connection
                else
                {
                    NativeThinkgear.TG_FreeConnection(handleID);
                    Debug.Log("Fail Connect");
                }
            }
            else
            {
                // the connection attempt was unsuccessful
                NativeThinkgear.TG_FreeConnection(handleID);
                Debug.Log("Fail Connect");
            }        
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
