namespace BethanysPieShop.InventoryManagement.NotificationContext
{
    public class Result<T>
    {
        private readonly List<string> _errors;
        public bool IsSucces { get;private set; } = true;
        public T Object {  get; private set; }
        public IReadOnlyList<string> Errors => _errors.AsReadOnly();
        private Result()
        {
            _errors = new List<string>();
        }
        public static Result<T> Create()
        {
           return new Result<T>();
        }
        public static Result<T> Scucsse(T obj)
        {
            return new Result<T>()
            {
                Object= obj
            };
        }
        public static Result<T> Failure(string error)
        {
            var result= new Result<T>()
            {   
                IsSucces=false,                
            };
            result._errors.Add(error);
            
            return result;
        }
        public static Result<T> Failure(List<string> errors)
        {
            var result= new Result<T>()
            {
                IsSucces = false,
            };
            result._errors.AddRange(errors);
           
            return result;
        }
        public bool AddError(string error)
        {
            if (error==null)
            {
                return false;
            }
            IsSucces = false;
           
            _errors.Add(error);

            return true;
        }
        //public override string ToString()
        //{
        //  foreach (var error in Errors) 
        //    {
        //        return error;
        //    }
        //}
    }
}