using System;
using System.Collections;
using System.Collections.Generic;
using Toolkid.UIGrid;
using UnityEngine;

namespace Kuroshio {

    public class SeedSlotBehaviour : Placeables {

    }

    [Serializable]

    public struct SeedData {

#if UNITY_EDITOR
        public UnityDateTime m_plantTime;
        public UnityDateTime m_ripeTime;
#endif
        public DateTime PlantTime { get => plantTime; }
        public DateTime RipeTime { get => ripeTime; }

        private DateTime plantTime;
        private DateTime ripeTime;

        public SeedData(DateTime plantTime, DateTime ripeTime) {
            this.plantTime = plantTime;
            this.ripeTime = ripeTime;
#if UNITY_EDITOR
            m_plantTime = new UnityDateTime(plantTime);
            m_ripeTime = new UnityDateTime(ripeTime);
#endif
        }
    }
    [Serializable]
    public struct UnityDateTime {
        public int Hour;
        public int Minute;
        public int Second;
        public UnityDateTime(DateTime dateTime) {
            Hour = dateTime.Hour;
            Minute = dateTime.Minute;
            Second = dateTime.Second;
        }
    }
}