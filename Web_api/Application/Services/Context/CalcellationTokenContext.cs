using Application.Interface.Context;

namespace Application.Services.Context
{
    public class CancellationTokenContext : ICancellationTokenContext
    {
        private CancellationToken? _currentCancellationToken;
        public CancellationToken CurrentCancellationToken => _currentCancellationToken ??= _currentCancellationToken.GetValueOrDefault();

        public void SetCancellationToken(CancellationToken cancellationToken)
        {
            if (_currentCancellationToken is not null)
                throw new Exception("Cannot override an existing cancellation token!");

            _currentCancellationToken = cancellationToken;
        }
    }
}
