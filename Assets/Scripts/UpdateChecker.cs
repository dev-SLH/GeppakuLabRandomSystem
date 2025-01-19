using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Michsky.MUIP;
using UnityEngine;

public class UpdateChecker : MonoBehaviour
{
    private string currentVersion; // 현재 버전
    private string apiUrl = "https://api.github.com/repos/dev-SLH/GeppakuLabRandomSystem/releases/latest"; // GitHub API URL
    private string downloadPath; // 다운로드된 설치 파일 경로

    public ModalWindowManager confirmWindow; // 업데이트 확인 창
    public ModalWindowManager installWindow; // 설치 진행 상태 창

    private string latestVersion; // 최신 버전
    private string downloadUrl; // 다운로드 URL
    private bool isApplicationQuitting = false; // 애플리케이션 종료 상태 확인용

    private void OnApplicationQuit()
    {
        isApplicationQuitting = true;

        // 다운로드된 파일 삭제
        if (File.Exists(downloadPath))
        {
            try
            {
                File.Delete(downloadPath);
                Debug.Log("ダウンロードされたインストーラーファイルが削除されました。\nDownloaded installer file has been deleted.");
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"ファイル削除中にエラー発生: {ex.Message}\nError while deleting file: {ex.Message}");
            }
        }
    }

    private async void Start()
    {
        // 현재 버전 및 다운로드 경로 초기화
        currentVersion = Application.version; // Unity의 Player Settings에서 설정된 버전
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
                var releaseInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseBody);

                //latestVersion = releaseInfo.tag_name.ToString();
                if (!string.IsNullOrEmpty(latestVersion) && latestVersion.StartsWith("v"))
                {
                    latestVersion = latestVersion.Substring(1); // 'v' 제거
                }

                if (!string.IsNullOrEmpty(latestVersion) && latestVersion != currentVersion)
                {
                    //downloadUrl = releaseInfo["assets"]?[0]?["browser_download_url"]?.ToString();
                    if (!string.IsNullOrEmpty(downloadUrl))
                    {
                        ShowConfirmWindow($"新しいバージョン {latestVersion} が見つかりました。\nWould you like to update to version {latestVersion}?");
                    }
                    else
                    {
                        ShowInstallWindow("ダウンロードリンクが見つかりません。\nDownload link not found.");
                    }
                }
                else
                {
                    ShowInstallWindow("現在のバージョンは最新です。\nCurrent version is up-to-date.");
                }
            }
            else
            {
                ShowInstallWindow($"アップデート確認失敗: {response.StatusCode}\nFailed to check for updates: {response.StatusCode}");
            }
        }
    }

    private async System.Threading.Tasks.Task DownloadAndInstall()
    {
        using (WebClient webClient = new WebClient())
        {
            ShowInstallWindow("ダウンロードを開始します。\nStarting download");

            // 버튼 비활성화
            installWindow.confirmButton.Interactable(false);

            webClient.DownloadProgressChanged += (s, e) =>
            {
                // 진행 중에는 버튼 비활성화 유지
                if (!isApplicationQuitting)
                {
                    installWindow.confirmButton.Interactable(false);
                    ShowInstallWindow($"ダウンロード中... {e.ProgressPercentage}% 完了\nDownloading... {e.ProgressPercentage}% complete");
                }
            };

            try
            {
                // 설치 파일 다운로드
                await webClient.DownloadFileTaskAsync(new Uri(downloadUrl), downloadPath);
                if (!isApplicationQuitting)
                {
                    ShowInstallWindow("ダウンロードが完了しました。インストールを開始します。\nDownload complete. Starting installation.");
                    RunInstaller(downloadPath);
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
                // 버튼 활성화
                if (!isApplicationQuitting)
                {
                    installWindow.confirmButton.Interactable(true);
                }
            }
        }
    }

    private void RunInstaller(string installerPath)
    {
        if (!File.Exists(installerPath))
        {
            Debug.LogError($"Installer not found at path: {installerPath}");
            ShowInstallWindow("インストーラが見つかりませんでした。\nInstaller file not found.");
            installWindow.confirmButton.gameObject.SetActive(true); // 버튼 활성화
            return;
        }

        Debug.Log($"Installer found at path: {installerPath}");
        Debug.Log($"Attempting to start installer...");

        var processInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = installerPath, // 설치 파일 직접 실행
            UseShellExecute = true,  // Shell 실행 사용
            Verb = "runas"           // 관리자 권한 요청
        };

        try
        {
            var process = System.Diagnostics.Process.Start(processInfo);
            if (process == null)
            {
                Debug.LogError("Failed to start the installer process.");
                ShowInstallWindow("インストーラの実行に失敗しました。\nFailed to start the installer process.");
            }
            else
            {
                Debug.Log("Installer started successfully.");
                Application.Quit(); // 현재 프로그램 종료
            }
        }
        catch (System.ComponentModel.Win32Exception ex)
        {
            Debug.LogError($"Installer execution canceled: {ex.Message}");
            ShowInstallWindow("インストーラの実行がキャンセルされました。管理者権限が必要です。\nInstallation canceled. Administrator privileges are required.");
            installWindow.confirmButton.gameObject.SetActive(true); // 버튼 활성화
        }
        catch (Exception ex)
        {
            Debug.LogError($"Unexpected error of type {ex.GetType().Name}: {ex.Message}");
            ShowInstallWindow($"予期しないエラーが発生しました: {ex.GetType().Name} - {ex.Message}\nUnexpected error occurred: {ex.GetType().Name} - {ex.Message}");
            installWindow.confirmButton.gameObject.SetActive(true); // 버튼 활성화
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
                await DownloadAndInstall();
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

    private void ShowInstallWindow(string description)
    {
        if (!Application.isPlaying || installWindow == null)
        {
            Debug.LogWarning("installWindow にアクセスできません。アプリケーションが終了中です。\nCannot access installWindow. The application is exiting.");
            return;
        }

        installWindow.descriptionText = description; // 설명 설정
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
