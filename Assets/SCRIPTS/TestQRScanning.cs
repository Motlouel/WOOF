using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQRScanning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var webCamDevices = WebCamTexture.devices;
        foreach (var camDevice in webCamDevices)
        {
            print("Camera Device found: " +camDevice.name);
            print("\tFront Facing: " + camDevice.isFrontFacing);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
