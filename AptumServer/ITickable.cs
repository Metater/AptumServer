using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer
{
    public interface ITickable
    {
        public void Tick(long id);
    }
}
