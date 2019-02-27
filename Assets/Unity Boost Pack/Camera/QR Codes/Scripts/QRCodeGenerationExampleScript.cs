using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QRCodeGenerationExampleScript : MonoBehaviour
{
    [SerializeField] private string _stringToEncode = string.Empty;
    private Image _displayQRImage;

    private void Awake()
    {
        _displayQRImage = GetComponent<Image>();
    }

    private void Start()
    {
        TryCreateQRCode();
    }

    private void TryCreateQRCode()
    {
        if (_stringToEncode.Length > 0)
        {
            CreateQRCode();
        }
    }

    private void CreateQRCode()
    {
        _displayQRImage.sprite = QRCodeGenerator.GenerateQRCode(_stringToEncode);
    }
}
