using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEditor.Lib.Interfaces
{
    public interface ITagValidation<T>
    {
        bool Validate(T val);
    }
}
