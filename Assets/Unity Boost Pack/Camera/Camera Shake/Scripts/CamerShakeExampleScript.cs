using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamerShakeExampleScript : MonoBehaviour
{
    [Range(0, 1)] public float ShakeTime;
    [SerializeField] private Button _shakeCameraBtn;
    [SerializeField] private CameraShake _cameraShaker;

    private void Awake()
    {
        _shakeCameraBtn.onClick.AddListener(CallShakeCamera);
    }

    private void CallShakeCamera()
    {
        _cameraShaker.ShakeTime = ShakeTime;
        _cameraShaker.IsCameraShakeActive = true;
    }
}
