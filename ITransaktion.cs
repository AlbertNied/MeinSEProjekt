using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeinSEProjekt
{
    public interface ITransaktion
    {
        void Durchfueren(decimal betrag);
    }
}
