using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SoundGenerator))]
public class Letras : MonoBehaviour {
    private string[] letras = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
    private string[] morse = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--..", ".----", "..---", "...--", "....-", ".....", "-....", "--...", "---..", "----.", "-----" };
    
    public string textoTemp;

    private bool pausado = false;
    private bool escrevendo = false;
    private bool escrito = false;

    private bool esperandoLetra = true;
    private bool esperandoPalavra = false;
    private bool esperandoFrase = false;

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
        if(Input.GetKey(KeyCode.Backspace)) {
            Recomecar();
        }
        if ((Time.time - tempoFim >= tempoLetra) && !escrevendo && !escrito) {
            escrito = true;
            print(textoTemp);
            textoEntrada.text += Traduzir(textoTemp).ToUpper();
            textoTemp = "";
            if (Time.time - tempoFim >= tempoPalavra && !pausado && !escrevendo) {
                textoEntrada.text += " ";
                pausado = true;
            }
        }
    }

    public void Recomecar() {
        textoEntrada.text = "";
    }

    public void ComecarEscrever() {
        tempoInicio = Time.time;
        escrito = false;
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