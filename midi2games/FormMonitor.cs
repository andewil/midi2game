using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
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
    public partial class FormMonitor : Form
    {
        private DataTable eventsData = new DataTable();
        private MidiHandler midiHandler;

        public FormMonitor()
        {
            InitializeComponent();
            //SetStyle(
            //    ControlStyles.DoubleBuffer |
            //    ControlStyles.UserPaint |
            //    ControlStyles.AllPaintingInWmPaint, true);
            //UpdateStyles();
            InitDataSet();            
        }

        private void InitDataSet()
        {
            this.dataGridView1.CellValueNeeded += new
                DataGridViewCellValueEventHandler(dataGridView1_CellValueNeeded);
            if (dataGridView1.RowCount > 0)
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            //dataGridView1.DataSource = eventsData;
        }        
        
        public void SetHandler(MidiHandler handler)
        {
            if (midiHandler != null)
                midiHandler.OnRecieveEvent -= HandleOnRecieve;
            midiHandler = handler;
            if (midiHandler != null)
                midiHandler.OnRecieveEvent += HandleOnRecieve;
        }

        private const string nv = "--";
        private List<MidiMonitorRecord> eventStore = new List<MidiMonitorRecord>();

        private void HandleOnRecieve(object sender, MidiEventReceivedEventArgs args)
        {
            //AddDataRecord(sender, args);
            MidiMonitorRecord r = new MidiMonitorRecord(DateTime.Now, args.Event);
            eventStore.Add(r);
            //dataGridView1.RowCount = eventStore.Count;
            //SetRowCount(eventStore.Count);
            return;

            switch (args.Event.EventType)
            {
                case MidiEventType.ControlChange:
                    var cce = (ControlChangeEvent)args.Event;
                    AddGridRow(
                            DateTime.Now.ToString("HH:mm:ss:fff"),
                            "Control change",
                            cce.Channel,
                            cce.ControlNumber,
                            cce.ControlValue,
                            nv,
                            nv,
                            nv
                        );
                    break;
                case MidiEventType.ProgramChange:
                    var pce = (ProgramChangeEvent)args.Event;
                    AddGridRow(
                            DateTime.Now.ToString("HH:mm:ss:fff"),
                            "Program change",
                            pce.Channel,
                            nv,
                            nv,
                            pce.ProgramNumber,
                            nv,
                            nv
                        );
                    break;
                case MidiEventType.NoteOn:
                    var noe = (NoteOnEvent)args.Event;
                    AddGridRow(
                            DateTime.Now.ToString("HH:mm:ss:fff"),
                            "NoteOn",
                            noe.Channel,
                            nv,
                            nv,
                            nv,
                            noe.NoteNumber,
                            noe.Velocity                            
                        );
                    break;
                case MidiEventType.NoteOff:
                    var nOffEvevent = (NoteOffEvent)args.Event;
                    AddGridRow(
                            DateTime.Now.ToString("HH:mm:ss:fff"),
                            "NoteOff",
                            nOffEvevent.Channel,
                            nv,
                            nv,
                            nv,
                            nOffEvevent.NoteNumber,
                            nOffEvevent.Velocity
                            );
                    break;
            }
        }

        public FormMidi2Game formMain;
        //private delegate void SafeCallAddListItem(ListViewItem item);
        private delegate void SafeCallAddGridRow(object[] values);
        private delegate void SafeCallSetRowCount(int rowCount);
        //private void AddListItem(ListViewItem item)
        //{
        //    if (listViewMidiEvents.InvokeRequired)
        //    {
        //        var d = new SafeCallAddListItem(AddListItem);
        //        listViewMidiEvents.Invoke(d, new object[] { item } );
        //    }
        //    else
        //    {
        //        listViewMidiEvents.Items.Add(item);
        //    }
        //}

        private void AddGridRow(params object[] values)
        {
            if (dataGridView1.InvokeRequired)
            {
                var d = new SafeCallAddGridRow(AddGridRow);
                dataGridView1.Invoke(d, new object[] { values });
            }
            else
            {
                dataGridView1.Rows.Add(values);
                //dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            }
        }

        private void SetRowCount(int rowCount)
        {
            if (dataGridView1.InvokeRequired)
            {
                var d = new SafeCallSetRowCount(SetRowCount);
                dataGridView1.Invoke(d, new object[] { rowCount });
            }
            else
            {
                dataGridView1.RowCount = rowCount;
                if (dataGridView1.RowCount > 0)
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            }
        }

        private void FormMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetHandler(null);
            if (formMain != null)
                formMain.formMonitor = null;
        }

        private void dataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex >= eventStore.Count) return;
            MidiMonitorRecord eventInfo = eventStore[e.RowIndex];

            // Set the cell value to paint
            // first row is always RowIndex
            if (e.ColumnIndex > 0)
            {
                e.Value = GetStringDataByColumnIndex(e.ColumnIndex, eventInfo);  
            } else
            {
                e.Value = e.RowIndex;
            }
            
        }

        private void PaintRow(int rowIndex, Color clr)
        {
            for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
            {
                dataGridView1.Rows[rowIndex].Cells[i].Style.BackColor = clr;
            }
        }

        string GetStringDataByColumnIndex(int columnIndex, MidiEventReceivedEventArgs args)
        {
            string c1 = DateTime.Now.ToString("HH:mm:ss:fff");
            string c2;
            string c3;
            string c4;
            string c5;
            string c6;
            string c7;
            string c8;

            switch (args.Event.EventType)
            {                
                case MidiEventType.ControlChange:
                    var cce = (ControlChangeEvent)args.Event;
                    c2 = "Control change";
                    c3 = cce.Channel.ToString();
                    c4 = cce.ControlNumber.ToString();
                    c5 = cce.ControlValue.ToString();
                    c6 = nv;
                    c7 = nv;
                    c8 = nv;
                    break;
                case MidiEventType.ProgramChange:
                    var pce = (ProgramChangeEvent)args.Event;
                    c2 = "Program change";
                    c3 = pce.Channel.ToString();
                    c4 = nv;
                    c5 = nv;
                    c6 = pce.ProgramNumber.ToString();
                    c7 = nv;
                    c8 = nv;
                    break;
                case MidiEventType.NoteOn:
                    var noe = (NoteOnEvent)args.Event;
                    c2 = "NoteOn";
                    c3 = noe.Channel.ToString();
                    c4 = nv;
                    c5 = nv;
                    c6 = nv;
                    c7 = noe.NoteNumber.ToString();
                    c8 = noe.Velocity.ToString();
                    break;
                case MidiEventType.NoteOff:
                    var nOffEvevent = (NoteOffEvent)args.Event;
                    c2 = "NoteOff";
                    c3 = nOffEvevent.Channel.ToString();
                    c4 = nv;
                    c5 = nv;
                    c6 = nv;
                    c7 = nOffEvevent.NoteNumber.ToString();
                    c8 = nOffEvevent.Velocity.ToString();
                    break;
                default:
                    c2 = nv;
                    c3 = nv;
                    c4 = nv;
                    c5 = nv;
                    c6 = nv;
                    c7 = nv;
                    c8 = nv;
                    break;
            }

            if (columnIndex == 1) { return c1; }
            else if (columnIndex == 2) { return c2; }
            else if (columnIndex == 3) { return c3; }
            else if (columnIndex == 4) { return c4; }
            else if (columnIndex == 5) { return c5; }
            else if (columnIndex == 6) { return c6; }
            else if (columnIndex == 7) { return c7; }
            else if (columnIndex == 8) { return c8; }
            else return nv;
        }

        string GetStringDataByColumnIndex(int columnIndex, MidiMonitorRecord rec)
        {           
            if (columnIndex == 1) { return rec.eventDate.ToString(); }
            else if (columnIndex == 2) { return rec.eventType; }
            else if (columnIndex == 3) { return rec.channel.ToString(); }
            else if (columnIndex == 4) { return rec.controlNumber.ToString(); }
            else if (columnIndex == 5) { return rec.controlValue.ToString(); }
            else if (columnIndex == 6) { return rec.programNumber.ToString(); }
            else if (columnIndex == 7) { return rec.noteNumber.ToString(); }
            else if (columnIndex == 8) { return rec.noteVelocity.ToString(); }
            else return nv;
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            tsEventCount.Text = eventStore.Count.ToString();
            if (eventStore.Count == 0)
                return;
            if (eventStore.Count != dataGridView1.RowCount)
                SetRowCount(eventStore.Count);
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            eventStore.Clear();
            SetRowCount(0);
        }

    }

    public class MidiMonitorRecord
    {
        public DateTime eventDate;
        public string eventType;
        public int channel;
        public int programNumber;
        public int controlNumber;
        public int controlValue;
        public int noteNumber;
        public int noteVelocity;

        public MidiMonitorRecord(DateTime dt)
        {
            eventDate = dt;
        }

        public MidiMonitorRecord(DateTime dt, MidiEvent ev) {
            eventDate = dt;
            SetEvent(ev);
        }

        public void SetEvent(MidiEvent ev)
        {
            switch (ev.EventType)
            {
                case MidiEventType.ControlChange:
                    var cce = (ControlChangeEvent)ev;
                    eventType = "Control change";
                    channel = cce.Channel;
                    controlNumber = cce.ControlNumber;
                    controlValue = cce.ControlValue;
                    break;
                case MidiEventType.ProgramChange:
                    var pce = (ProgramChangeEvent)ev;
                    eventType = "Program change";
                    channel = pce.Channel;
                    programNumber = pce.ProgramNumber;
                    break;
                case MidiEventType.NoteOn:
                    var noe = (NoteOnEvent)ev;
                    eventType = "NoteOn";
                    channel = noe.Channel;
                    noteNumber = noe.NoteNumber;
                    noteVelocity = noe.Velocity;
                    break;
                case MidiEventType.NoteOff:
                    var nOffEvevent = (NoteOffEvent)ev;
                    eventType = "NoteOn";
                    channel = nOffEvevent.Channel;
                    noteNumber = nOffEvevent.NoteNumber;
                    noteVelocity = nOffEvevent.Velocity;
                    break;
            }
        }
    }
}
