using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanDiegoUnified.Models
{
    public class EstimateTime
    {
        public static int time { get; set; } = 0;
        public static int downloadTime { get; set; } = 0;
        public static int documentTime { get; set; } = 0;
        public static int videoTime { get; set; } = 0;
        public static int desktopTime { get; set; } = 0;
        public static int pictureTime { get; set; } = 0;
        public static int musicTime { get; set; } = 0;
        public static int videoFileTime { get; set; } = 0;
        public static int audioFileTime { get; set; } = 0;
        public static int miscFileTime { get; set; } = 0;
    }
}
