using UnityEngine;

public class GameMenu : MonoBehaviour {

    public GameObject pauseRoot;

    // Use this for initialization
    void Awake() {
        PauseStage = PauseStages.NONE;
    }

    void Update() {
        if (Input.GetButtonDown("Pause")) {
            switch (internalPauseStage) {
                case PauseStages.NONE:
                case PauseStages.SETTINGS:
                case PauseStages.QUIT:
                    PauseStage = PauseStages.MAIN;
                    break;
                case PauseStages.MAIN:
                    PauseStage = PauseStages.NONE;
                    break;
            }
        }
    }

    private PauseStages internalPauseStage = PauseStages.NONE;

    private PauseStages PauseStage {
        get {
            return internalPauseStage;
        }
        set {
            internalPauseStage = value;

            Time.timeScale = 0f;

            switch (value) {
                case PauseStages.NONE:
                    pauseRoot.SetActive(false);
                    Time.timeScale = 1f;
                    break;
                case PauseStages.MAIN:
                    pauseRoot.SetActive(true);
                    pauseRoot.transform.Find("Main").gameObject.SetActive(true);
                    pauseRoot.transform.Find("Settings").gameObject.SetActive(false);
                    pauseRoot.transform.Find("Quit").gameObject.SetActive(false);
                    break;
                case PauseStages.SETTINGS:
                    pauseRoot.SetActive(true);
                    pauseRoot.transform.Find("Main").gameObject.SetActive(false);
                    pauseRoot.transform.Find("Settings").gameObject.SetActive(true);
                    pauseRoot.transform.Find("Quit").gameObject.SetActive(false);
                    break;
                case PauseStages.QUIT:
                    pauseRoot.SetActive(true);
                    pauseRoot.transform.Find("Main").gameObject.SetActive(false);
                    pauseRoot.transform.Find("Settings").gameObject.SetActive(false);
                    pauseRoot.transform.Find("Quit").gameObject.SetActive(true);
                    break;
            }
        }
    }

    public bool IsPaused {
        get {
            return PauseStage != PauseStages.NONE;
        }
    }

    public void UnpauseGame() {
        PauseStage = PauseStages.NONE;
    }

    public void PauseToMain() {
        PauseStage = PauseStages.MAIN;
    }

    public void PauseToSettings() {
        PauseStage = PauseStages.SETTINGS;
    }

    public void PauseToQuit() {
        PauseStage = PauseStages.QUIT;
    }

    public void QuitToDesktop() {
        Application.Quit();
    }

    public void SettingsSnappyMovement(bool value) {
        GameSettings.MovementStyle = value ? MovementStyles.SNAPPY : MovementStyles.SMOOTH;
    }

    public void SettingsVolumeSFX(float value) {
        GameSettings.VolumeSFX = value;
    }

    public void SettingsVolumeMusic(float value) {
        GameSettings.VolumeBGM = value;
    }
}

public enum PauseStages {
    NONE,
    MAIN,
    SETTINGS,
    QUIT
}
