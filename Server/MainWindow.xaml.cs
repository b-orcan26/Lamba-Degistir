using Server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;


namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string mesaj_1 = "ilkDegerMsg";
        public const string mesaj_2 = "lambaDegistirMsg";
        public const string mesaj_3 = "sayiDegistirMsg";
        public const string m_ipAddress = "127.0.0.1";
        public const int m_port = 4000;

        public delegate Boolean LambaHandler();

        static LambaHandler lamba_handler;

        public delegate int SayiHandler();

        static SayiHandler sayi_handler;

        public static List<StreamWriter> writers = new List<StreamWriter>();

        public static Lamba lamba;

        AcceptThread acceptThread;

        public static ConnectedThread connectedThread;

        public MainWindow()
        {
            InitializeComponent();
            lamba = new Lamba();
            this.DataContext = lamba;
            lamba_handler = LambaDurumuDegistir;
            sayi_handler = SayiDegistir;
            acceptThread = new AcceptThread(m_ipAddress,m_port);
            acceptThread.thread.Start();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lamba_handler();
            if (writers.Count > 0)
            {
                connectedThread.TumIstemcilerLambaDegistir();
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            sayi_handler();
            if (writers.Count > 0)
            {
                connectedThread.TumIstemcilerSayiDegistir();
            }
            
        }

        public Boolean LambaDurumuDegistir()
        {
            if (lamba.AcikMi)
            {
                lamba.AcikMi = false;
                return lamba.AcikMi;
            }
            else
            {
                lamba.AcikMi = true;
                return lamba.AcikMi;
            }
        }

        public int SayiDegistir()
        {
            Random rastgele = new Random();
            int sayi = rastgele.Next();
            lamba.Sayi = sayi;
            return lamba.Sayi;
        }

        public class AcceptThread
        {
            public Thread thread;
            private IPAddress ipAddress;
            private TcpListener tcpListener;


            public AcceptThread(String _ipAddress , int _port)
            {

                ipAddress = IPAddress.Parse(_ipAddress);
                tcpListener = new TcpListener(ipAddress, _port);
                thread = new Thread(dinlemeyeBasla);
            }

            public void dinlemeyeBasla()
            {
                Socket istemciSocket = null;
                tcpListener.Start();

                while (true)
                {

                    try
                    {
                        istemciSocket = tcpListener.AcceptSocket();
                    }
                    catch (Exception e)
                    {
                        System.Windows.MessageBox.Show(e.Message);
                        break;
                    }

                    if (istemciSocket != null)
                    {
                        connectedThread = new ConnectedThread(istemciSocket);
                        connectedThread.thread.Start();
                        //İlk deger gonderme
                        Dictionary<string, string> dict = new Dictionary<string, string>();
                        dict.Add("mesaj", mesaj_1);
                        dict.Add("lamba", lamba.AcikMi.ToString());
                        dict.Add("sayi", lamba.Sayi.ToString());

                        string s = string.Join(";", dict.Select(x => x.Key + "=" + x.Value).ToArray());
                        connectedThread.Yaz(s);

                        // System.Windows.MessageBox.Show("istemci baglandi");
                        istemciSocket = null;
                    }

                }
            }


        }


        public class ConnectedThread
        {
            private Socket socket;
            private NetworkStream ag;
            public StreamWriter yazici;
            private StreamReader okuyucu;
            public Thread thread;

            public ConnectedThread(Socket istemciSocket)
            {
                socket = istemciSocket;           
                ag = new NetworkStream(socket);
                yazici = new StreamWriter(ag);
                okuyucu = new StreamReader(ag);
                thread = new Thread(OkumayaBasla);
            }

            public void OkumayaBasla()
            {
                writers.Add(yazici);

                while (true)
                {
                    try
                    {
                        string yazi = okuyucu.ReadLine();
                        if (yazi.Length > 0)
                        {
                            Dictionary<string, string> dict = (yazi.Contains(';')) ? yazi.Split(';')
                                        .Select(item => item.Split('='))
                                        .ToDictionary(key => key[0].Trim(), value => value[1].Trim()) :
                                        yazi.Split(' ').Select(x => x.Split('=')).ToDictionary(x => x[0].Trim(), y => y[1].Trim());

                            String gelenMesaj = dict["mesaj"];

                            if (String.Compare(gelenMesaj, mesaj_2) == 0)
                            {
                                //lamba durumu degistir
                                lamba_handler();
                                TumIstemcilerLambaDegistir();
                                yazi = "";
                            }
                            else if (String.Compare(gelenMesaj, mesaj_3) == 0)
                            {
                                //Sayi degistir
                                sayi_handler();
                                TumIstemcilerSayiDegistir();
                                yazi = "";
                            }
                        }

                    }

                    catch (Exception e)
                    {
                        System.Windows.MessageBox.Show(e.Message);
                    }
                }
            }

            public void Yaz(String ileti)
            {
                yazici.WriteLine(ileti);
                yazici.Flush();
            }

            public void TumIstemcilerLambaDegistir()
            {
                Dictionary<string, string> hash = new Dictionary<string, string>();
                hash.Add("mesaj", mesaj_2);
                hash.Add("lamba", lamba.AcikMi.ToString());
                string s = string.Join(";", hash.Select(x => x.Key + "=" + x.Value).ToArray());

                foreach (var writer in writers)
                {
                    writer.WriteLine(s);
                    writer.Flush();
                }

            }

            public void TumIstemcilerSayiDegistir()
            {
                Dictionary<string, string> hash = new Dictionary<string, string>();
                hash.Add("mesaj", mesaj_3);
                hash.Add("sayi", lamba.Sayi.ToString());
                string s = string.Join(";", hash.Select(x => x.Key + "=" + x.Value).ToArray());

                foreach (var writer in writers)
                {
                    writer.WriteLine(s);
                    writer.Flush();
                }

            }


        }

       
    }
}
