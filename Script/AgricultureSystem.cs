using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Playables;
namespace Kuroshio.Agriculture {

    public class AgricultureSystem {

        public static AgricultureSystem Current { get; private set; }
        
        public AgriculturePageManager Page => page;

        public event EventHandler<FarmData> Exhausted;
        public event EventHandler<FarmData> CropRipen;

        AgriculturePageManager page;

        FarmData[] farmDatas;
        TimeSpan elapsedTime;

        Dictionary<string, SeedData> seeds = new Dictionary<string, SeedData>() {
            { "coffee", new SeedData("coffee", TimeSpan.FromSeconds(60d), 1f) }
        };

        public AgricultureSystem() {
            Current = this;
        }

        public void ActiveAgriculturePage(AgriculturePageManager page) {
            this.page = page;
        }

        public bool TryGetLastEndTime(out DateTime lastEndTime) {
            lastEndTime = DateTime.MinValue;
            try {
                lastEndTime = DateTime.Now.AddSeconds(-30);
            } catch {
                Debug.Log("Failed to gain last end time from cloud.");
            }
            return lastEndTime == DateTime.MinValue;
        }

        public bool TryGetFromDatas(out FarmData[] farmDatas) {
            farmDatas = null;
            try {
                farmDatas = new FarmData[1];
                farmDatas[0] = new FarmData("coffee");
            }
            catch {
                Debug.Log("Failed to gain farm datas from cloud.");
            }
            return farmDatas != null;
        }
        
        public void Initializes() {            
            if (!TryGetLastEndTime(out DateTime lastEndTime)) {                
                return;
            }
                      
            if (!TryGetFromDatas(out farmDatas)) {                
                return;
            }            

            elapsedTime = DateTime.Now - lastEndTime;
            UpdateFarmDatas();
            if (Page) {
                SyncFarmDatas();
            }
            //RunFarmlandSystemInBackground();
        }

        private void UpdateFarmDatas() {
            for(int i = 0; i < farmDatas.Length; i++) {
                if(farmDatas[i].IsPlanted) {
                    farmDatas[i].SetSeedArgs(seeds[farmDatas[i].SeedId]);                    
                    TimeSpan remainingTime = farmDatas[i].RipenTime - farmDatas[i].ValidTime;
                    if(elapsedTime <= remainingTime) {
                        float consumption = elapsedTime.Seconds * farmDatas[i].ConsumptionRate;
                        if (farmDatas[i].Fertility < consumption) {
                            // 增加有效生長時間 = 剩餘肥沃度 / 肥沃度消耗比
                            TimeSpan validTime = TimeSpan.FromSeconds(farmDatas[i].Fertility / farmDatas[i].ConsumptionRate);
                            farmDatas[i].Fertility = 0;
                            farmDatas[i].ValidTime.Add(validTime);
                            // 在此調用肥沃度不足事件
                        }
                        else {
                            farmDatas[i].Fertility -= consumption;
                            farmDatas[i].ValidTime.Add(elapsedTime);
                        }
                    }
                    else {
                        float consumption = remainingTime.Seconds * farmDatas[i].ConsumptionRate;
                        if (farmDatas[i].Fertility < consumption) {
                            TimeSpan validTime = TimeSpan.FromSeconds(farmDatas[i].Fertility / farmDatas[i].ConsumptionRate);
                            farmDatas[i].Fertility = 0;
                            farmDatas[i].ValidTime.Add(validTime);
                            // 在此調用肥沃度不足事件
                        }
                        else {
                            farmDatas[i].Fertility -= consumption;
                            farmDatas[i].ValidTime.Add(remainingTime);
                            // 在此調用完全成熟事件
                        }
                    }
                }
            }
        }
        private void SyncFarmDatas() {
            // 同步更新後的農田資訊至農田頁面
        }
    }    
}