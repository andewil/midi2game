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
    public partial class FormRuleNote : Form, IFormRule
    {
        private HandleRule handleRule;
        public FormRuleNote()
        {
            InitializeComponent();
        }

        public void CancelEdit()
        {
            throw new NotImplementedException();
        }

        public void SaveRule()
        {
            NoteOnRule noteOnRule = handleRule as NoteOnRule;
            if (noteOnRule != null)
            {
                noteOnRule.Note = int.Parse(editNote.Text);
            }
        }

        public void SetRule(HandleRule rule)
        {
            handleRule = rule;
            NoteOnRule noteOnRule = handleRule as NoteOnRule;
            if (noteOnRule != null)
            {
                editNote.Text = noteOnRule.Note.ToString();
            }
        }

        private void editNote_TextChanged(object sender, EventArgs e)
        {
            if (editNote.Text != null)
            {
                int val = 0;
                int.TryParse(editNote.Text, out val);
                handleRule.SetValue("Note", val);
            }
        }
    }
}
