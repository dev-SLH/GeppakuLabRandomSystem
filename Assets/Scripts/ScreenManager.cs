using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private int windowedWidth;
    private int windowedHeight;
    private bool isFullscreen;

    private void Start()
    {
        // 현재 해상도 저장 및 모드 감지
        isFullscreen = Screen.fullScreenMode != FullScreenMode.Windowed;
        
        if (!isFullscreen)
        {
            windowedWidth = Screen.width;
            windowedHeight = Screen.height;
        }
        else
        {
            // 전체화면 모드에서 창 모드 해상도 초기화
            windowedWidth = Mathf.Min(Screen.currentResolution.width / 2, 1920);
            windowedHeight = Mathf.Min(Screen.currentResolution.height / 2, 1080);
        }
    }

    /// <summary>
    /// 전체화면 모드로 전환
    /// </summary>
    public void SetFullscreen()
    {
        if (!isFullscreen)
        {
            windowedWidth = Screen.width;
            windowedHeight = Screen.height;
            Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, FullScreenMode.FullScreenWindow);
            isFullscreen = true;
        }
    }

    /// <summary>
    /// 창 모드로 전환 (정상 해상도 유지)
    /// </summary>
    public void SetWindowed()
    {
        if (isFullscreen || Screen.fullScreenMode != FullScreenMode.Windowed)
        {
            StartCoroutine(SetWindowedWithCorrectResolution());
        }
    }

    /// <summary>
    /// 창 모드로 전환 (정상 해상도로 강제 적용)
    /// </summary>
    private System.Collections.IEnumerator SetWindowedWithCorrectResolution()
    {
        // 창 모드 적용 및 해상도 조정
        int safeWidth = Mathf.Clamp(windowedWidth, 800, Screen.currentResolution.width);
        int safeHeight = Mathf.Clamp(windowedHeight, 600, Screen.currentResolution.height);
        
        Screen.SetResolution(safeWidth, safeHeight, FullScreenMode.Windowed);

        // 렌더링 이후 강제 재확인 및 재적용
        yield return new WaitForEndOfFrame();

        if (Screen.fullScreenMode != FullScreenMode.Windowed)
        {
            Screen.SetResolution(safeWidth, safeHeight, FullScreenMode.Windowed);
            Debug.LogWarning($"Screen mode forced to Windowed at {safeWidth}x{safeHeight}");
        }
        else
        {
            Debug.Log($"Windowed mode successfully applied at {safeWidth}x{safeHeight}");
        }

        isFullscreen = false;
    }
}