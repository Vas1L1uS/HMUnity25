using System.Text;
using UnityEngine;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;
using System.Collections;
using System;

public class SaveLoadManager : MonoBehaviour
{
    [HideInInspector] public PlayerSettings Settings;
    [SerializeField] private GoogleDriveSaveLoadConfig _config;

    public event Action LoadComplete;

    private void Awake()
    {
        Settings = new PlayerSettings();
    }

    public void Save()
    {
        var jsonString = JsonUtility.ToJson(Settings);

        if (_config.FileId == string.Empty)
        {
            StartCoroutine(UploadFile(_config.FileName, jsonString));
            return;
        }
        else
        {
            StartCoroutine(UpdateFile(_config.FileId, _config.FileName, jsonString));
        }
    }

    public void LoadFile()
    {
        var jsonString = JsonUtility.ToJson(Settings);

        if (_config.FileId == string.Empty)
        {
            Debug.Log($"FileId is empty! No file in GoogleDrive.");
            return;
        }
        else
        {
            StartCoroutine(DownloadFile(_config.FileId));
        }
    }



    private void WriteUploadFileInfo(GoogleDriveFiles.CreateRequest request)
    {
        if (_config.FileId == string.Empty) _config.FileId = request.ResponseData.Id;
        string result = System.Text.Encoding.UTF8.GetString(request.ResponseData.Content);
        Debug.Log($"Save in GoogleDrive {result}");
    }

    private void WriteDownloadFileInfo(GoogleDriveFiles.DownloadRequest request)
    {
        if (_config.FileId == string.Empty) _config.FileId = request.ResponseData.Id;
        string result = System.Text.Encoding.UTF8.GetString(request.ResponseData.Content);
        Debug.Log($"Load from GoogleDrive {result}");

        Settings = JsonUtility.FromJson<PlayerSettings>(result);
        LoadComplete?.Invoke();
    }

    private void WriteUpdateFileInfo(GoogleDriveFiles.UpdateRequest request)
    {
        string result = System.Text.Encoding.UTF8.GetString(request.RequestData.Content);
        Debug.Log($"Update file in GoogleDrive {result}");
    }

    public IEnumerator UploadFile(string fileName, string obj)
    {
        var file = new File { Name = fileName, Content = Encoding.ASCII.GetBytes(obj) };
        GoogleDriveFiles.CreateRequest request = GoogleDriveFiles.Create(file);
        yield return request.Send();
        WriteUploadFileInfo(request);
    }


    public IEnumerator DownloadFile(string fileId)
    {
        GoogleDriveFiles.DownloadRequest request = GoogleDriveFiles.Download(fileId);
        yield return request.Send();
        WriteDownloadFileInfo(request);
    }


    public IEnumerator UpdateFile(string fileId, string fileName, string obj)
    {
        var file = new File { Name = fileName, Content = Encoding.ASCII.GetBytes(obj) };
        GoogleDriveFiles.UpdateRequest request = GoogleDriveFiles.Update(fileId, file);
        yield return request.Send();
        WriteUpdateFileInfo(request);
    }
}