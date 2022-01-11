using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public GridButton[] _gridButtons;
    public GameObject _ecranWin;
    public Text _scorePlayerOText;
    public Text _scorePlayerXText;
    public GridManager _gridManager;

    public Color _playerOColor;
    public Color _playerXColor;

    public GameObject infosPlayerO;
    public GameObject infosPlayerX;

    public GameObject scorePlayerO;
    public GameObject scorePlayerX;



    public VictoryScreenManager victoryScreenManager;



    private int _scorePlayerX = 0;
    private int _scorePlayerO = 0;
    private string _playerName = "O";


    public string getPlayerName()
    {
        return _playerName;
    }

    public Color getPlayerColor()
    {
        return getPlayerColor(_playerName);
    }

    public Color getPlayerColor(string player)
    {
        return player == "O" ? _playerOColor : _playerXColor;
    }

    void Awake()
    {
        victoryScreenManager.setGameManager(this);


        infosPlayerX.GetComponent<Image>().color = _playerXColor;
        infosPlayerO.GetComponent<Image>().color = _playerOColor;
        
        scorePlayerX.GetComponent<Image>().color = _playerXColor;
        scorePlayerO.GetComponent<Image>().color = _playerOColor;

        for (int i = 0; i < _gridButtons.Length; i++)
        {
            _gridButtons[i].setGameManager(this);
        }
    }

    private void Start() {
        init();
        printScore();
    }

    public void EndTurn()
    {
        if (testVictory())
        {
            for (int i = 0; i < _gridButtons.Length; i++)
            {
                _gridButtons[i].disable();
            }

            if (_playerName == "O")
                _scorePlayerO++;
            else
                _scorePlayerX++;

            printScore();

            victoryScreenManager.setVictoryPlayer(_playerName);

            victoryScreenManager.startAppearAnimation();

        }
        else if (testDraw())
        {
            victoryScreenManager.setDraw();

            victoryScreenManager.startAppearAnimation();
        }
        else
        {
            changePlayer();
        }
    }

    private void init()
    {
        _gridManager.startGridAparitionAnimation();
        victoryScreenManager.startDesappearAnimation();
        
        _playerName = Random.Range(0, 2) == 0 ? "O" : "X";

        printPlayerInfos();

        foreach(GridButton gridButtonscript in _gridButtons)
        {
            gridButtonscript.reset();
        }
    }

    private void printScore()
    {
        _scorePlayerOText.text = _scorePlayerO.ToString("00");
        _scorePlayerXText.text = _scorePlayerX.ToString("00");
    }

    private void printPlayerInfos()
    {
        if (_playerName == "O")
        {
            infosPlayerO.SetActive(true);
            infosPlayerX.SetActive(false);
        }
        else
        {
            infosPlayerO.SetActive(false);
            infosPlayerX.SetActive(true);
        }
    }

    private void changePlayer()
    {
        _playerName = _playerName == "O" ? "X" : "O";
        printPlayerInfos();
    }

    private bool testDraw()
    {
        for (int i = 0; i < _gridButtons.Length; i++)
        {
            if (_gridButtons[i]._button.interactable == true)
            {
                return false;
            }
        }
        return true;
    }

    private bool testEqualityBetween(int case1, int case2, int case3)
    {
        return  _gridButtons[case1].getStatus() == _gridButtons[case2].getStatus()
        && _gridButtons[case2].getStatus() == _gridButtons[case3].getStatus()
        && _gridButtons[case1].getStatus() != "";
    }

    private bool testVictory()
    {
        // ---- test rows
        if (testEqualityBetween(0, 1, 2))
            return true;

        if (testEqualityBetween(3, 4, 5))
            return true;

        if (testEqualityBetween(6, 7, 8))
            return true;


        // ---- test columns
        if (testEqualityBetween(0, 3, 6))
            return true;

        if (testEqualityBetween(1, 4, 7))
            return true;

        if (testEqualityBetween(2, 5, 8))
            return true;


        // ---- test diags
        if (testEqualityBetween(0, 4, 8))
            return true;

        if (testEqualityBetween(2, 4, 6))
            return true;

        return false;
    }

    

}
