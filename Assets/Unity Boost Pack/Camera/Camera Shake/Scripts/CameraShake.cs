using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* All credits to the script goes to ArmanDoesStuff on youtube
 * who created this camera shake script. I simply tweaked some variables and
 * functions to make it more versertile and readible. 
 * His tutorial is at https://www.youtube.com/watch?v=s3FS7AkiEnE
 * as of the creation of this script
 */

public class CameraShake : MonoBehaviour
{
    public bool IsCameraShakeActive;
    [Range(0, 1)] public float ShakeTime;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _shakeTimeMultiplier = 5f;
    [SerializeField] private float _shakeForceMagnitude = 0.9f;
    [SerializeField] private float _shakeRotationMagnitude = 0.2f;
    [SerializeField] private float _shakeDecayRate = 0.9f;

    private float _timeCounter;
    private bool _beforeFirstShake = true;
    private Vector3 _originalPosition;

    private void Update()
    {
        ShouldShakeCamera();
    }

    private void ShouldShakeCamera()
    {
        if (IsCameraShakeActive && ShakeTime > 0)
        {
            if (_beforeFirstShake)
            {
                _originalPosition = _camera.localPosition;
                _beforeFirstShake = false;
            }
            ShakeCamera();
        }
        else
        {
            ResetCameraPos();
            _beforeFirstShake = true;
        }
    }

    private void ShakeCamera()
    {
        _timeCounter += Time.deltaTime * Mathf.Pow(ShakeTime, 0.3f) * _shakeTimeMultiplier;
        Vector3 newPos = GetRandomNoiseVec3() * _shakeForceMagnitude * ShakeTime;
        _camera.localPosition = newPos;
        _camera.localRotation = Quaternion.Euler(newPos * _shakeRotationMagnitude);
        ShakeTime -= Time.deltaTime * _shakeDecayRate * ShakeTime;

        if (ShakeTime <= 0.1)
        {
            ShakeTime = 0;
            IsCameraShakeActive = false;
        }
    }

    private void ResetCameraPos()
    {
        Vector3 newPos = Vector3.Lerp(_camera.localPosition, _originalPosition, Time.deltaTime);
        _camera.localPosition = newPos;
        _camera.localRotation = Quaternion.Euler(newPos * _shakeRotationMagnitude);
    }

    private Vector3 GetRandomNoiseVec3()
    {
        return new Vector3
        (
            GetRandomNoiseFloat(1),
            GetRandomNoiseFloat(2),
            0
        );
    }

    private float GetRandomNoiseFloat(float inSeed)
    {
        return (Mathf.PerlinNoise(inSeed, _timeCounter) - 0.5f) * 2;
    }
}
