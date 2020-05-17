using Server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

        public partial class MainWindow : Window
        {

            public const string mesaj_1 = "ilkDegerMsg";
            public const string mesaj_2 = "lambaDegistirMsg";
            public const string mesaj_3 = "sayiDegistirMsg";
            public const string my_ipAdress = "127.0.0.1";
            public const int my_port = 4000;
            readonly ConnectThread connectThread;

            public delegate void LambaHandler(Boolean durum);
            static LambaHandler lamba_handler;
            public delegate void SayiHandler(int sayi);
            static SayiHandler sayi_handler;
            Lamba lamba;

            public MainWindow()
            {
                InitializeComponent();

                connectThread = new ConnectThread(my_ipAdress, my_port);
                connectThread.Baglan();
                lamba = new Lamba();
                this.DataContext = lamba;
                lamba_handler = LambaDegistir;
                sayi_handler = SayiDegistir;


                this.DataContext = lamba;
            }

            private void LambaDegistir_Click(object sender, RoutedEventArgs e)
            {

                connectThread.Yaz(mesaj_2);
            }

            private void SayiDegistir_Click(object sender, RoutedEventArgs e)
            {

                connectThread.Yaz(mesaj_3);
            }

            public void LambaDegistir(Boolean _durum)
            {
                lamba.AcikMi = _durum;
            }

            public void SayiDegistir(int sayi)
            {
                lamba.Sayi = sayi;
            }


            public class ConnectThread
            {
                public Thread thread;
                String ipAdress;
                int port;
                TcpClient tcpClient;
                NetworkStream ag;
                StreamReader okuyucu;
                StreamWriter yazici;

                public ConnectThread(String _ipAdress, int _port)
                {
                    ipAdress = _ipAdress;
                    port = _port;
                    thread = new Thread(Baglan);
                }

                public void Baglan()
                {
                    try
                    {
                        tcpClient = new TcpClient(ipAdress, port);
                        ag = tcpClient.GetStream();
                        okuyucu = new StreamReader(ag);
                        yazici = new StreamWriter(ag);
                        thread.Interrupt();
                        thread = new Thread(Okumayabasla);
                        thread.Start();

                    }
                    catch (Exception e)
                    {
                        System.Windows.MessageBox.Show("Baglanamadi");
                    }


                }

                public void Okumayabasla()
                {


                    while (true)
                    {
                        try
                        {
                            string yazi = okuyucu.ReadLine();
                            if (yazi.Length > 0)
                            {
                                Dictionary<string, string> dict = yazi.Split(';')
                                        .Select(item => item.Split('='))
                                        .ToDictionary(key => key[0].Trim(), value => value[1].Trim());
                                String mesaj = dict["mesaj"];

                                if (String.Compare(mesaj, mesaj_1) == 0)
                                {
                                    //ilk degerler alindi ve lamba durumu degistirildi


                                    Boolean _durum = Boolean.Parse(dict["lamba"]);
                                    int _sayi = int.Parse(dict["sayi"]);

                                    lamba_handler(_durum);
                                    sayi_handler(_sayi);
                                    okuyucu.DiscardBufferedData();
                                    yazi = "";
                                }
                                else if (String.Compare(mesaj, mesaj_2) == 0)
                                {
                                    //Sadece lambanin durumu degistiginde
                                    Boolean _durum = Boolean.Parse(dict["lamba"]);
                                    lamba_handler(_durum);
                                    yazi = "";
                                }
                                else if (String.Compare(mesaj, mesaj_3) == 0)
                                {
                                    // Sadece sayinin degeri degistiginde
                                    int _sayi = int.Parse(dict["sayi"]);
                                    sayi_handler(_sayi);
                                    yazi = "";
                                }

                            }
                        }
                        catch (Exception e)
                        {
                            System.Windows.MessageBox.Show(e.Message + "bsbs");
                        }
                    }
                }

                public void Yaz(String ileti)
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("mesaj", ileti);
                    string s = string.Join(";", dict.Select(x => x.Key + "=" + x.Value).ToArray());
                    yazici.WriteLine(s);
                    yazici.Flush();

                }

            }


        }
    }

