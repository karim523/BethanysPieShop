using BethanysPieShop.InventoryManagement.NotificationContext;
using System.Text.RegularExpressions;

namespace BethanysPieShop.InventoryManagement.Domain.SupplierManagement
{
    public class Supplier
    {
        public int Id { get;private set; }
        public string Name { get; private set; }
        public string Adress { get; private set; } 
        public string? Email { get; private set; } 
        public int Phone { get; private set; }
        private Supplier()
        {
            
        }
        public static Result<Supplier> Create(string name, string adress, int phone )
        {
            var result =  Result<Supplier>.Create();
            if (phone <= 0)
            {
                result.AddError( "Phone is Invalid" );
            }
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
            {
                result.AddError("Name is Invalid");
            }
            if (string.IsNullOrWhiteSpace(adress) || adress.Length < 1)
            {
                result.AddError("Adress is Invalid");
            }
            if (result.IsSucces )
            {
                return Result<Supplier>.Scucsse(new Supplier
                {
                    Phone = phone,
                    Name = name,
                    Adress = adress
                });
            }

            return result;
        }
        public Result<bool> UpdateSupplier(string name,string address,int phone,string email=null)
        {
            var result = Result<bool>.Create();
            if (phone <= 0)
            {
                result.AddError("Phone is Invalid");
            }
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
            {
                result.AddError("Name is Invalid");
            }
            if (string.IsNullOrWhiteSpace(address) || address.Length < 1)
            {
                result.AddError("Adress is Invalid");
            }
            if (!result.IsSucces)
            {
                return result;
            }
            Phone = phone;
            Name = name;
            Adress = address;
            return Result<bool>.Scucsse(true);
        }
        public bool SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                Email = null;
                return true;
            }
            string strRegex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
         
            Regex re = new Regex(strRegex);

            if (re.IsMatch(email))
            {
                Email = email;
                return true;
            }
            else
                Result<bool>.Failure("Email is Invalid");
            return false;

        }
    }
}
