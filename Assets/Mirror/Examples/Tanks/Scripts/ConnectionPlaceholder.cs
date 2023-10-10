using System.Collections;
using TMPro;
using UnityEngine;

public class ConnectionPlaceholder : MonoBehaviour
{
    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private TMP_Text _infoText;

    private Coroutine _animation;

    public void Show()
    {
        if (_animation != null)
            StopCoroutine(_animation);

        _animation = StartCoroutine(AnimationLoop());
        
        _selfCanvas.enabled = true;
    }

    public void Hide()
    {
        _selfCanvas.enabled = false;

        StopCoroutine(_animation);
        _animation = null;
    }

    private IEnumerator AnimationLoop()
    {
        int dotCount = 0;

        while (true)
        {
            _infoText.text = "Connection" + new string('.', dotCount);
            dotCount = (dotCount + 1) % 3;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
