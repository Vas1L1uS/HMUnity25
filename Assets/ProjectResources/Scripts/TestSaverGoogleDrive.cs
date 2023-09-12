using UnityEngine;

public class TestSaverGoogleDrive : MonoBehaviour
{
    [SerializeField] private SaveLoadManager _saveLoadManager;
    [SerializeField] private PlayerSettingsConfig _playerSettings;

    [ContextMenu("Load")]
    public void Load()
    {
        _saveLoadManager.LoadFile();
        _saveLoadManager.LoadComplete += WriteInSettingsInfo;
    }

    [ContextMenu("Save")]
    public void Save()
    {
        WriteInSaverInfo();
        _saveLoadManager.Save();
    }

    private void WriteInSettingsInfo()
    {
        _playerSettings.JumpHeight = _saveLoadManager.Settings.JumpHeight;
        _playerSettings.Speed = _saveLoadManager.Settings.Speed;
        _playerSettings.Damage = _saveLoadManager.Settings.Damage;
        _playerSettings.MaxHealth = _saveLoadManager.Settings.MaxHealth;
    }

    private void WriteInSaverInfo()
    {
        _saveLoadManager.Settings.JumpHeight = _playerSettings.JumpHeight;
        _saveLoadManager.Settings.Speed = _playerSettings.Speed;
        _saveLoadManager.Settings.Damage = _playerSettings.Damage;
        _saveLoadManager.Settings.MaxHealth = _playerSettings.MaxHealth;
    }
}