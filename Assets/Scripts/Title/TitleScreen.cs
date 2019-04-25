using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
    public void BeginDemo() {
        //SceneManager.LoadScene("SampleScene" /* too late to rename this */);
        throw new System.Exception("please just use level zero for now");
    }

    public void BeginGame() {
        SceneManager.LoadScene("LevelZero");
    }
}
