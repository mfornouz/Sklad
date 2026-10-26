using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace Sklad.ViewsModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {

        public bool IsTablesEnabled { get; set; } = true;

    }
}
