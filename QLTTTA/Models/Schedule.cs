using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTTTA.Models
{
    public class Schedule
    {
        public string RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Schedule(string roomId, DateTime start, DateTime end)
        {
            if (start >= end) throw new ArgumentException("Thời gian bắt đầu phải trước thời gian kết thúc.");
            RoomId = roomId; StartTime = start; EndTime = end;
        }

        public bool IsConflict(Schedule other)
        {
            if (other == null || this.RoomId != other.RoomId) return false;
            return (this.StartTime < other.EndTime) && (other.StartTime < this.EndTime);
        }
    }

}
