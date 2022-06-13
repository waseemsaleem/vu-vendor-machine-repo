namespace VendorMachine.Core.ViewModels
{
    public class GenericResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Reponse { get; set; }
    }
}