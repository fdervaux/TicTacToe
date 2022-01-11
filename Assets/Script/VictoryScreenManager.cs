using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreenManager : MonoBehaviour
{

    public float _animationDuration;
    public AnimationCurve _animationCurve;
    public float _appearAnimationDuration;
    public AnimationCurve _appearAnimationCurve;
    
    private CanvasGroup _canvasGroup;
    public GameObject victoryScreenPlayerO;
    public GameObject victoryScreenPlayerX;
    public Text victoryScreenText;

    private GameManager _gameManager;

    public void setGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }



    private void Awake() {
        _canvasGroup = GetComponent<CanvasGroup>();
        victoryScreenPlayerX.GetComponent<Image>().color = _gameManager.getPlayerColor("X");
        victoryScreenPlayerO.GetComponent<Image>().color = _gameManager.getPlayerColor("O");
    }

    public IEnumerator appearAnimation()
    {
        float currentTime = 0;
        while (currentTime <= _animationDuration)
        {
            // 0 et 1
            float factor = _animationCurve.Evaluate(currentTime / _animationDuration);

            _canvasGroup.alpha = factor;

            currentTime += Time.deltaTime;
            yield return null;
        }
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public IEnumerator desappearAnimation()
    {
        float currentTime = 0;

        while (currentTime <= _animationDuration)
        {
            // 0 et 1
            float factor = _animationCurve.Evaluate(1-(currentTime / _animationDuration));

            _canvasGroup.alpha = factor;

            currentTime += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    public void startAppearAnimation()
    {
        StartCoroutine(appearAnimation());
    }

    public void startDesappearAnimation()
    {
        StartCoroutine(desappearAnimation());
    }

    public void setVictoryPlayer(string player)
    {
        victoryScreenText.text = "à gagné !";

        if (player == "O")
        {
            victoryScreenPlayerO.SetActive(true);
            victoryScreenPlayerX.SetActive(false);
            StartCoroutine(appearAnimation(new Vector3(1, 1, 0), victoryScreenPlayerO.GetComponent<RectTransform>()));
        }

        else
        {
            victoryScreenPlayerX.SetActive(true);
            victoryScreenPlayerO.SetActive(false);
            StartCoroutine(appearAnimation(new Vector3(1, 1, 0), victoryScreenPlayerX.GetComponent<RectTransform>()));
        }
    }

    public void setDraw()
    {
        victoryScreenPlayerO.SetActive(true);
        victoryScreenPlayerX.SetActive(true);
        victoryScreenText.text = "Match Nul !";
        StartCoroutine(appearAnimation(new Vector3(1, 1, 0), victoryScreenPlayerO.GetComponent<RectTransform>()));
        StartCoroutine(appearAnimation(new Vector3(1, 1, 0), victoryScreenPlayerX.GetComponent<RectTransform>()));
    }

    public IEnumerator appearAnimation(Vector3 scaleAnimationMask, RectTransform rectTransform)
    {
        float currentTime = 0;
        Vector3 startScale = rectTransform.localScale;

        while (currentTime <= _appearAnimationDuration)
        {
            // 0 et 1
            float factor = _appearAnimationCurve.Evaluate(currentTime / _appearAnimationDuration);

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

}
