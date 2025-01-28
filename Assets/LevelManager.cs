using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] spawners;
    [SerializeField] private float timer = 600f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private int nextLvlID;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private int levelReward = 1000;
    private GameData gameData;
    public event EventHandler<int> onWin;
    private bool timerReached = false;

    private void Start()
    {
        gameData = SaveSystem.LoadGame();
    }

    private void Update()
    {
        if (playerData.health > 0)
        {
            timer -= Time.deltaTime;

            int minutes, seconds;
            minutes = Mathf.FloorToInt(timer / 60);
            seconds = Mathf.FloorToInt(timer % 60);
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.SetDifficulty(minutes);
            }



            if (seconds < 10)
            {
                timerText.text = minutes.ToString() + ":" + "0" + seconds.ToString();
            }
            else
            {
                timerText.text = minutes.ToString() + ":" + seconds.ToString();
            }

            if (timer < 0 && !timerReached)
            {
                gameData.experience += levelReward + GlobalData.reward;
                timerReached = true;
                GlobalData.won = true;
                onWin.Invoke(this,levelReward + GlobalData.reward);
                SaveSystem.SaveGame(gameData);
                if(nextLvlID != -1)
                {
                    LevelsSystem.UnlockLevel(nextLvlID);
                }
                
                GlobalData.reward = 0;
            }
        }
        else
        {
            if (!timerReached)
            {
                timerReached = true;
                gameData.experience += GlobalData.reward;
                SaveSystem.SaveGame(gameData);
                GlobalData.reward = 0;
            }
            
        }


    }
}
