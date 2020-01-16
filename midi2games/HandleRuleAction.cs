using NLog;
using System;
using System.Windows.Forms;

namespace midi2games
{
    public abstract class HandleRuleAction : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Execute action
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Get humanize description of action
        /// </summary>
        /// <returns>String</returns>
        public abstract string GetHumanName();
    }


    public class RuleActionKey : HandleRuleAction
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public string KeysToSend { get; set; }
        public override void Execute()
        {
            EmulateKeyPress(KeysToSend);
            //throw new NotImplementedException();
        }
        public RuleActionKey() { }
        public RuleActionKey(string keys)
        {
            KeysToSend = keys;
        }

        private void EmulateKeyPress(string s)
        {
            SendKeys.SendWait(s);
            logger.Debug("Key press : " + s);
        }

        public override string GetHumanName()
        {
            return "Key press: " + KeysToSend;
        }
    }

    public class RuleActionMouseOffset : HandleRuleAction
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public Orientation Direction { get; set; }
        public int Offset { get; set; }
        public override void Execute()
        {
            Cursor cursor = new Cursor(Cursor.Current.Handle);
            if (Direction == Orientation.Horizontal)
            {
                Cursor.Position = new System.Drawing.Point(Cursor.Position.X + Offset, Cursor.Position.Y);
            }
            else
            {
                Cursor.Position = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y + Offset);
            }
        }
        public RuleActionMouseOffset() { }

        public override string GetHumanName()
        {
            return $"Mouse offset ({Direction.ToString()}, {Offset})";
        }
    }

    public class RuleActionMouseAbs : HandleRuleAction
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public int X { get; set; }
        public int Y { get; set; }
        public override void Execute()
        {
            Cursor cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new System.Drawing.Point(X, Y);
        }
        public RuleActionMouseAbs() { }

        public override string GetHumanName()
        {
            return $"Mouse absolute position ({X}, {Y})";
        }
    }
}
