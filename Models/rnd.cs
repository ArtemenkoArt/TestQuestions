﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _012_TestQuestions
{
    public static class rnd
    {
        private static Random random;
        private static object syncObj = new object();

        public static int GetRandom(int min, int max)
        {
            lock (syncObj)
            {
                if (random == null)
                    random = new Random();
                return random.Next(min, max);
            }
        }
    }
}
