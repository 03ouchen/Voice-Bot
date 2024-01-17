using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis; // go to References clique left and add a system.speech
using System.Speech.Recognition;
using System.Diagnostics;

namespace Voice_Bot
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer s = new SpeechSynthesizer();
        Boolean wake = true;
        Choices list = new Choices();
        public Form1()
        {
            SpeechRecognitionEngine rec = new SpeechRecognitionEngine();
            list.Add(new String[] { "Hello","how are you","what time is it ?" , "what is Today?", "open bing","wake","sleep","restart","update"});
            Grammar gr = new Grammar(new GrammarBuilder(list));

            try
            {
                rec.RequestRecognizerUpdate();
                rec.LoadGrammar(gr);
                rec.SpeechHypothesized +=rec_SpeechHypothesized;
                rec.SetInputToDefaultAudioDevice();
                rec.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch { return; }

            s.SelectVoiceByHints(VoiceGender.Female);
            //s.Speak("hello,my name is robot");
            InitializeComponent();
        }
        public void restart()
        {
            Process.Start(@"D:\Nouveau dossier\vs_professional"); // l'application de vs_professional va etre démarer 
            Environment.Exit(0);
        }
        public void say(String h)
        {
            s.Speak(h);
            textBox2.AppendText(h + "\n");
        }

        private void rec_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            String r = e.Result.Text;
            if (r == "wake")
            {
                wake = true;
                label3.Text = "wake state ";
            }
            if (r == "sleep") { 
                wake = false;
                label3.Text = "wake sleep mode";
            }

            if (wake == true) {

                if (r == "restart" || r == "update")
                {
                    restart();
                }


            if(r == "hello")// what you say 
            {
                say("Hi");// what it says 

                
            }
                
            if (r == "what time is it ?")
            {
                say(DateTime.Now.ToString("h:mm tt"));
            }

            if (r == "what is today ?")
            {
                say(DateTime.Now.ToString("M/d/yyyy"));
            }

            if (r == "how are you ?")// what you say 
            {
                say("I'm fine, what about you");// what it says 

            }
                 
            if (r == "open bing")
            {
                Process.Start("http://bing.com");
            }
            textBox2.AppendText(r + "\n");
            } 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
