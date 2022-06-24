using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamDeckWindows.Models
{
    public class Setting
    {
        public int SettingId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Tool> Tools { get; private set; } = new ObservableCollection<Tool>();
    }
    public class Tool
    {
        public int ToolId { get; set; }
        public string Name { get; set; }

    }
}
