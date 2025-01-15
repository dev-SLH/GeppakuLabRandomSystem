// 2025-01-12 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

#if UNITY_STANDALONE_WIN
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class AspectRatioController : MonoBehaviour
{
    public float targetAspectWidth = 16f;  // 인스펙터에서 설정할 목표 가로 비율
    public float targetAspectHeight = 9f;  // 인스펙터에서 설정할 목표 세로 비율
    public ResolutionOption minimumResolution = ResolutionOption.HD; // 최소 해상도 옵션

    private float targetAspectRatio;

    public enum ResolutionOption
    {
        SD,  // 640x480
        HD,  // 1280x720
        FHD, // 1920x1080
        QHD, // 2560x1440
        UHD  // 3840x2160
    }

    private const int SWP_NOZORDER = 0x0004;
    private const int SWP_FRAMECHANGED = 0x0020;

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    private struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    void Start()
    {
        targetAspectRatio = targetAspectWidth / targetAspectHeight;
    }

    void Update()
    {
        IntPtr hwnd = GetForegroundWindow();
        if (GetWindowRect(hwnd, out Rect rect))
        {
            int currentWidth = rect.Right - rect.Left;
            int currentHeight = rect.Bottom - rect.Top;

            if (IsBelowMinimumResolution(currentWidth, currentHeight))
            {
                // 최소 크기 이하로 드래그 중일 때는 아무 작업도 하지 않습니다.
                return;
            }

            // 최소 크기 이상일 경우 비율 유지
            AdjustToAspectRatio(hwnd, rect.Left, rect.Top, currentWidth, currentHeight);
        }
    }

    private bool IsBelowMinimumResolution(int width, int height)
    {
        (int minWidth, int minHeight) = GetMinimumResolution();
        return width < minWidth || height < minHeight;
    }

    private void AdjustToAspectRatio(IntPtr hwnd, int left, int top, int currentWidth, int currentHeight)
    {
        int newWidth = currentWidth;
        int newHeight = currentHeight;

        float currentAspectRatio = (float)currentWidth / currentHeight;

        if (Math.Abs(currentAspectRatio - targetAspectRatio) > 0.01f)
        {
            if (currentAspectRatio > targetAspectRatio)
            {
                newHeight = Mathf.RoundToInt(currentWidth / targetAspectRatio);
            }
            else
            {
                newWidth = Mathf.RoundToInt(currentHeight * targetAspectRatio);
            }

            SetWindowPos(hwnd, IntPtr.Zero, left, top, newWidth, newHeight, SWP_NOZORDER | SWP_FRAMECHANGED);
        }
    }

    private (int, int) GetMinimumResolution()
    {
        switch (minimumResolution)
        {
            case ResolutionOption.SD:
                return (640, 480);
            case ResolutionOption.HD:
                return (1280, 720);
            case ResolutionOption.FHD:
                return (1920, 1080);
            case ResolutionOption.QHD:
                return (2560, 1440);
            case ResolutionOption.UHD:
                return (3840, 2160);
            default:
                return (1280, 720); // 기본 HD 해상도
        }
    }
}
#endif
