using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLeapMotionEpicApp
{
    class YoutubeVids
    {
        private String[] urls = new string[25];
        private Random rnd;

        public YoutubeVids()
        {
            rnd = new Random();
            urls[0] = "https://youtu.be/vTIIMJ9tUc8?t=27s";
            urls[1] = "https://youtu.be/ECWwpmP3spY?t=57s";
            urls[2] = "https://www.youtube.com/watch?v=Bk1_DbbzSdY";
            urls[3] = "https://youtu.be/vjW8wmF5VWc?t=27s";
            urls[4] = "https://youtu.be/3M_5oYU-IsU?t=2m30s";
            urls[5] = "https://youtu.be/otCpCn0l4Wo?t=14s";
            urls[6] = "https://youtu.be/9bZkp7q19f0?t=1m9s";
            urls[7] = "https://youtu.be/_P7S2lKif-A?t=29s";
            urls[8] = "https://youtu.be/jofNR_WkoCE?t=40s";
            urls[9] = "https://youtu.be/Ct6BUPvE2sM?t=15s";
            urls[10] = "https://youtu.be/msSc7Mv0QHY?t=14s";
            urls[11] = "https://youtu.be/k85mRPqvMbE?t=7s";
            urls[12] = "https://youtu.be/VHoT4N43jK8?t=1m2s";
        }

        public string getRandomUrl()
        {
            return urls[rnd.Next(13)];
        }
    }
}
