using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public static class QRCodeGenerator
{
    public static Sprite GenerateQRCode(string textToBeEncoded, int textureSize = 256)
    {
        Texture2D qrCode = new Texture2D(textureSize, textureSize);
        Color32[] qrEncodedTexture = EncodeString(textToBeEncoded, qrCode.width, qrCode.height);

        qrCode.SetPixels32(qrEncodedTexture);
        qrCode.Apply();
        return Sprite.Create(qrCode, new Rect(0, 0, qrCode.width, qrCode.height), new Vector2(0.5f, 0.5f));
    }

    private static Color32[] EncodeString(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

}
