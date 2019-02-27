using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QRCodeScanningExampleScript : MonoBehaviour
{
    private RawImage _cameraDisplay;

    private void Awake()
    {
        _cameraDisplay = GetComponent<RawImage>();
    }

    private void OnEnable()
    {
        QRCodeScanner.StartWebCamera();
    }

    private void OnDisable()
    {
        QRCodeScanner.StopWebCamera();
    }

    private void Update()
    {
        TryDecodeQRCode();
    }

    private void TryDecodeQRCode()
    {
        if (QRCodeScanner.GetWebCameraFeed().didUpdateThisFrame)
        {
            DisplayCamera();
            DecodeQRCode();
        }
    }

    private void DisplayCamera()
    {
        _cameraDisplay.texture = QRCodeScanner.GetWebCameraFeed();
        _cameraDisplay.rectTransform.localEulerAngles = new Vector3(0, 0, -QRCodeScanner.GetWebCameraFeed().videoRotationAngle);
    }

    private void DecodeQRCode()
    {
        string result = QRCodeScanner.DecodeCurrentFrame();

        if (result != null)
        {
            Debug.Log(result);
        }
    }
}
