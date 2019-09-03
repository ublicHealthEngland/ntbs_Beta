﻿using System;
using System.Collections.Generic;

namespace ntbs_service.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }

        public virtual Hospital Hospital { get; set; }
        public virtual PatientDetails PatientDetails { get; set; }
        public virtual ClinicalTimeline ClinicalTimeline { get; set; }
    }
}
