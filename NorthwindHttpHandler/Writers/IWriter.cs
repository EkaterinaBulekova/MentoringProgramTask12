using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindHttpHandler.Writers
{
    public interface IWriter
    {
        void Write<T>(IEnumerable<T> source, Stream outStream);
    }
}
