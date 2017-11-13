using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace ComReader
{
    public partial class SerialPortExtend : SerialPort
    {
        public new void Open()
        {
            try
            {
                base.Open();
                GC.SuppressFinalize(BaseStream);
            }
            catch
            {
            }
        }

        public new void Dispose()
        {
            Dispose(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (base.Container != null))
            {
                base.Container.Dispose();
            }

            try
            {
                GC.ReRegisterForFinalize(BaseStream);
            }
            catch
            {
            }
            base.Dispose(disposing);
        }

    }
}
