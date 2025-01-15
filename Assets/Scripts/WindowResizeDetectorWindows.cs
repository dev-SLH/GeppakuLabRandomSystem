// 2025-01-12 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using System;
using System.Runtime.InteropServices;
using Michsky.MUIP;
using UnityEngine;
using UnityEngine.Events;

public class WindowResizeDetectorWindows : MonoBehaviour
{
    public enum ResolutionOption
    {
        SD,
        HD,
        FHD,
        QHD,
        UHD
    }

    [SerializeField] 
    private ResolutionOption minimumResolution = ResolutionOption.HD;
    
    public CustomDropdown minimumDropdown;
    
    private const string PREFS_MIN_RESOLUTION_KEY = "minimumResolution";
    
    #if UNITY_STANDALONE_WIN && !UNITY_EDITOR
    private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    private static IntPtr windowHandle;
    private static IntPtr originalWndProc;
    private static WndProcDelegate customWndProcDelegate;

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    private const int GWL_WNDPROC = -4;
    private const uint WM_EXITSIZEMOVE = 0x0232;
    private const uint SWP_NOZORDER = 0x0004;
    private const uint SWP_FRAMECHANGED = 0x0020;
    #endif

    private void Awake()
    {
        if (PlayerPrefs.HasKey(PREFS_MIN_RESOLUTION_KEY))
        {
            minimumResolution= (ResolutionOption)PlayerPrefs.GetInt("minimumResolution");
            minimumDropdown.ChangeDropdownInfo(PlayerPrefs.GetInt("minimumResolution"));
        }
    }
    
    void Start()
    {
        #if UNITY_STANDALONE_WIN && !UNITY_EDITOR
        windowHandle = GetForegroundWindow();
        customWndProcDelegate = CustomWndProc;
        originalWndProc = GetWindowLongPtr(windowHandle, GWL_WNDPROC);
        SetWindowLongPtr(windowHandle, GWL_WNDPROC, Marshal.GetFunctionPointerForDelegate(customWndProcDelegate));
        #endif
    }

    private (int, int) GetMinimumResolutionInstance()
    {
        switch (minimumResolution)
        {
            case ResolutionOption.SD:
                return (854, 480);
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

    [VisibleEnum(typeof(ResolutionOption))]
    public void SetMinimumResolution(int newResolution)
    {
        PlayerPrefs.SetInt("minimumResolution", (int)newResolution);
        PlayerPrefs.Save();
        
        minimumResolution = (ResolutionOption)newResolution;
        Debug.Log("Minimum resolution changed to: " + minimumResolution.ToString());
    }
    
    #if UNITY_STANDALONE_WIN && !UNITY_EDITOR
    private static void EnforceMinimumSize(IntPtr hWnd, WindowResizeDetectorWindows instance)
    {
        if (GetWindowRect(hWnd, out Rect rect))
        {
            int currentWidth = rect.Right - rect.Left;
            int currentHeight = rect.Bottom - rect.Top;

            (int minWidth, int minHeight) = instance.GetMinimumResolutionInstance();
            if (currentWidth < minWidth || currentHeight < minHeight)
            {
                SetWindowPos(hWnd, IntPtr.Zero, rect.Left, rect.Top, minWidth, minHeight, SWP_NOZORDER | SWP_FRAMECHANGED);
            }
        }
    }

    [AOT.MonoPInvokeCallback(typeof(WndProcDelegate))]
    private static IntPtr CustomWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        if (msg == WM_EXITSIZEMOVE)
        {
            Debug.Log("Resize Ended");
            var instance = (WindowResizeDetectorWindows)FindObjectOfType(typeof(WindowResizeDetectorWindows));
            if (instance != null)
            {
                EnforceMinimumSize(hWnd, instance);
            }
        }
        return CallWindowProc(originalWndProc, hWnd, msg, wParam, lParam);
    }
    #endif
    
    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    private struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}