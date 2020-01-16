using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midi2games
{
    interface IFormRule
    {
        void SetRule(HandleRule rule);
        void SaveRule();
        void CancelEdit();
    }
}
