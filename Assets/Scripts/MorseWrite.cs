﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SoundGenerator))]
public class MorseWrite : MonoBehaviour
{
    public bool runMorse = true;
    public char[] letras = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    public string[] morse = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };
    public Text texto;
    public Light luz;
    public float vida = 5;
    public float vidaAtual;
    public string resposta = "cadeira";
    public Image imgVida;

    SoundGenerator sg;

    // Use this for initialization
    void Start()
    {
        if(runMorse)
            StartCoroutine(MorsePalavra(resposta));
        vidaAtual = vida;
        sg = GetComponent<SoundGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (texto.text.Length > 0)
                {
                    texto.text = texto.text.Substring(0, texto.text.Length - 1);
                }
            }
            else if(Input.GetKeyDown(KeyCode.Return))
            {
                if(resposta == texto.text)
                {
                    luz.won = true;
                } else
                {
                    vidaAtual--;
                    luz.lost = true;
                    texto.text = "";
                    SceneManager.LoadScene("Traduzir");
                }
            } else
            {
                string input = Input.inputString.ToUpper();
                print(input);
                texto.text += input;
            }
        }
        imgVida.fillAmount = vidaAtual / vida;
    }

    IEnumerator MorsePalavra(string str)
    {
        yield return new WaitForSeconds(2);
        foreach (char c in str)
        {
            print(c);
            if(c == ' ')
            {
                yield return new WaitForSeconds(1);
            } else
            {
                string m = "";
                for (int i = 0; i < letras.Length; i++)
                {
                    if(letras[i] == c)
                    {
                        m = morse[i];
                    }
                }

                foreach (char ch in m)
                {
                    luz.active = true;
                    sg.useSinusAudioWave = true;
                    float delay;
                    if (ch == '-')
                    {
                        delay = 0.6f;
                    } else
                    {
                        delay = 0.2f;
                    }
                    yield return new WaitForSeconds(delay);
                    luz.active = false;
                    sg.useSinusAudioWave = false;
                    yield return new WaitForSeconds(0.2f);
                }
            }
            yield return new WaitForSeconds(0.6f);
        }
    }
}
