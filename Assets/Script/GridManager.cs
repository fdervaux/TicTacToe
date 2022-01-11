using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public RectTransform _lineVLeft;
    public RectTransform _lineVRight;
    public RectTransform _lineHUp;
    public RectTransform _lineHDown;
    public float _scaleAnimationDuration;
    public AnimationCurve _scaleAnimationCurve;


    public IEnumerator ScaleAnimation(Vector3 scaleAnimationMask, RectTransform rectTransform)
    {
        float currentTime = 0;
        Vector3 startScale = rectTransform.localScale;

        while (currentTime <= _scaleAnimationDuration)
        {
            // 0 et 1
            float factor = _scaleAnimationCurve.Evaluate(currentTime / _scaleAnimationDuration);

            Vector3 newScale = Vector3.zero;

            newScale.x = Mathf.Lerp(startScale.x, startScale.x * factor, scaleAnimationMask.x);
            newScale.y = Mathf.Lerp(startScale.y, startScale.y * factor, scaleAnimationMask.y);
            newScale.z = Mathf.Lerp(startScale.z, startScale.z * factor, scaleAnimationMask.z);

            rectTransform.localScale = newScale;

            currentTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = startScale;
    }

    public void startGridAparitionAnimation()
    {
        StartCoroutine(ScaleAnimation(new Vector3(0, 1, 0), _lineVLeft));
        StartCoroutine(ScaleAnimation(new Vector3(0, 1, 0), _lineVRight));
        StartCoroutine(ScaleAnimation(new Vector3(1, 0, 0), _lineHDown));
        StartCoroutine(ScaleAnimation(new Vector3(1, 0, 0), _lineHUp));
    }
}
