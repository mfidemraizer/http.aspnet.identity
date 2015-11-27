namespace Http.AspNet.Identity
{
    using System.Net.Http.Headers;
    using System.Threading;

    internal static class Authentication
    {
        internal static ReaderWriterLockSlim Lock { get; } = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private static AuthenticationHeaderValue _authenticationHeader;

        public static AuthenticationHeaderValue AuthenticationHeader
        {
            get { return _authenticationHeader; }
            set
            {
                Lock.EnterWriteLock();

                try
                {
                    _authenticationHeader = value;
                }
                finally
                {
                    if (Lock.IsWriteLockHeld)
                        Lock.ExitWriteLock();
                }
            }
        }
    }
}