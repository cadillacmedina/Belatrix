using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobLogger
{
    public class ContentLog
    {
        private int idContent = 0;
        private string typeContent = "";
        private string contText = "";
        private bool message = false;
        private bool error = false;
        private bool warning = false;

        public ContentLog()
        {
        }

        public ContentLog(bool _message, bool _error, bool _warning)
        {
            this.message = _message;
            this.error = _error;
            this.warning = _warning;
        }

        public int IdContent
        {
            get
            {
                if (this.message)
                    this.idContent = 1;
                if (this.error)
                    this.idContent = 2;
                if (this.warning)
                    this.idContent = 3;
                return this.idContent;
            }
            set
            {
                this.idContent = value;
            }
        }

        public string TypeContent
        {
            get
            {
                if (this.message)
                    this.typeContent = "Message";
                if (this.error)
                    this.typeContent = "Error";
                if (this.warning)
                    this.typeContent = "Warning";
                return this.typeContent;
            }
            set
            {
                this.typeContent = value;
            }
        }

        public string ContText
        {
            get
            {
                return this.contText;
            }
            set
            {
                this.contText = value;
            }
        }
    }
}