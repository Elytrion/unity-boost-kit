using System;
using UnityEngine;
using ZXing;

public static class QRCodeScanner
{
    private static WebCamTexture _camTexture;

    public static void StartWebCamera()
    {
        _camTexture = GetBackFacingWebCamTexture();
        if (_camTexture == null)
        {
            _camTexture = GetFirstWebCamTexture();
        }
        if (_camTexture != null)
        {
            _camTexture.Play();
        }
    }

    public static void StopWebCamera()
    {
        if (_camTexture.isPlaying)
        {
            _camTexture.Stop();
        }
    }

    public static WebCamTexture GetWebCameraFeed()
    {
        if (_camTexture != null)
        {
            return _camTexture;
        }
        else
        {
            return null;
        }
    }

    public static string DecodeCurrentFrame()
    {
        if (_camTexture != null)
        {
            try
            {
                IBarcodeReader qrCodeReader = new BarcodeReader();

                var result = qrCodeReader.Decode(_camTexture.GetPixels32(), _camTexture.width, _camTexture.height);
                if (result != null)
                {
                    return result.Text;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); return null; }
        }
        else
        {
            return null;
        }
    }

    private static WebCamTexture GetBackFacingWebCamTexture()
    {
        foreach (WebCamDevice device in WebCamTexture.devices)
        {
            if (!device.isFrontFacing)
            {
                return new WebCamTexture(device.name);
            }
        }

        return null;
    }

    private static WebCamTexture GetFirstWebCamTexture()
    {
        if (WebCamTexture.devices.Length > 0)
        {
            return new WebCamTexture(WebCamTexture.devices[0].name);
        }
        else
        {
            return null;
        }
    }

}
