using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private Slider dialogSlider;
    [SerializeField] private string[] dialog = new string[8];

    private void Awake()
    {
        StartCoroutine(DialogChange());
    }

    IEnumerator DialogChange()
    {
        while (true)
        {
            for (int i = 0; i < dialog.Length; i++)
            {
                yield return new WaitUntil(() => dialogSlider.value >= 0.125f * (i + 1));
                dialogText.text = dialog[i];
            
                yield return new WaitUntil(() => dialogSlider.value >= 0.125f * (i + 1));
                dialogText.text = dialog[i];
            
                yield return new WaitUntil(() => dialogSlider.value >= 0.125f * (i + 1));
                dialogText.text = dialog[i];
            
                yield return new WaitUntil(() => dialogSlider.value >= 0.125f * (i + 1));
                dialogText.text = dialog[i];
            
                yield return new WaitUntil(() => dialogSlider.value >= 0.125f * (i + 1));
                dialogText.text = dialog[i];
            
                yield return new WaitUntil(() => dialogSlider.value >= 0.125f * (i + 1));
                dialogText.text = dialog[i];
            
                yield return new WaitUntil(() => dialogSlider.value >= 0.125f * (i + 1));
                dialogText.text = dialog[i];
            
                yield return new WaitUntil(() => dialogSlider.value >= 0.125f * (i + 1));
                dialogText.text = dialog[i];
                yield return new WaitUntil(() => dialogSlider.value >= 0.125f * (9 + 1));
            }

            yield return null;
        }
    }
}
