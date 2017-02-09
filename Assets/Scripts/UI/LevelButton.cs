﻿using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelButton : MonoBehaviour
{

    private int levelCollectDiamonds;
    private int prevLevelCollectDiamonds;
    private int diamondsOnPrevLevel;
    private Button levelButton;
    private bool isBonusLevel;

    [SerializeField, HeaderAttribute("информация под камнем")]
    private Text diamondsInform;
    [SerializeField, HeaderAttribute("индекс уровня начиная с 0")]
    private int levelNumber;
    [SerializeField, HeaderAttribute("количество алмазов на уровне")]
    private int diamondsOnLevel;



    void Start()
    {
        levelCollectDiamonds = GameController.Instance.LevelsData[levelNumber].diamondsCollected;       //собрано алмазов на уровне

        if (levelNumber > 0)
        {
            prevLevelCollectDiamonds = GameController.Instance.LevelsData[levelNumber - 1].diamondsCollected;    //собрано алмазов на предыдущем уровне
            diamondsOnPrevLevel = GameController.Instance.LevelsData[levelNumber - 1].IsCollected.Length;       //всего алмазов на предыдущем уровне
        }

        if (levelCollectDiamonds == diamondsOnLevel)
        {
            isBonusLevel = true;
        }
        else
        {
            isBonusLevel = false;
        }

        levelButton = GetComponent<Button>();
        if ((prevLevelCollectDiamonds >= diamondsOnPrevLevel && diamondsOnPrevLevel != 0) || (levelNumber == 0))       //если уровень открыт или это первый уровень
        {
            levelButton.interactable = true;
        }
        else        //если уровень не доступен
        {
            levelButton.interactable = false;
        }

        diamondsInform.text = levelCollectDiamonds.ToString() + "/" + diamondsOnLevel.ToString();

    }

    public void LoadGameLevel(string name)
    {
        GameController.Instance.CurrentLevel = levelNumber;
        GameController.Instance.DiamondsOnLevel = diamondsOnLevel;
        GameController.Instance.OnBonusLevel = isBonusLevel;

        GameController.Instance.LoadScene(name);
    }
}
