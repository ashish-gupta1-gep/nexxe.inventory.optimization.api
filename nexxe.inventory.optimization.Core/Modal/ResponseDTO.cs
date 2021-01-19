using dm.lib.exceptionmanager.nuget;
namespace nexxe.inventory.optimization.Core.Modal
{
    public class ResponseDTO<T> : GepReturn
    {
        public T ReturnValue { get; set; }
    }
}
