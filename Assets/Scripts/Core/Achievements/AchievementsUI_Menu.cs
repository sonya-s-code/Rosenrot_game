﻿using UnityEngine;
using UnityEngine.UI;

public class AchievementsUI_Menu : AchievementUI_Base
{
    [SerializeField]
    protected Text m_Progress;
    [SerializeField]
    protected Text m_Reward;
    [SerializeField]
    protected Image m_RewardImage;
    [SerializeField]
    private GameObject m_AchievementElementPrefab;

    private Achievement[] achievements;

    void Start()
    {
        Show();
    }

    public override void Show()
    {
        base.Show();

        achievements = GameController.Instance.AchievementRevards.Achievements;

        for (int i = 0; i < achievements.Length; i++)
        {
            SetFields(i);
            GameObject achievement = Instantiate(m_AchievementElementPrefab, this.transform);

        }
    }

    protected override void SetFields(int indexInResource)
    {
        base.SetFields(indexInResource);

        SetReward(indexInResource);

        SetProgress(indexInResource);
    }

    private void SetReward(int indexInResource)
    {
        int[] needToAchieve = GameController.Instance.AchievementRevards.Achievements[indexInResource].m_NeedToAchieve;
        int currentValue = AchievementsController.GetAchievement(GetType(indexInResource));

        string rewardString = "+";
        for (int i = 0; i < achievements[indexInResource].m_LeveledRevards.Length; i++)
        {
            if (i > 0)
            {
                rewardString += "/";
            }

            string leveledRew = achievements[indexInResource].m_LeveledRevards[i].ToString();

            if (currentValue >= needToAchieve[i])
            {
                leveledRew = "<color=#b73535ff>" + leveledRew + "</color>";
            }
            rewardString += leveledRew;
        }

        m_Reward.text = rewardString;

        m_RewardImage.sprite = GameController.Instance.AchievementRevards.RewardSprites[(int)achievements[indexInResource].m_RevardType];
    }

    private void SetProgress(int indexInResource)
    {
        int[] needToAchieve = GameController.Instance.AchievementRevards.Achievements[indexInResource].m_NeedToAchieve;
        int currentValue = AchievementsController.GetAchievement(GetType(indexInResource));

        int level = GetAchievementLevel(indexInResource);

        string progressString = currentValue.ToString() + "/";

        if (level < (needToAchieve.Length - 1))
        {
            progressString += "<color=#b73535ff>" + needToAchieve[level + 1].ToString() + "</color>";
        }
        else if (level == (needToAchieve.Length - 1))
        {
            progressString += "<color=#b73535ff>" + needToAchieve[needToAchieve.Length - 1].ToString() + "</color>";
        }

        m_Progress.text = progressString;
    }
}

