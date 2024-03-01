using System.Net.Mail;

namespace Logic.TechnicalAssement.App.Validators.Model
{
  public class EmailValidator
  {
        public static bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch(ArgumentException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }
  }
}
