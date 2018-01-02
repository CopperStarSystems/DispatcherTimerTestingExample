using System.Windows.Threading;

namespace ConsoleAppTest
{
    // The code in this class came from Sheva's TechSpace.
    // URL: http://shevaspace.spaces.live.com/blog/cns!FD9A0F1F8DD06954!411.entry
    static class DispatcherHelper
    {
        private static DispatcherOperationCallback exitFrameCallback = new
           DispatcherOperationCallback(ExitFrame);

        /// <summary>
        /// Processes all UI messages currently in the message queue.
        /// </summary>
        public static void DoEvents()
        {
            // Create new nested message pump.
            DispatcherFrame nestedFrame = new DispatcherFrame();

            // Dispatch a callback to the current message queue, when getting called, 
            // this callback will end the nested message loop.
            // note that the priority of this callback should be lower than the that of UI event messages.
            DispatcherOperation exitOperation = Dispatcher.CurrentDispatcher.BeginInvoke(
                DispatcherPriority.Background, exitFrameCallback, nestedFrame);

            // pump the nested message loop, the nested message loop will immediately 
            // process the messages left inside the message queue.
            Dispatcher.PushFrame(nestedFrame);

            // If the "exitFrame" callback doesn't get finished, Abort it.
            if (exitOperation.Status != DispatcherOperationStatus.Completed)
            {
                exitOperation.Abort();
            }
        }

        private static object ExitFrame(object state)
        {
            DispatcherFrame frame = state as DispatcherFrame;

            // Exit the nested message loop.
            frame.Continue = false;
            return null;
        }
    }
}