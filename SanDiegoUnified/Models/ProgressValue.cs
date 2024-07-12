using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanDiegoUnified.Models
{
    public class ProgressValue
    {
        public static int downloadProgressbar { get; set; } = 0;
        public static int documentProgressbar { get; set; } = 0;
        public static int videoProgressbar { get; set; } = 0;
        public static int musicProgressbar { get; set; } = 0;
        public static int picProgressbar { get; set; } = 0;
        public static int desktopProgressbar { get; set; } = 0;
    }
}
