using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI text;
    public string[] messages;

    [SerializeField] float timeBtwnChars;
    [SerializeField] float timeBtwnWords;

    private int i = 0;

    void Start()
    {
        EndCheck();
    }

    private void Update()
    {
        if (Input.GetButtonUp("Continue"))
        {
            i += 1;
            StopAllCoroutines();
            EndCheck();
        }
    }

    public void EndCheck()
    {
        if (i <= messages.Length - 1)
        {
            text.text = messages[i];
            StartCoroutine(TextVisible());
        }
        else
            dialogPanel.SetActive(false);
    }

    private IEnumerator TextVisible()
    {
        text.ForceMeshUpdate();
        int totalVisibleCharacters = text.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            text.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {

                text.maxVisibleCharacters = visibleCount + 3;

                string continueStr = " >>";
                string textTemp = text.text;
                string textContinue = text.text + continueStr;

                while (true)
                {
                    yield return new WaitForSeconds(0.3f);
                    text.text = textContinue;
                    yield return new WaitForSeconds(0.3f);
                    text.text = textTemp;
                }
            }

            counter += 1;
            yield return new WaitForSeconds(timeBtwnChars);


        }
    }
}
