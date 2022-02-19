using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WebTMDTLibrary.Hepler
{
    public class EmailHelper
    {
        public string site = "https://localhost:7094/";
        public string siteOnline = "https://localhost:7094/";
        public string SendEmailConfirm(string userEmail, string token, string username)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("timelive.circleqm@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Xác thực email cho tài khoản của bạn";
            mailMessage.IsBodyHtml = true;

            string encodetk = token.Replace("+", "%2B"); ;
            string link = site + "account/confirm?email=" + userEmail + "&token=" + encodetk;
            string linkOnline = siteOnline + "account/confirm?email=" + userEmail + "&token=" + encodetk;

            mailMessage.Body = "<!DOCTYPE html><html><head> <title></title> <meta charset='utf-8' /> " +
                "<style> table, th, td { border: 1px solid black; } </style></head><body style='font-family: monospace;'>" +
                " <br /> <table width='100%'> <tr> <td style='background-color:#97b6e4;text-align: center;'> " +
                "<img src='https://res.cloudinary.com/dkmk9tdwx/image/upload/v1628192627/logo_v5ukvv.png' " +
                "style='width: 45px;height: 45px'> <h1 >Circle" + "'s" + " Shop</h1> </td> </tr> <tr> <td style='text-align: center;padding: 20px;'> " +
                "<p>Thân gửi " + username + ", <p> <p>Cảm ơn bạn đã đăng ký tài khoản, hãy nhấn vào link ở dưới để hoàn thành quá trình đăng ký tài khoản! </p> <br />" +
                " <a href='" + linkOnline + "' style='background-color: red;padding: 10px;'> Click vào đây để xác nhận tài khoản! </a>" +
                " </td> </tr> <tr> <td style='background-color:#d6ffa6'> <h2>Liên hệ với cửa hàng</h2> <p>Cửa hàng mua thực phẩm online TP.HCM." +
                " Chuyên bán các loại rau sạch, củ quả, trái cây, thực phẩm tươi sống</p> <p>Địa chỉ : 23/25D đường số 1, phường Bình Thuận, Q.7, " +
                "TP.HCM</p> <p>Email : 18110320@student.hcmute.edu.vn</p> <p>Hot line : 0788283308</p> " +
                "<p>Debug (local-link) : <a href='" + link + "' style='background-color: red;padding: 10px;'> Link debug local! </a></p> </td> </tr> </table></body></html>";


            var client = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587))
            {
                Credentials = new NetworkCredential("timelive.circleqm@gmail.com", "5YemExFc!6QpT+aT"),
                EnableSsl = true,
                UseDefaultCredentials = false, // ?? :D ??
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            try
            {
                client.Send(mailMessage);
                client.Dispose();
                return "true";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string SendEmailResetPassword(string userEmail, string token, string username)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("timelive.circleqm@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Khôi phục mật khẩu cho tài khoản của bạn";
            mailMessage.IsBodyHtml = true;

            string encodetk = token.Replace("+", "%2B");
            string link = site + "confirmPassword?email=" + userEmail + "&token=" + encodetk;
            string linkOnline = siteOnline + "confirmPassword?email=" + userEmail + "&token=" + encodetk;
            mailMessage.Body = "<!DOCTYPE html><html><head> <title></title> <meta charset='utf-8' /> " +
                "<style> table, th, td { border: 1px solid black; } </style></head><body style='font-family: monospace;'>" +
                " <br /> <table width='100%'> <tr> <td style='background-color:#97b6e4;text-align: center;'> " +
                "<img src='https://res.cloudinary.com/dkmk9tdwx/image/upload/v1628192627/logo_v5ukvv.png' " +
                "style='width: 45px;height: 45px'> <h1 >Circle" + "'s" + " Shop</h1> </td> </tr> <tr> <td style='text-align: center;padding: 20px;'> " +
                "<p>Thân gửi " + username + ", <p> <p>Bạn đã gửi yêu cầu reset mật khẩu, hãy click vào link bên dưới để reset mật khẩu bạn!</p> <br />" +
                " <a href='" + linkOnline + "' style='background-color: red;padding: 10px;'> Click vào đây để xác nhận đổi mật khẩu! </a>" +
                " </td> </tr> <tr> <td style='background-color:#d6ffa6'> <h2>Liên hệ với cửa hàng</h2> <p>Cửa hàng mua thực phẩm online TP.HCM." +
                " Chuyên bán các loại rau sạch, củ quả, trái cây, thực phẩm tươi sống</p> <p>Địa chỉ : 23/25D đường số 1, phường Bình Thuận, Q.7, " +
                "TP.HCM</p> <p>Email : 18110320@student.hcmute.edu.vn</p> <p>Hot line : 0788283308</p> " +
                "<p>Debug (local-link) : <a href='" + link + "' style='background-color: red;padding: 10px;'> Link debug local! </a></p> </td> </tr> </table></body></html>";



            var client = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587))
            {
                Credentials = new NetworkCredential("timelive.circleqm@gmail.com", "5YemExFc!6QpT+aT"),
                EnableSsl = true,
                UseDefaultCredentials = false, // ?? :D ??
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            try
            {
                client.Send(mailMessage);
                client.Dispose();
                return "true";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
