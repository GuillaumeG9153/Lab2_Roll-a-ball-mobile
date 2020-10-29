using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using TMPro;

public class TiltingGround : MonoBehaviour
{
    public TextMeshProUGUI gyroText;

    public bool isUseOldInput;

    private Quaternion initialQuaternion;

    private static Quaternion GyroToUnity(Quaternion q)
    {
        //change from right-handed coordinate to left-handed coordinate
        return new Quaternion(-q.x, -q.z, -q.y, q.w);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isUseOldInput)
        {
            //old input
            UnityEngine.Input.gyro.enabled = true;
            initialQuaternion = UnityEngine.Input.gyro.attitude;
        }
        //else
        //{
            //input system
           // InputSystem.EnableDevice(UnityEngine.InputSystem.AttitudeSensor.current);
            //initialQuaternion = UnityEngine.InputSystem.AttitudeSensor.current.attitude.ReadValue();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion q_sensor = Quaternion.identity;
        if (isUseOldInput)
        {
            q_sensor = UnityEngine.Input.gyro.attitude;
        }
        //else
        //{
            //q_sensor = UnityEngine.InputSystem.AttitudeSensor.current.attitude.ReadValue();
       // }

        this.transform.rotation = GyroToUnity(Quaternion.Inverse(initialQuaternion) * q_sensor);
        gyroText.text = (this.transform.rotation).eulerAngles.ToString("F6");
        
    }
}
