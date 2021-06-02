using System;

namespace HSEApiTraining.Models.Events
{
    /// <summary>
    /// Событие, получающее информаци по ошибке
    /// </summary>
    public class ErrorTextEventArgs : EventArgs
    {
        public string Error { get; set; }

        public ErrorTextEventArgs(string error) => Error = error;
    }
}
