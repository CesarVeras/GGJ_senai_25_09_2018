using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MorseWrite : MonoBehaviour
{

    public char[] letras = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    public string[] morse = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };
    public Text texto;
    public Light luz;
    public float vida = 5;
    public float vidaAtual;
    public string resposta = "rexi tegui manda um doguinho";
    public Image imgVida;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(MorsePalavra(resposta));
        vidaAtual = vida;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                texto.text = texto.text.Substring(0, texto.text.Length - 1);
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
                string input = Input.inputString;
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
                    yield return new WaitForSeconds(0.2f);
                }
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
