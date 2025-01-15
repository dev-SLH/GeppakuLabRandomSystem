using UnityEngine;
using UnityEngine.Events;
using System.Runtime.InteropServices;

public class WindowsScreenModeDetector : MonoBehaviour
{
    public UnityEvent onFullScreenActivated;
    public UnityEvent onWindowedActivated;

    private bool isFullScreen;

#if UNITY_STANDALONE_WIN
    [DllImport("user32.dll")]
    private static extern bool IsZoomed(System.IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern System.IntPtr GetActiveWindow();
#endif

    private void Start()
    {
        DetectInitialScreenMode();
        Application.onBeforeRender += CheckScreenMode;
    }

    private void OnDestroy()
    {
        Application.onBeforeRender -= CheckScreenMode;
    }

    /// <summary>
    /// 프로그램 실행 시 초기 화면 모드 감지
    /// </summary>
    private void DetectInitialScreenMode()
    {
        #if UNITY_STANDALONE_WIN
        // 윈도우 모드인지 전체 화면 모드인지 확인
        isFullScreen = IsZoomed(GetActiveWindow()) || (Screen.fullScreenMode != FullScreenMode.Windowed);

        if (isFullScreen)
        {
            onFullScreenActivated?.Invoke();
            Debug.Log("Program Started in Full Screen Mode");
        }
        else
        {
            onWindowedActivated?.Invoke();
            Debug.Log("Program Started in Windowed Mode");
        }
        #endif
    }

    /// <summary>
    /// 전체화면 및 창 모드 전환 감지 (Alt+Enter 포함)
    /// </summary>
    private void CheckScreenMode()
    {
        #if UNITY_STANDALONE_WIN
        bool currentState = IsZoomed(GetActiveWindow()) || (Screen.fullScreenMode != FullScreenMode.Windowed);

        if (currentState != isFullScreen)
        {
            isFullScreen = currentState;

            if (isFullScreen)
            {
                onFullScreenActivated?.Invoke();
                Debug.Log("Full Screen Activated (Alt+Enter Detected)");
            }
            else
            {
                onWindowedActivated?.Invoke();
                Debug.Log("Windowed Mode Activated (Alt+Enter Detected)");
            }
        }
        #endif
    }
}