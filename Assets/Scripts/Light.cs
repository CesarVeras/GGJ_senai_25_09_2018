using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Light : MonoBehaviour
{

    public Color on;
    public Color off;
    public Color win;
    public Color lose;
    public bool active = false;
    public bool won = false;
    public bool lost = false;
    public Image img;

    // Use this for initialization
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (won)
        {
            img.color = win;

        }
        else if (lost)
        {
            img.color = lose;

        }
        else if (!(active && (img.color == on)))
        {
            img.color = active ? on : off;
        }
    }
}
