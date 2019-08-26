using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ZPL_Printer
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    private void IsSerialized_Checked(object sender, RoutedEventArgs e)
    {
      if (isSerialized.IsChecked == true)
      {
        serialNoField.IsEnabled = true;
        serialNoField.IsEnabled = true;
      }
      else
      {
        serialNoField.IsEnabled = false;
        serialNoField.IsEnabled = false;
      }
    }

    public void MatDoc_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    public void LineItem_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void MatNo_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void SerialNumber_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    public void Print_Click(object sender, RoutedEventArgs e)
    {
      string matDoc = matDocField.Text;
      string lineItem = lineItemField.Text;
      string matNo = matNoField.Text;
      

      string barcode = matDoc + "-" + lineItem + "-" + matNo + "*[SN:" + serialNoField.Text + "]";

      MessageBoxResult labelResult = MessageBox.Show(barcode);

      print_zpl(barcode);


      void print_zpl(string label)
      {
        if (string.IsNullOrEmpty(label))
        {
          throw new ArgumentException("Please enter valid barcode data.", nameof(label));
        }
        // Printer IP Address and communication port

        string ipAddress = "127.0.0.1";

        int port = 9100;

        // ZPL Command(s)

        string ZPLString =

            "^XA" +

            "^FO50,50" +

            "^A0N50,50" +

            "^FDHello, World!^FS" +

            "^XZ";

        try

        {

          // Open connection

          System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();

          client.Connect(ipAddress, port);

          // Write ZPL String to connection

          System.IO.StreamWriter writer =

      new System.IO.StreamWriter(client.GetStream());

          writer.Write(ZPLString);

          writer.Flush();

          // Close Connection

          writer.Close();

          client.Close();

        }

        catch (Exception ex)

        {

          // Catch Exception

        }
      }
    }
  }
}
