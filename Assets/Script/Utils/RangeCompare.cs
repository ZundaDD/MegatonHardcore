using System;
using System.Collections.Generic;
using UnityEngine;

namespace Megaton
{
    public class RangeCompare <TRange,TValue> where TRange: IComparable
    {
        List<TRange> ranges;
        List<TValue> values;
        TValue defaultValue;

        public RangeCompare(List<TRange> ranges, List<TValue> values,TValue defaultValue)
        {
            this.ranges = ranges;
            this.values = values;
            this.defaultValue = defaultValue;
        }

        public TValue Query(TRange range)
        {
            for (int i = 0; i < ranges.Count; i++)
            {
                if(range.CompareTo(ranges[i]) <= 0) return values[i];
            }
            return defaultValue;
        }
    }

}