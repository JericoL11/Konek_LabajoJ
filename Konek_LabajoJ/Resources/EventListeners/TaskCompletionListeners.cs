using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konek_LabajoJ.Resources.EventListeners
{
    public class TaskCompletionListeners : Java.Lang.Object, IOnSuccessListener, IOnFailureListener
    {

        //Fields
        public event EventHandler<TaskSuccessEventArgs> Success;

        public event EventHandler<TaskFailureEventArgs> Failure;

        //clsses 
        public class TaskFailureEventArgs : EventArgs
        {
            public string Cause { get; set; }
        }

        public class TaskSuccessEventArgs : EventArgs
        {
            public Java.Lang.Object Result { get; set; }
        }


        //methods
        public void OnFailure(Java.Lang.Exception e)
        {
            Failure?.Invoke(this, new TaskFailureEventArgs { Cause = e.Message });
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            Success?.Invoke(this, new TaskSuccessEventArgs { Result = result });
        }
    }
}