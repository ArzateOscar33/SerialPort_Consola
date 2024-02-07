using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaClaseCOM
{
    public class Program
    {
        //private string[] ports = SerialPort.GetPortNames();
        //string COM = "COM4";
        //ValidacionCOM vCOM = new ValidacionCOM("COM4");
        static void Main(string[] args)
        {
            // Crear una instancia de ValidacionCOM con el nombre del puerto COM
            ValidacionCOM validacionCOM = new ValidacionCOM("COM5");

            // Suscribirse al evento DataReceived
            validacionCOM.DataReceived += OnDataReceived;

            // Abrir la conexión
            validacionCOM.OpenConnection();

            Console.WriteLine("Presiona cualquier tecla para cerrar la conexión.");
            Console.ReadKey();

            // Cerrar la conexión
            validacionCOM.CloseConnection();
        }

        static void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            // Acceder al comando recibido
            string receivedCommand = e.ReceivedCommand;
            Console.WriteLine($"Datos recibidos: {receivedCommand}");
        }
    }
}