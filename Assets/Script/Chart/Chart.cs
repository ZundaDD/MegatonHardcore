using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Megaton
{
    [Serializable]
    public class Chart
    {
        public ChartInfo Info;
        public ChartMusic Music;
        public List<Note> Content;
    }
}