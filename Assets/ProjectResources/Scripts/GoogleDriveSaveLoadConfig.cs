using UnityEngine;

[CreateAssetMenu]
public class GoogleDriveSaveLoadConfig : ScriptableObject
{
    public string FileName{ get => _fileName; set => _fileName = value; }
    public string FileId { get => _fileID; set => _fileID = value; }

    [SerializeField] private string _fileName;
    [SerializeField] private string _fileID;
}