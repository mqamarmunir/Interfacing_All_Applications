namespace TestService
{
    partial class Service1
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort3 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort4 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort5 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort6 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort7 = new System.IO.Ports.SerialPort(this.components);
            // 
            // serialPort1
            // 
           // this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // serialPort2
            // 
            this.serialPort2.PortName = "COM8";
            this.serialPort2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort2_DataReceived);
            // 
            // serialPort3
            // 
            this.serialPort3.PortName = "COM8";
            this.serialPort3.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort3_DataReceived);
            // 
            // serialPort4
            // 
            this.serialPort4.PortName = "COM8";
            this.serialPort4.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort4_DataReceived);
            // 
            // serialPort5
            // 
            this.serialPort5.PortName = "COM8";
            this.serialPort5.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort5_DataReceived);
            // 
            // serialPort6
            // 
            this.serialPort6.PortName = "COM8";
            this.serialPort6.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort6_DataReceived);
            // 
            // serialPort7
            // 
            this.serialPort7.PortName = "COM8";
            this.serialPort7.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort7_DataReceived);
            // 
            // Service1
            // 
            this.ServiceName = "SerialReader_Trees";

        }

        #endregion
        private System.IO.Ports.SerialPort serialPort1;
        private System.IO.Ports.SerialPort serialPort2;
        private System.IO.Ports.SerialPort serialPort3;
        private System.IO.Ports.SerialPort serialPort4;
        private System.IO.Ports.SerialPort serialPort5;
        private System.IO.Ports.SerialPort serialPort6;
        private System.IO.Ports.SerialPort serialPort7;
    }
}
