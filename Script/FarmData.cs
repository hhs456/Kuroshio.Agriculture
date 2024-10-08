﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kuroshio.Agriculture {
    /// <summary>
    /// 屯田資料結構 (暫定)
    /// </summary>
    [Serializable]
    public class FarmData : ISeedArgs {
        public string SeedId => seedId;
        public bool IsPlanted => isPlanted;
        public float Fertility { get => fertility; set => fertility = value; }
        public DateTime PlantTime => plantTime;
        public TimeSpan ValidTime => validTime;
        
        public TimeSpan RipenTime { get; private set; }
        public float ConsumptionRate { get; private set; }

        [SerializeField] private string seedId;
        [SerializeField] private bool isPlanted;
        [SerializeField] private float fertility;
        [SerializeField] private DateTime plantTime;
        [SerializeField] private TimeSpan validTime;

        public FarmData(string seedId) {
            this.seedId = seedId;
        }

        public void SetSeedArgs(SeedData data) {
            ConsumptionRate = data.ConsumptionRate;
            RipenTime = data.RipenTime;
        }
    }
    /// <summary>
    /// 種子資料結構 (暫定)
    /// </summary>
    [Serializable]

    public struct SeedData : ISeedArgs {
        public string SeedId => seedId;
        public TimeSpan RipenTime => ripenTime;

        public float ConsumptionRate => consumptionRate;

        [SerializeField] private string seedId;
        [SerializeField] private TimeSpan ripenTime;
        [SerializeField] private float consumptionRate;

        public SeedData(string seedId, TimeSpan ripenTime, float consumptionRate) {
            this.seedId = seedId;
            this.ripenTime = ripenTime;
            this.consumptionRate = consumptionRate;
        }
    }

    public interface ISeedArgs {
        float ConsumptionRate { get; }
        TimeSpan RipenTime { get; }
    }
}
