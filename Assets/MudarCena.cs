using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MudarCena : MonoBehaviour {

    public void GoToWrite() {
        SceneManager.LoadScene("Escrever");
    }

    public void GoToTranslate() {
        SceneManager.LoadScene("Traduzir");
    }
}
