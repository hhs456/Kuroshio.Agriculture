using System;
using System.Collections;
using System.Collections.Generic;
using Toolkid.UIGrid;
using UnityEngine;
using UnityEngine.UI;
namespace Kuroshio.Agriculture {
    public class FarmSlotBehaviour : MonoBehaviour {
        public float timeToGrow = 60f; // 成熟所需的時間（秒）
        private float currentTime = 0f; // 當前經過的時間
        private bool isPlanted = false; // 種子是否已經種下

        public GameObject treePrefab; // 成熟後的樹木預製件
        private string seedKey; // 用於唯一識別每顆種子的鍵
        public void PlantSeed(string uniqueSeedID) {
            isPlanted = true;
            currentTime = 0f;
            seedKey = uniqueSeedID; // 為每個種子分配唯一ID

            // 記錄當前系統時間
            string plantTime = DateTime.Now.ToString();
            PlayerPrefs.SetString(seedKey, plantTime);
            GetComponentInChildren<Text>().enabled = true;
        }

        // 更新種子狀態
        void Update() {
            if (isPlanted) {
                currentTime += Time.deltaTime;                

                float remainingTime = GetRemainingTime();
                // 更新 UI 來顯示倒數計時
                if (remainingTime > 0) {
                    TimeSpan timeSpan = TimeSpan.FromSeconds(remainingTime);
                    GetComponentInChildren<Text>().text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}"; // 顯示分鐘和秒數
                }
                else {
                    GetComponentInChildren<Text>().text = "樹已成熟！";
                }
                if (currentTime >= timeToGrow) {
                    GrowTree();
                }
            }
        }

        // 成熟後生成樹木
        private void GrowTree() {
            Instantiate(treePrefab, transform.position, Quaternion.identity);
            PlayerPrefs.DeleteKey(seedKey); // 移除記錄的時間
            Destroy(gameObject); // 摧毀種子物體
        }

        // 獲取剩餘時間
        public float GetRemainingTime() {
            return Mathf.Max(0, timeToGrow - currentTime);
        }

        // 從記錄中加載並計算剩餘時間
        public void LoadAndCalculateRemainingTime(string uniqueSeedID) {
            seedKey = uniqueSeedID;

            if (PlayerPrefs.HasKey(seedKey)) {
                string savedDateTime = PlayerPrefs.GetString(seedKey);
                DateTime plantedTime = DateTime.Parse(savedDateTime);
                TimeSpan elapsedTime = DateTime.Now - plantedTime;
                currentTime = (float)elapsedTime.TotalSeconds;

                if (currentTime >= timeToGrow) {
                    GrowTree();
                }
            }
        }
    }
}
