using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Services
{
    public class DragAndDropService
    {
        public Object Data { get; set; }
        public Object OwnData { get; set; }
        public string Zone { get; set; }

        public void StartDrag(Object data, string zone)
        {
            this.Data = data;
            this.Zone = zone;
            this.OwnData = OwnData;
        }
        public bool Accepts(string zone)
        {
            return Zone == zone;
        }
    }
}
