using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
    public void BeginDemo() {
        SceneManager.LoadScene("SampleScene" /* too late to rename this */);
    }

    public void BeginGame() {
        SceneManager.LoadScene("LevelZero");
    }
}
