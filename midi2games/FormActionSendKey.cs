using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace midi2games
{
    public partial class FormActionSendKey : Form, IFormAction
    {
        public FormActionSendKey()
        {
            InitializeComponent();
        }

        RuleActionKey action;

        private void textBoxKeys_TextChanged(object sender, EventArgs e)
        {
            action.KeysToSend = textBoxKeys.Text;
        }

        public void SetAction(HandleRuleAction value)
        {
            action = (RuleActionKey)value;
            textBoxKeys.Text = action.KeysToSend;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Keys modifierKeys = e.Modifiers;

            Keys pressedKey = e.KeyData ^ modifierKeys; //remove modifier keys

            //do stuff with pressed and modifier keys
            var converter = new KeysConverter();
            textBox1.Text = converter.ConvertToString(e.KeyData);
        }
    }
}
