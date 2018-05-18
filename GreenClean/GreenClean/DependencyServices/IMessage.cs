using System;
using System.Collections.Generic;
using System.Text;

namespace GreenClean.DependencyServices
{
    public interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
