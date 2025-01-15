using UnityEngine;

// Quits the player when the user hits escape

public class Quit : MonoBehaviour
{
    public static void QuitAct()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }
}