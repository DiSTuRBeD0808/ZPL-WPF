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

    private void NSIsSerialized_Checked(object sender, RoutedEventArgs e)
    {
      if (NSisSerialized.IsChecked == true)
      {
        NSserialNoField.IsEnabled = true;
        NSserialNoField.IsEnabled = true;
      }
      else
      {
        NSserialNoField.IsEnabled = false;
        NSserialNoField.IsEnabled = false;
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

    private void Qty_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    public void Print_Click(object sender, RoutedEventArgs e)
    {
      string matDoc = matDocField.Text;
      string lineItem = lineItemField.Text;
      string matNo = matNoField.Text;
      string matDesc = matDescField.Text;
      string qty = quantityField.Text;
      

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

        string ipAddress = ipAddressField.Text;

        int port = 9100;

        // ZPL Command(s)

        string ZPLString =

        ("^XA^FS^FT10,70^XGE:LOGO.GRF,1,1^FS" + "^FT320,70^A0N,75,75^FH\\^FN1^FD" +/*Material Number*/ matNo + "^FS^FT25,130^A0N,55,55^FH\\^FN2^FD" + /* Material Description*/matDesc + "^FS^MMT^LH0,0^CI0^BY110,110^FT26,300^BXN,5,200,24,24,1^FH\\^FN3^FD" + /*barcode*/ barcode + "^FS^FT214,230^A0N,43,44^FH\\^FDDoc:^FS" + "^FT320,230^A0N,43,44^FH\\^FN5^FD" + /*Material Document*/ matDoc+ '-' + lineItem + "^FS^FT196,280^A0N,43,44^FH\\^FDS.Nr:^FS^FT320,280^A0N,43,44^FH\\^FN7^FD" + /*Serial Number*/ serialNoField.Text + "^FS^FT200,330^A0N,43,44^FH\\^FDQty:^FS^FT320,330^A0N,43,44^FH\\^FN11^FD" + /*Quantity*/ qty + "^FS^FO320,580^GB120,120,4^FS^XZ");

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

    public void NSPrint_Click(object sender, RoutedEventArgs e)
    {
      {
        string NSmatDoc = NSmatDocField.Text;
        string NSlineItem = NSlineItemField.Text;
        string NSmatNo = NSmatNoField.Text;
        string NSmatDesc = NSmatDescField.Text;
        string NSqty = NSquantityField.Text;


        string NSbarcode = "NS" + NSmatDoc + "-" + NSlineItem + "-" + NSmatNo + "*[SN:" + NSserialNoField.Text + "]";

        MessageBoxResult NSlabelResult = MessageBox.Show(NSbarcode);

        print_zpl(NSbarcode);


        void print_zpl(string label)
        {
          if (string.IsNullOrEmpty(label))
          {
            throw new ArgumentException("Please enter valid barcode data.", nameof(label));
          }
          // Printer IP Address and communication port

          string ipAddress = ipAddressField.Text;

          int port = 9100;

          // ZPL Command(s)

          string NSZPLString =

          ("^XA^FS^FT220,70^A0N,70,70^FH\\^FN1^FD" + NSmatNo + "^FS^FT26,110^A0N,43,44^FH\\^FD" + NSmatDesc + "^FS^MMT^LH0,0^CI0^BY110,110^FT26,237^BXN,5,200,22,22,1^FH\\^FN5^FD" + NSbarcode + "^FS^FT274,150^A0N,43,44^FH\\^FDNonStock^FS^FT256,290^A0N,43,44^FH\\^FN3^FDQuantity: " + NSqty + "^FS^FT258,200^A0N,43,44^FH\\^FDDoc:^FS^FT350,200^A0N,43,44^FH\\^FN8^FDNS" + NSmatDoc + "-" + NSlineItem + "^FS^FT256,250^A0N,43,44^FH\\^FDS.Nr:^FS^FT350,250^A0N,43,44^FH\\^FN2^FD" + NSserialNoField.Text + "^FS");
          try

          {

            // Open connection

            System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();

            client.Connect(ipAddress, port);

            // Write ZPL String to connection

            System.IO.StreamWriter writer =

        new System.IO.StreamWriter(client.GetStream());

            writer.Write(NSZPLString);

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

    private void PresNoField_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void PlineItemField_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void PmatNoField_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void PmatDescField_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void PqtyField_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void Pprint_Click(object sender, RoutedEventArgs e)
    {
      {
        string PreservationNo = PreservationField.Text;
        string PlineItem = PlineItemField.Text;
        string PmatNo = PmatNoField.Text;
        string PmatDesc = PmatDescField.Text;
        string Pquantity = PquantityField.Text;


        string Pbarcode = "P" + PreservationNo + "-" + PlineItem + "-" + PmatNo;

        MessageBoxResult PlabelResult = MessageBox.Show(Pbarcode);

        print_zpl(Pbarcode);


        void print_zpl(string label)
        {
          if (string.IsNullOrEmpty(label))
          {
            throw new ArgumentException("Please enter valid barcode data.", nameof(label));
          }
          // Printer IP Address and communication port

          string ipAddress = ipAddressField.Text;

          int port = 9100;

          // ZPL Command(s)

          string PZPLString =

          ("^XA^FS^FT170,70^A0N,45,45^FH\\^FN1^FD" + PmatNo + "^FS^FT30,130^A0N,35,35^FH\\^FN2^FD" + PmatDesc + "^FS^MMT^LH0,0^CI0^BY110,110^FT46,270^BXN,5,200,22,22,1^FH\\^FN3^FD" + Pbarcode + "^FS^FT335,185^A0N,23,24^FH\\^FDRes:^FS^FT400,185^A0N,25,24^FH\\^FN4^FD" + PreservationNo + "-" + PlineItem + "^FS^FT338,205^A0N,23,24^FH\\^FDQty:^FS^FT400,205^A0N,25,24^FH\\^FN10^FD" + Pquantity + "^FS^XZ");
          try

          {

            // Open connection

            System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();

            client.Connect(ipAddress, port);

            // Write ZPL String to connection

            System.IO.StreamWriter writer =

        new System.IO.StreamWriter(client.GetStream());

            writer.Write(PZPLString);

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
}
