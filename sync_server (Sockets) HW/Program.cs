using System.Net.Sockets;
using System.Net;
using System.Text;

namespace sync_server__Sockets__HW
{
    class Program
    {
        static string address = "127.0.0.1";
        static int port = 8080;

        static void Main(string[] args)
        {
            //initialization
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
            UdpClient listener = new UdpClient(ipPoint);

            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Dictionary<string, string> map = new Dictionary<string, string>()
            {
                { "hi", "Hello!" },
                { "how_are_you", "I'm doing well, thank you!" },
                { "what_is_your_name", "My name is Host." },
                { "thank_you", "You're welcome!" },
                { "sorry", "I'm sorry." },
                { "need_help", "How can I help you today?" },
                { "bye", "Goodbye! Have a great day!" },
                { "hello", "Hi there!" },
                { "help", "You can ask about our services or any information you need." },
                { "what_do_you_do", "I am here." },
                { "what_time_is_it", $"Current server time is {DateTime.Now.ToLongTimeString()}" },
                { "tell_me_a_joke", "Why don't scientists trust atoms? Because they make up everything!" },
                { "how_can_i_contact_support", "You can reach our support team at support@example.com." },
                { "what_is_the_weather", "I'm not equipped to provide weather updates, but you can check your favorite weather service." },
                { "where_are_you_located", "I exist in the cloud, so I'm everywhere and nowhere at the same time!" }
            };

            try
            {
                Console.WriteLine("Server started! Waiting for connection...");

                while (true)
                {
                    byte[] data = listener.Receive(ref remoteEndPoint);//wait

                    string msg = Encoding.Unicode.GetString(data);
                    Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {msg} from {remoteEndPoint}");

                    if (map.ContainsKey(msg))
                        data = Encoding.Unicode.GetBytes(map[msg]);
                    else data = Encoding.Unicode.GetBytes("I didn't understand that.");

                    listener.Send(data, data.Length, remoteEndPoint);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            listener.Close();
        }
    }
}
