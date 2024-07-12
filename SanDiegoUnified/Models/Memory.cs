using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanDiegoUnified.Models
{
    public class Memory
    {
        public static int desktopMemoryGB { get; set; } = 0;
        public static int documentMemoryGB { get; set; } = 0;
        public static int videoMemoryGB { get; set; } = 0;
        public static int musicMemoryGB { get; set; } = 0;
        public static int pictureMemoryGB { get; set; } = 0;
        public static int downloadMemoryGB { get; set; } = 0;
        public static int audioFileMemoryGB { get; set; } = 0;
        public static int videoFileMemoryGB { get; set; } = 0;
        public static int miscFileMemoryGB { get; set; } = 0;
    }
}
