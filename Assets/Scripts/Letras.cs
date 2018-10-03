using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SoundGenerator))]
public class Letras : MonoBehaviour {
    public string[] letras = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    public string[] morse = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };
    public List<string> palavras;

    public string textoTemp;

    public bool pausado = false;
    public bool escrevendo = false;

    public bool esperandoLetra = true;
    public bool esperandoPalavra = false;
    public bool esperandoFrase = false;

    public float tempoInicio;
    public float tempoDecorrido;
    public float tempoFim;
    public float tempoLetra = 1f;
    public float tempoPalavra = 1.5f;
    public float tempoFrase = 2f;

    public Text textoEntrada;

    private SoundGenerator sg;

    // Use this for initialization
    void Start() {
        textoTemp = "";
        tempoInicio = Time.time;
        sg = GetComponent<SoundGenerator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.touchCount > 0) {
            TouchPhase phase = Input.GetTouch(0).phase;
            if (phase == TouchPhase.Began) {
                ComecarEscrever();
            } else if (phase == TouchPhase.Ended) {
                PararEscrever();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            ComecarEscrever();
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            PararEscrever();
        }

        if ((textoTemp.Length == 8 || Time.time - tempoFim >= tempoLetra) && !escrevendo) {
            palavras.Add(textoTemp);
            textoEntrada.text += Traduzir(textoTemp);
            textoTemp = "";
            if (Time.time - tempoFim >= tempoPalavra && !pausado && !escrevendo) {
                textoEntrada.text += " ";
                pausado = true;
            }
        }
    }

    public void ComecarEscrever() {
        tempoInicio = Time.time;
        escrevendo = true;
        sg.useSinusAudioWave = true;
    }

    public void PararEscrever() {

        if (Time.time - tempoInicio < 0.3f) {
            textoTemp += '.';
        } else {
            textoTemp += '-';
        }
        tempoFim = Time.time;
        pausado = false;
        escrevendo = false;
        sg.useSinusAudioWave = false;
    }

    public string Traduzir(string palavra) {
        for (int i = 0; i < morse.Length; i++) {
            if (palavra == morse[i]) {
                return letras[i];
            }
        }
        return "";
    }
}