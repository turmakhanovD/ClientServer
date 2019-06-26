using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace client
{
    class Program
    {
        static int port = 8005; // порт для приема входящих запросов
        static void Main(string[] args)
        {
            // получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            string message = "Вы не выбрали индекс улицы";
            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);

                // начинаем прослушивание
                listenSocket.Listen(10);

                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[256]; // буфер для получаемых данных

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());

                    switch(builder.ToString())
                    {
                        case "1":
                            message = "Самал\nИманова\nИванов";
                            break;
                        case "2":
                            message = "Молдагулова\nПобеда\nГете";
                            break;
                        case "3":
                            message = "Президентская\nДобровольская\nХренова";
                            break;
                        case "4":
                            message = "Маяковская\nЛюблянская\nАкарыс";
                            break;
                        case "5":
                            message = "Масленкова\nМеруерт\n9 мая";
                            break;
                        case "6":
                            message = "Драбовская\nМиленкова\nЗайцева";
                            break;
                        case "7":
                            message = "Нибрана\nУская\nАлександровская";
                            break;
                        case "8":
                            message = "Барева\nИманбаева\nГабдулинна";
                            break;
                        case "0": message = "Зрюкенбергова\nИльинкова\nИванова";
                            break;

                    }
                    // отправляем ответ
                    
                    data = Encoding.Unicode.GetBytes(message);
                    handler.Send(data);
                    // закрываем сокет
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}