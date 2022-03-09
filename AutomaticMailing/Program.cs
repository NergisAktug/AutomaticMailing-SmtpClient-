using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticMailing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string cs = @"Data Source=NIRVANA\SQLEXPRESS;Initial Catalog=Northwind;Persist Security Info=True;User ID=MailDB;Password=1";
            string sql="Select*from Orders Where OrderDate>= DATEADD(Day, -720, CONVERT(date, SYSDATETIME()))";
            SqlDataAdapter sda=new SqlDataAdapter(sql,cs);
            DataTable dt =new DataTable();
            sda.Fill(dt);

            string mailBody = "";
            foreach (DataRow item in dt.Rows)
            {
                mailBody += item["OrderDate"] + " " + item["CustomerID"];
            }
            MailGonder(mailBody);
        }

        private static void MailGonder(string mailBody)
        {
            MailMessage ePosta=new MailMessage();
            ePosta.From =new MailAddress("nergis.aktug2014@gmail.com");
            ePosta.To.Add("nergis.aktug2020@gmail.com");
            ePosta.Subject = "Last Order";
            ePosta.Body= mailBody;

            //Mail gidecek olan mailin Google Hesabınızı yönetin->Güvenlik->Daha az güvenli uygulama erişimi-> Aç ayarı yapılalıdır.
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential("nergis.aktug2020@gmail.com","######");
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.Send(ePosta);
        
        }
    }
}
