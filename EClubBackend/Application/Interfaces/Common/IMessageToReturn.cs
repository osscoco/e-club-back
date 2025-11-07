namespace E_Club.Application.Interfaces.Common
{
    public interface IMessageToReturn
    {
        string MessageSuccess(string model, string operation);
        string MessageError(string model, string operation);
    }
}