using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceEditor : MonoBehaviour
{
    public static SequenceEditor editorWindow;

    SequenceHolder sequenceHolder;
    List<bool> sequence;
    List<Image> sequenceVisualization;
    int highlightIndex;
    [SerializeField] Vector3 highlightTargetPos;

    [SerializeField] RectTransform highlight;
    [SerializeField] RectTransform sequenceParent;
    [SerializeField] Sprite on, off;

    public void Init(SequenceHolder _sequenceHolder)
    {
        if (_sequenceHolder == null || editorWindow != null)
            Destroy(gameObject);
        else
            editorWindow = this;

        sequenceHolder = _sequenceHolder;

        sequence = sequenceHolder.GetSequence();

        sequenceVisualization = new List<Image>();

        foreach (bool slot in sequence) {
            GameObject t = new GameObject("slot",typeof(RectTransform));
            Image i = t.AddComponent<Image>();
            i.sprite = slot ? on : off;
            t.transform.SetParent(sequenceParent);
            if (i != null)
                sequenceVisualization.Add(i);
        }

        highlightTargetPos = sequenceParent.position;

        Time.timeScale = 0;
    }

    void UpdateHighlight(int newIndex) {
        highlightIndex = newIndex;
        highlightTargetPos = sequenceVisualization[highlightIndex].rectTransform.position;

    }

    private void Update()
    {
        int xInput = 0;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            xInput = -1;


        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            xInput = 1;

        if (xInput != 0)
            UpdateHighlight(Mathf.Clamp(highlightIndex + (int)(xInput),0,sequence.Count-1));

        if (highlight != null)
            highlight.position = Vector3.MoveTowards(highlight.position, highlightTargetPos, 25);
        else
            Debug.LogError("highlight not assigned, please do.");




        int yInput = 0;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            yInput = 1;


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            yInput = -1;

        if (yInput != 0) {
            sequence[highlightIndex] = !sequence[highlightIndex];
            sequenceVisualization[highlightIndex].sprite = (sequence[highlightIndex]) ? on : off;
        }


        if (Input.GetKeyDown(KeyCode.E)) {
            sequenceHolder.SetSequence(sequence);
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }

    public static bool IsOpen() {
        return (editorWindow != null);
    }
}
