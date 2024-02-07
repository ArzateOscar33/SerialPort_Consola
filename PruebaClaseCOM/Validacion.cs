using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;


public class ValidacionCOM
{
    private SerialPort serialPort;

    public event EventHandler<DataReceivedEventArgs> DataReceived;

    public ValidacionCOM(string portName)
    {
        // Constructor que recibe el nombre del puerto COM
        InitializeSerialPort(portName);
    }

    public void OpenConnection()
    {
        try
        {
            // Intentar abrir el puerto serie
            serialPort.Open();
            Console.WriteLine("Conexión establecida.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al abrir el puerto serie: {ex.Message}");
        }
    }

    public void CloseConnection()
    {
        // Cerrar el puerto serie
        if (serialPort.IsOpen)
        {
            serialPort.Close();
            Console.WriteLine("Conexión cerrada.");
        }
    }

    private void InitializeSerialPort(string portName)
    {
        // Inicializar el objeto SerialPort con las propiedades necesarias
        serialPort = new SerialPort();
        serialPort.PortName = portName;
        serialPort.BaudRate = 115200;
        serialPort.DataBits = 8;
        serialPort.StopBits = StopBits.One;
        serialPort.Parity = Parity.None;

        // Manejar el evento DataReceived internamente
        serialPort.DataReceived += SerialPort_DataReceived;
    }

    private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        // Leer el comando recibido
        string receivedData = serialPort.ReadExisting();

        // Invocar el evento personalizado DataReceived con el comando
        DataReceived?.Invoke(this, new DataReceivedEventArgs(receivedData));
    }
}

public class DataReceivedEventArgs : EventArgs
{
    public string ReceivedCommand { get; }

    public DataReceivedEventArgs(string receivedCommand)
    {
        ReceivedCommand = receivedCommand;
    }
}
