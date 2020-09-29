﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Triangle.Configuration
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PercentagesList
    {
        [JsonProperty]
        private readonly List<int> mPercentagesSet = new List<int>();

        public void Set(int num)
        {
            int roundedNumber = GetRoundedNumber(num);
            mPercentagesSet.Add(roundedNumber);
        }

        public void Reset(int num)
        {
            int roundedNumber = GetRoundedNumber(num);
            mPercentagesSet.Remove(roundedNumber);
        }

        public bool HasLowerPercentage(int num)
        {
            return mPercentagesSet.Any(percentage => percentage <= num);
        }

        private int GetRoundedNumber(int num)
        {
            if (num >= 100)
                return 100;

            if (num <= 0)
                return 0;

            int roundedNumber = ((int)Math.Round(num / 10.0)) * 10;

            if (roundedNumber == 100)
                return 90;

            if (roundedNumber == 0)
                return 10;

            return roundedNumber;
        }
    }
}