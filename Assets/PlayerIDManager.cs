using UnityEngine;
using System.Collections;
using System;

public class PlayerIDManager : Singleton<PlayerIDManager> {
    protected PlayerIDManager() { }

    private Action _doneSettingUpPlayerName;

    [SerializeField]
    private RectTransform namePanel;

    private NameInput _nameInput;
    private NameInput nameInput {
        get {
            if(!_nameInput) {
                //missing ref exception?
                _nameInput = namePanel.GetComponentInChildren<NameInput>();
            }
            return _nameInput;
        }
    }

    public void OnEnable() {
        nameInput.onSubmitNameCompleted += playerNameSetupCompleted;
    }

    public void OnDisable() {
        nameInput.onSubmitNameCompleted -= playerNameSetupCompleted;
    }

    private void playerNameSetupCompleted() {
        namePanel.gameObject.SetActive(false);
        _doneSettingUpPlayerName.Invoke();
    }

    public void setupPlayerName(Action doneSettingUpPlayerName) {
        namePanel.gameObject.SetActive(true);
        _doneSettingUpPlayerName = doneSettingUpPlayerName;
        nameInput.getName();
    }
}
