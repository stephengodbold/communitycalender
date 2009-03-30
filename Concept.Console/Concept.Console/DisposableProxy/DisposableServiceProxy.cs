using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Concept.Console.DisposableProxy
{
    public class DisposableServiceProxy : IServiceContract, IDisposable
    {
        #region IDisposable Members

        public void Dispose()
        {
            System.Console.WriteLine("Disposing service proxy");
        }

        #endregion

        #region IServiceContract Members

        public bool CheckACondition(int someValue)
        {
            return (someValue > 0);
        }

        #endregion
    }
}
