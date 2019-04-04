using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
    public void BeginGame() {
        SceneManager.LoadScene("SampleScene" /* too late to rename this */);
    }
}
