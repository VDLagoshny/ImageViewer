using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WFA_PictureShower
{
    /// <summary>
    /// The class for working with few WinForm and passing thread between ones
    /// </summary>
    class CSyncContext
    {
        #region Parameters
        /// <summary>
        /// The initialization class SynchronizationContext's instance
        /// </summary>
        private SynchronizationContext _context;

        /// <summary>
        /// The property of _context parameter
        /// </summary>
        public SynchronizationContext Context
        {
            set { }
            get { return _context; }
        }
        #endregion

        /// <summary>
        /// The class's constructor
        /// </summary>
        public CSyncContext(){}

        /// <summary>
        /// The method is launched for passing class SynchronizationContext's instance
        /// </summary>
        /// <param name="threadStart">Standard input parameters, type ParameterizedThreadStart</param>
        /// <param name="sender">Standard input parameters, type object</param>
        public void SyncContextPassing(ParameterizedThreadStart parameterizedThreadStart, object sender)
        {
            Thread _thread = new Thread(parameterizedThreadStart);
            _thread.Start(sender);
        }

        /// <summary>
        /// The method is launched for returning class SynchronizationContext's instance
        /// </summary>
        /// <param name="sendOrPostCallBack">Standard input parameters, type SendOrPostCallback</param>
        /// <param name="sender">Standard input parameters, type object</param>
        /// <param name="context">Standard input parameters, type SynchronizationContext</param>
        public void SyncContextReturning(SendOrPostCallback sendOrPostCallBack, object sender, SynchronizationContext context)
        {
            if (context != null)
                context.Post(sendOrPostCallBack, sender);
        }
    }
}