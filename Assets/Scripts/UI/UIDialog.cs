using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialog : MonoBehaviour {
    public Text text_Content;

    public void ShowText(Dialog dialog) {
        ShowText(dialog.sentences[0].content);
    }

    public void ShowText(string content) {
        StartCoroutine(ShowTextCor(content));
    }
    IEnumerator ShowTextCor(string content) {
        float interval = .03f;

        text_Content.text = "";

        for (int i = 0; i < content.Length; i++) {
            text_Content.text += content[i];

            yield return new WaitForSeconds(interval);
        }
    }
}
