using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.Desktop.Communication;
using System.Devices;
using System.IO;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              

namespace Teste
{
    
    public partial class Form1 : Form
    {
        
        RAPI Rapi = new RAPI();
        RemoteDeviceManager remoteDeviceManager;
        RemoteDevice dev;

        string ConfigBase = "C:\\CONTROL\\Dados\\MC75\\ClientConfig.txt";
        string BaseinventBase = "C:\\CONTROL\\Dados\\MC75\\Baseinvent.zip";
        string BaseinventDestino = @"\Program Files\ColetorNet3\invent\Baseinvent.zip";
        string ConfigDestino = @"\Program Files\ColetorNet3\Config\ClientConfig.txt";
        string ColNumber = @"\Program Files\ColetorNet3\Config\ColNumber.txt";
        string arquivos = @"\Program Files\ColetorNet3\invent\Baseinvent.sdf";
        string invcnt = @"\Program Files\ColetorNet3\invent\invcnt.txt";
     
       
    


        public Form1()
        {
            InitializeComponent();
        


        }
        
        public void DeleteFile(string deviceFilePath)
        {
            if (Rapi.DeviceFileExists(deviceFilePath))
            {
                Rapi.DeleteDeviceFile(deviceFilePath);
            }
        }
      
  
       
     
        private void Form1_Load(object sender, EventArgs e)
        {
            try {
                remoteDeviceManager = new RemoteDeviceManager();
                dev = remoteDeviceManager.Devices.FirstConnectedDevice;

                

                if (dev == null)
                {
                    MessageBox.Show("Não Conectado","Coletor:",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    MessageBox.Show("Insira o coletor e Reinicie a Aplicação","ERRO");

                }
                 
                if (Rapi.DevicePresent){
                    var number = RemoteFile.ReadAllText(dev, ColNumber, Encoding.Default);
                    var text = number.Replace("Maquina=", ":");
                    
                    this.label1.Text = text;
                }
                    
            }catch (Exception ex)
            {
               MessageBox.Show("Coletor sem Numero","ERROR:");
                this.label1.Text=ex.Message;
            }
            
           
        }
        
        public void copiar() { 
            if (Rapi.DevicePresent)
            {
                
                Rapi.Connect(true);
                DeleteFile(ConfigDestino);
                DeleteFile(BaseinventDestino);
                DeleteFile(arquivos);
                DeleteFile(invcnt);


                RemoteFile.CopyFileToDevice(dev, ConfigBase, ConfigDestino, true);
                RemoteFile.CopyFileToDevice(dev, BaseinventBase, BaseinventDestino, true);
                this.label2.Text ="CONCLUIDO";
               
                
            }

            else
            {
                MessageBox.Show("Não Foi Possivel Transferir os Arquivos", "Transferencia",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            copiar();
            

        }


        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
