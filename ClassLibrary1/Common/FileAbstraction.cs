using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEditor.Core.Common
{

        public interface IFileAbstraction
        {
            System.IO.Stream Stream { get; }

            void CloseStream(System.IO.Stream stream);
        }
    
}
