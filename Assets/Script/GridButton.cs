using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridButton : MonoBehaviour
{
    public Button _button;

    public GameObject _crossPrefab;
    public GameObject _circlePrefab;

    private GameObject _current;
    private GameManager _gameManager;

    public float _appearAnimationDuration;
    public AnimationCurve _appearAnimationCurve;
    public AnimationCurve _rotateAnimationCurve;

    private string _status = "";

    public void reset()
    {
        Destroy(_current);
        _status = "";
        active();
    }

    public void setGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Awake()
    {
        reset();
        _button.transform.Find("Cross");
    }

    public void disable()
    {
        _button.interactable = false;
    }

    public void active()
    {
        _button.interactable = true;
    }

    public string getStatus()
    {
        return _status;
    }

    public void onClickButton()
    {
        _status = _gameManager.getPlayerName();

        if( _status == "O" )
            _current = Instantiate(_circlePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        else
            _current = Instantiate(_crossPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        _current.transform.SetParent(this.transform, false);

        _current.GetComponent<Image>().color = _gameManager.getPlayerColor();   

        disable();

        startAppearAnimation();
        _gameManager.EndTurn();
    }

    public IEnumerator appearAnimation(Vector3 scaleAnimationMask, RectTransform rectTransform)
    {
        float currentTime = 0;
        Vector3 startScale = rectTransform.localScale;

        Quaternion startRotation = rectTransform.localRotation;

        while (currentTime <= _appearAnimationDuration)
        {
            // 0 et 1
            float factor = _appearAnimationCurve.Evaluate(currentTime / _appearAnimationDuration);

            Vector3 newScale = Vector3.zero;

            newScale.x = Mathf.Lerp(startScale.x, startScale.x * factor, scaleAnimationMask.x);
            newScale.y = Mathf.Lerp(startScale.y, startScale.y * factor, scaleAnimationMask.y);
            newScale.z = Mathf.Lerp(startScale.z, startScale.z * factor, scaleAnimationMask.z);
            rectTransform.localScale = newScale;


            float rotateFactor = _rotateAnimationCurve.Evaluate(currentTime / _appearAnimationDuration);

            rectTransform.localRotation = startRotation;
            rectTransform.Rotate(new Vector3(0, 0, rotateFactor * 180));


            currentTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = startScale;
        rectTransform.localRotation = startRotation;
    }

    public void startAppearAnimation()
    {
        StartCoroutine(appearAnimation(new Vector3(1, 1, 0), this.GetComponent<RectTransform>()));
    }

}
