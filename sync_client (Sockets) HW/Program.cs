using System.Net.Sockets;
using System.Net;
using System.Text;

namespace sync_client__Sockets__HW
{
    class Program
    {
        static string address = "127.0.0.1";
        static int port = 8080;

        static void Main(string[] args)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                IPEndPoint remoteIpPoint = new IPEndPoint(IPAddress.Any, 0);

                UdpClient client = new UdpClient();

                string message = "";
                while (message != "end")
                {
                    Console.Write("Enter a message: ");
                    message = Console.ReadLine();
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    client.Send(data, data.Length, ipPoint);


                    data = new byte[1024];
                    data = client.Receive(ref remoteIpPoint);
                    string response = Encoding.Unicode.GetString(data);

                    Console.WriteLine("server response: " + response);
                }
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
