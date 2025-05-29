using Haukcode.HighResolutionTimer;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlockStudioCS {
    public partial class Form1: Form {

        Dictionary<int, Dictionary<int, NoteBlock>> notes = new Dictionary<int, Dictionary<int, NoteBlock>>();
        List<Layer> layers = new List<Layer>();

        List<int> maxIndex = new List<int>();

        NBSLoader loader = new NBSLoader();

        Pen penLightGray = new Pen(Color.FromArgb(200, 200, 200));
        Pen penGray1 = new Pen(Color.FromArgb(100, 100, 100));
        Pen penGray2 = new Pen(Color.FromArgb(70, 70, 70));
        SolidBrush brushLightGray = new SolidBrush(Color.FromArgb(200, 200, 200));
        SolidBrush brushGray = new SolidBrush(Color.FromArgb(60, 60, 60));
        SolidBrush brushSelected = new SolidBrush(Color.FromArgb(50, 200, 200, 200));
        SolidBrush brushKeySelected = new SolidBrush(Color.FromArgb(100, 100, 100, 200));
        SolidBrush brushDarken = new SolidBrush(Color.FromArgb(150, 30, 30, 30));

        StringFormat centered = new StringFormat();

        Dictionary<Keys, bool> keyStates = new Dictionary<Keys, bool>();

        Point LayersMouse = new Point(-1, -1);

        delegate void TickHandler();

        int farthestNoteX = 0;
        int farthestNoteY = 0;
        int totalNotes = 0;

        private int _playbackPosition;
        int playbackPosition {
            get { return _playbackPosition; }
            set {
                _playbackPosition = value;
                tbx_Position.Text = $"{(_playbackPosition / 16) + 1}, {((_playbackPosition / 4) % 4) + 1}, {(_playbackPosition % 4) + 1}";
                // SongTempo = ticks per second
                double secPerTick = 100.0 / SongTempo;
                double secIntoSong = playbackPosition * secPerTick;

                // total whole seconds elapsed
                int totalSeconds = (int)Math.Floor(secIntoSong);
                // hours component
                int hours = totalSeconds / 3600;
                // minutes component
                int minutes = (totalSeconds / 60) % 60;
                // seconds component (0–59)
                int seconds = totalSeconds % 60;
                // fractional part of a second, turned into two-digit hundredths
                int hundredths = (int)((secIntoSong - totalSeconds) * 100);

                // format as MM:SS.hh
                lbl_SongCurrentTime.Text = $"{hours:00}:{minutes:00}:{seconds:00}.{hundredths:00}";

            }
        }

        int volume;
        object threadlock = new object();
        short SongTempo = 1000;

        int MinutesSpent = 0;
        int LeftClicks = 0;
        int RightClicks = 0;
        int NoteBlocksAdded = 0;
        int NoteBlocksRemoved = 0;
        System.Windows.Forms.Timer MinuteTimer = new System.Windows.Forms.Timer();

        string SongName = "";
        string SongAuthor = "";
        string SongOriginalAuthor = "";
        string SongDescription = "";
        string MidiSchematicFileName = "";

        sbyte Looping = 0;
        sbyte MaxLoopCount = 0;
        sbyte TimeSignature = 4;
        short LoopStartTick = 0;

        bool playing = false;

        Stopwatch sw = Stopwatch.StartNew();

        Random rnd = new Random();

        HighResolutionTimer timer = new HighResolutionTimer();

        public enum ins : byte {
            harp = 0,
            dbass = 1,
            bdrum = 2,
            snare = 3,
            click = 4,
            guitar = 5,
            flute = 6,
            bell = 7,
            chime = 8,
            xyl = 9,
            ironxyl = 10,
            cowbell = 11,
            didgeridoo = 12,
            bit = 13,
            banjo = 14,
            pling = 15
        }

        ins instrumentSelected = ins.harp;
        sbyte keySelected = 45;

        int displayWidth = 16;
        int displayHeight = 7;

        System.Windows.Forms.Timer ClearSoundBuffer = new System.Windows.Forms.Timer();

        private readonly SynchronizationContext _uiContext;
        private readonly Action _postTick;

    }
}
