using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midi2games
{
    public delegate void RuleEventHandler(object sender, HandleRule rule);
    public delegate void IndexedRuleEventHandler(object sender, HandleRule rule, int index);
}
