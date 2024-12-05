using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportifly.Model
{
    public class FeedbackModel
    {
        public int FeedbackId { get; set; }
        public int ClientId { get; set; }
        public string FeedbackComments { get; set; }
        public byte Ball { get; set; }
    }
}
