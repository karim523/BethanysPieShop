using BethanysPieShop.InventoryManagement.NotificationContext;
using System.Text.RegularExpressions;

namespace BethanysPieShop.InventoryManagement.Domain.SupplierManagement
{
    public class Supplier:Notifiable
    {
        public int Id { get;private set; }
        public string Name { get; private set; }
        public string Adress { get; private set; } 
        public string? Email { get; private set; } 
        public int Phone { get; private set; }

        public Supplier(string name, string adress, int phone)
        {
            SetName(name);
            SetAdress(adress);
            SetPhone(phone);
        }
        public bool UpdateSupplier(string name,string address,int phone,string email=null)
        {
            if (SetName(name) is true && SetAdress(address) is true && SetPhone(phone) is true && SetEmail(email) is true)
            {
                return true;
            }
            return false;
        }
      
        public bool SetPhone(int phone)
        {
            if (phone <= 0)
            {
                throw new ArgumentException("Phone is Invalid");
            }
            Phone = phone;
            return true;
        }
        public bool SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)||name.Length<3)
            {
                throw new ArgumentException("Name is Invalid");
            }
            Name = name;
            return true;
        }
        public bool SetAdress(string adress)
        {
            if (string.IsNullOrWhiteSpace(adress)||adress.Length<1)
            {
              throw new ArgumentException("Adress is Invalid");
            }
            Adress = adress;
            return true;

        }
        public bool SetEmail(string email)
        {
            //if (string.IsNullOrWhiteSpace(email))
            //{
            //    Email = null;
            //    return true;
            //}
            //if (!email.EndsWith("@gmail.com"))//TODO:Change Valiadtion
            //{
            //    throw new ArgumentException("Email is Invalid");
            //}
            //Email = email;
            //return true;

            if (string.IsNullOrWhiteSpace(email))
            {
                Email = null;
                return true;
            }
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                              @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
         
            Regex re = new Regex(strRegex);
            
            if (re.IsMatch(email))
                return true;
            else
                return false;

        }
    }
}
