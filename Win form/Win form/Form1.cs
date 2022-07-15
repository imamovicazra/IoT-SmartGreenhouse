using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace Azra_IOT
{
    public partial class Form1 : Form
    {

        public class dataFbase
        {
            public string dan { get; set; }
            public string vlaznost { get; set; }
            public string temperatura { get; set; }
            public string intenz { get; set; }

        }

        public SerialPort myport;
        public Form1()
        {
            InitializeComponent();
            myport = new SerialPort();



            myport.PortName = "COM3";
            myport.BaudRate = 9600;
            myport.DtrEnable = true;
            myport.Open();

            myport.DataReceived += serialPort1_DataReceived;
            client = new FireSharp.FirebaseClient(config);
        }

        IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret = "oyQi9m3hBj74NTxlR0oj9LReiUb0H7YfmXKeUp31",
            BasePath = "https://iot-azra-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        IFirebaseClient client;

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string line = myport.ReadLine();
            this.BeginInvoke(new LineReceivedEvent(LineReceived), line);
        }

        private delegate void LineReceivedEvent(string line);
        private void LineReceived(string portporuka)
        {
            var fdata = new dataFbase();
            /*
             Dan/noc:1
Vlaznost zraka: 40.00
Temperatura: 29.70C - 85.46F

             */
            if (portporuka.Contains("Dan"))
            {
                int dan = Int32.Parse(portporuka.Substring(portporuka.Length-2,1));
                if (dan == 1)
                {
                    lblDan.Text = "Napolju je noc!";
                }
                else
                {
                    lblDan.Text = "Napolju je dan!";
                }
            }
            if (portporuka.Contains("Vlaznost"))
            {
                lblTemperatura.Text = portporuka;
            }
            if (portporuka.Contains("Intenzitet"))
            {
                label2.Text = portporuka;
            }
            if (portporuka.Contains("Temperatura"))
            {
                lblVlaznost.Text = portporuka;
            }

            fdata.dan = lblDan.Text;
            fdata.temperatura = lblTemperatura.Text;
            fdata.vlaznost = lblVlaznost.Text;
            fdata.intenz = label2.Text;
            //client.Push("/", fdata);
            client.UpdateAsync("", fdata);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
       
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}


