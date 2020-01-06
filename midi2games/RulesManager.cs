using Melanchall.DryWetMidi.Devices;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midi2games
{
    class RulesManager
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public List<HandleRule> rulesStorage = new List<HandleRule>();
        public RulesManager()
        {
            
        }

        public void AddRule(HandleRule rule)
        {
            rulesStorage.Add(rule);
        }

        public void RemoveRule(HandleRule rule)
        {
            rulesStorage.Remove(rule);
        }

        public void Clear()
        {
            rulesStorage.Clear();
        }

        private MidiHandler handler;
        public MidiHandler MidiHandler {
            get { return handler; }
            set {
                if (handler != null)
                    handler.OnRecieveEvent -= HandleOnRecieve;
                handler = value;
                if (handler != null)
                    handler.OnRecieveEvent += HandleOnRecieve;
            }
        }

        private void HandleOnRecieve(object sender, MidiEventReceivedEventArgs args)
        {
            logger.Trace("RulesManager handle event " + args.Event.ToString());
            var ruleIndex = 0;
            foreach (HandleRule rule in rulesStorage)
            {
                logger.Trace($"handle rule index={ruleIndex} : {rule.GetType().Name}");                
                var matchResult = rule.CheckMatch(this, args.Event);
                logger.Trace($" -- check match = {matchResult}");
                if (matchResult)
                {                    
                    if (rule.Action != null)
                    {
                        logger.Trace($"-- executed linked action: {rule.Action.GetType().Name}");
                        rule.Action.Execute();
                    } else
                    {
                        logger.Trace("-- there is no linked action");
                    }


                    if (rule.StopProcessing)
                    {
                        logger.Trace("-- flag StopProcessing is set. Break.");
                        break;
                    }
                }
                ruleIndex++;
            }
        }

    }
}
