using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controller
{
    public interface IFrmSave
    {
        string TestName { get; set; }
        void showMessageBox(string message);
    }
}
