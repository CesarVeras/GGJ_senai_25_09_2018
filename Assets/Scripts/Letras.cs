using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letras : MonoBehaviour
{
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

    // Use this for initialization
    void Start()
    {
        textoTemp = "";
        tempoInicio = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Time.time - tempoFim >= tempoPausa && !pausado)
        {
            textoEntrada.text += ' ';
            pausado = true;
        }*/

        if ((textoTemp.Length == 4 || Time.time - tempoFim >= tempoLetra)) //&& !pausado)
        {
            palavras.Add(textoTemp);
            textoEntrada.text += Traduzir(textoTemp);
            textoTemp = "";
            if (Time.time - tempoFim >= tempoPalavra && !pausado && !escrevendo)
            {
                textoEntrada.text += " ";
                pausado = true;
            }
        }
    }

    public void ComecarEscrever()
    {
        tempoInicio = Time.time;
        escrevendo = true;
    }

    public void PararEscrever()
    {

        if (Time.time - tempoInicio < 0.3f)
        {
            textoTemp += '.';
        }
        else
        {
            textoTemp += '-';
        }
        tempoFim = Time.time;
        pausado = false;
        escrevendo = false;
    }

    public string Traduzir(string palavra)
    {
        for (int i = 0; i < morse.Length; i++)
        {
            if (palavra == morse[i])
            {
                return letras[i];
            }
        }
        return "";
    }
}
