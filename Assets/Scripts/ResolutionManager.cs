// 2025-01-13 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public enum ResolutionOption
    {
        SD,    // 854x480
        HD,    // 1280x720
        FHD, // 1920x1080
        QHD, // 2560×1440
        UHD    // 3840x2160
    }

    [VisibleEnum(typeof(ResolutionOption))]
    public void SetResolution(int resolution)
    {
        ResolutionOption option = (ResolutionOption)resolution;
        int width = 0, height = 0;
        
        switch (option)
        {
            case ResolutionOption.SD:
                width = 854;
                height = 480;
                break;
            case ResolutionOption.HD:
                width = 1280;
                height = 720;
                break;
            case ResolutionOption.FHD:
                width = 1920;
                height = 1080;
                break;
            case ResolutionOption.QHD:
                width = 2560;
                height = 1440;
                break;
            case ResolutionOption.UHD:
                width = 3840;
                height = 2160;
                break;
        }

        Screen.SetResolution(width, height, FullScreenMode.Windowed);
        
        // For full-screen mode, use the following line instead
        // Screen.SetResolution(width, height, FullScreenMode.FullScreenWindow);
    }
}
