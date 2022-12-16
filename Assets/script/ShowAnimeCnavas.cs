using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAnimeCnavas : MonoBehaviour
{
    [SerializeField] GameObject animecanvas;
    public void ShowCnavas()
    {
        animecanvas.SetActive(true);
    }

    public void ExitCanvas()
    {
        animecanvas.SetActive(false);
    }
}
