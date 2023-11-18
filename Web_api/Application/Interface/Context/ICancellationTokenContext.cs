
namespace Application.Interface.Context
{
    public interface ICancellationTokenContext
    {
        CancellationToken CurrentCancellationToken { get; }
        void SetCancellationToken(CancellationToken cancellationToken);
    }
}
