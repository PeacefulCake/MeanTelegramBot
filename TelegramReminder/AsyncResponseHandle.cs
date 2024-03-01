using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramReminder
{
    internal class AsyncResultHandle<T>
    {
        private EventWaitHandle _waitHandle = new AutoResetEvent(false);
        private T? _result;
        private int _timeout;

        public AsyncResultHandle(int timeout = -1)
        {
            _timeout = timeout;
            _result = default;
        }

        public async Task<T?> GetReultAsync(CancellationToken cancellationToken)
        {

            var task = new Task(WaitForResult);
            task.Start();
            await task.WaitAsync(cancellationToken);

            return _result;
        }

        private void WaitForResult()
        {
            _waitHandle.WaitOne(_timeout);
        }

        public void ReturnReult(T result)
        {
            _result = result;
            _waitHandle.Set();
        }
    }
}
