using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices; // WinAPI를 사용하기 위해 필요
using Michsky.MUIP;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class UpdateCheckerOpenFolder : MonoBehaviour
{
    // WinAPI 선언
    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    private static extern int ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

    private const int SW_SHOWNORMAL = 1; // 폴더를 기본 창으로 열기
    
    private string currentVersion; // 현재 버전
    private string apiUrl = "https://api.github.com/repos/dev-SLH/GeppakuLabRandomSystem/releases/latest"; // GitHub API URL
    private string downloadPath; // 다운로드된 설치 파일 경로

    public ModalWindowManager confirmWindow; // 업데이트 확인 창
    public ModalWindowManager installWindow; // 설치 진행 상태 창
    public ProgressBar progressBar; // 다운로드 진행률 표시
    
    private string latestVersion; // 최신 버전
    private string downloadUrl; // 다운로드 URL
    private bool isApplicationQuitting = false; // 강제 종료 플래그
    
    private void OnApplicationQuit()
    {
        // 강제 종료 플래그 설정
        isApplicationQuitting = true;
        Debug.Log("Application is quitting.");
    }
    
    private async void Start()
    {
        // 현재 버전 및 다운로드 경로 초기화
        currentVersion = Application.version; // Unity의 Player Settings에서 설정된 버전
        string projectTempFolder = Path.Combine(Path.GetTempPath(), "GeppakuLab");
        if (!Directory.Exists(projectTempFolder))
        {
            Directory.CreateDirectory(projectTempFolder);
        }
        
        string tempDownloadPath = Path.Combine(projectTempFolder, "GeppakuLab_RandomSystem_Installer.tmp");

        // 이전 임시 파일 삭제
        if (File.Exists(tempDownloadPath))
        {
            try
            {
                File.Delete(tempDownloadPath);
                Debug.Log($"Deleted leftover temp file: {tempDownloadPath}");
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Failed to delete leftover temp file: {ex.Message}");
            }
        }
        
        downloadPath = Path.Combine(Path.GetTempPath(), "GeppakuLab_RandomSystem_Installer.exe");

        // 업데이트 확인 실행
        await CheckForUpdate();
    }

    private async System.Threading.Tasks.Task CheckForUpdate()
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("User-Agent", "GeppakuLab Random System");

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(apiUrl);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject releaseInfo = JObject.Parse(responseBody);
                
                latestVersion = releaseInfo["tag_name"]?.ToString();
                if (!string.IsNullOrEmpty(latestVersion) && latestVersion.StartsWith("v"))
                {
                    latestVersion = latestVersion.Substring(1); // 'v' 제거
                }

                if (!string.IsNullOrEmpty(latestVersion) && latestVersion != currentVersion)
                {
                    // 첫 번째 Asset의 Download URL 가져오기
                    downloadUrl = releaseInfo["assets"]?[0]?["browser_download_url"]?.ToString();
                    if (!string.IsNullOrEmpty(downloadUrl))
                    {
                        ShowConfirmWindow($"新しいバージョン {latestVersion} が見つかりました。\nWould you like to download version {latestVersion}?");
                    }
                    else
                    {
                        ShowInstallWindow("ダウンロードリンクが見つかりません。\nDownload link not found.");
                    }
                }
                else
                {
                    return;
                    //ShowInstallWindow("現在のバージョンは最新です。\nCurrent version is up-to-date.");
                }
            }
            else
            {
                ShowInstallWindow($"アップデート確認失敗: {response.StatusCode}\nFailed to check for updates: {response.StatusCode}");
            }
        }
    }

    private async System.Threading.Tasks.Task DownloadAndOpenFolder()
    {
        // Temp 폴더 안에 고유 폴더 생성
        string projectTempFolder = Path.Combine(Path.GetTempPath(), "GeppakuLab");
        if (!Directory.Exists(projectTempFolder))
        {
            Directory.CreateDirectory(projectTempFolder);
            Debug.Log($"Created project-specific temp folder: {projectTempFolder}");
        }

        // 설치 파일 경로 설정
        string tempDownloadPath = Path.Combine(projectTempFolder, "GeppakuLab_RandomSystem_Installer.tmp");
        downloadPath = Path.Combine(projectTempFolder, "GeppakuLab_RandomSystem_Installer.exe");

        // 기존 임시 파일 삭제
        if (File.Exists(tempDownloadPath))
        {
            try
            {
                File.Delete(tempDownloadPath);
                Debug.Log($"Deleted previous temp file: {tempDownloadPath}");
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Failed to delete temp file: {ex.Message}");
            }
        }
        
        using (WebClient webClient = new WebClient())
        {
            ShowInstallWindow("ダウンロードを開始します。\nStarting download");

            progressBar.currentPercent = 0; // 초기화
            progressBar.gameObject.SetActive(true); // 활성화
            
            // 버튼 비활성화
            installWindow.confirmButton.Interactable(false);

            webClient.DownloadProgressChanged += (s, e) =>
            {
                if (!isApplicationQuitting)
                {
                    installWindow.confirmButton.Interactable(false);
                    progressBar.currentPercent = e.ProgressPercentage; // ProgressBar
                    ShowInstallWindow($"ダウンロード中...\nDownloading...");
                }
            };

            try
            {
                // 설치 파일 다운로드
                await webClient.DownloadFileTaskAsync(new Uri(downloadUrl), downloadPath);
                
                // 다운로드 완료 후 임시 파일을 최종 파일로 변경
                if (File.Exists(tempDownloadPath))
                {
                    File.Move(tempDownloadPath, downloadPath);
                    Debug.Log($"Download completed. Moved to final file: {downloadPath}");
                }

                if (!isApplicationQuitting)
                {
                    ShowInstallWindow("ダウンロードが完了しました。\nインストールフォルダを開きます。\nDownload complete. Opening installation folder.");
                    OpenFolder(Path.GetDirectoryName(downloadPath));
                    ShowInstallWindow("フォルダを開きました。\nインストーラーを実行してください。\nFolder opened. Please run the installer."); 
                }
            }
            catch (Exception ex)
            {
                if (!isApplicationQuitting)
                {
                    HandleException(ex);
                }
            }
            finally
            {
                if (!isApplicationQuitting)
                {
                    installWindow.confirmButton.Interactable(true);
                }
            }
        }
    }
    
    private void ShowConfirmWindow(string description)
    {
        if (confirmWindow != null)
        {
            confirmWindow.descriptionText = description;
            confirmWindow.UpdateUI();

            // 확인 버튼 클릭 리스너 추가
            confirmWindow.confirmButton.onClick.RemoveAllListeners();
            confirmWindow.confirmButton.onClick.AddListener(async () =>
            {
                confirmWindow.CloseWindow();
                await DownloadAndOpenFolder();
            });

            // 취소 버튼 클릭 리스너 추가
            confirmWindow.cancelButton.onClick.RemoveAllListeners();
            confirmWindow.cancelButton.onClick.AddListener(() =>
            {
                confirmWindow.CloseWindow();
            });

            confirmWindow.OpenWindow();
        }
        else
        {
            Debug.LogWarning("confirmWindow が設定されていません。\nconfirmWindow is not set.");
        }
    }

    private void OpenFolder(string folderPath)
    {
        Debug.Log($"Folder path: {folderPath}");
        Debug.Log($"Directory exists: {Directory.Exists(folderPath)}");
        
        if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
        {
            Debug.LogError($"Folder not found: {folderPath}");
            ShowInstallWindow("フォルダを開けませんでした。\nFailed to open folder.");
            return;
        }

        try
        {
            Debug.Log($"Opening folder: {folderPath}");
            ShellExecute(IntPtr.Zero, "open", folderPath, null, null, SW_SHOWNORMAL);
            
            // System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            // {
            //     FileName = folderPath,
            //     UseShellExecute = true
            // });
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to open folder: {ex.Message}");
            ShowInstallWindow($"フォルダを開く中にエラーが発生しました。\nError while opening folder: {ex.Message}");
        }
    }

    private void ShowInstallWindow(string description)
    {
        if (!Application.isPlaying || installWindow == null)
        {
            Debug.LogWarning("installWindow にアクセスできません。\nアプリケーションが終了中です。\nCannot access installWindow. The application is exiting.");
            return;
        }

        installWindow.descriptionText = description;
        installWindow.UpdateUI();
        installWindow.OpenWindow();
    }

    private void HandleException(Exception ex)
    {
        string userMessage = ex is WebException
            ? "ネットワークエラーが発生しました。\nNetwork error occurred."
            : $"予期しないエラーが発生しました: {ex.GetType().Name}\nUnexpected error occurred: {ex.GetType().Name}";

        ShowInstallWindow(userMessage);
    }
}
