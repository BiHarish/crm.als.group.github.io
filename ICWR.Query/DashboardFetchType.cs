using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.QueryInternal
{
    public enum DashboardFetchType
    {
        JobPendingForOpening = 1,
        ATDATANotUpdated = 2,
        BlNotReleased = 3,
        BlNotCreated = 4,
        BlHouseMasterNotReleased = 5,
        BlHouseMasterNotCreated = 6,
        CostNotBooked = 7,
        RevenueNotBooked = 8,
        Unbilled = 9,
        ShipmentPendingForClosure = 10,
        DlNotReleased = 11,
        DlNotCreated = 12,
        SIPending = 13,
        VGMPending = 14,
        InvoiceDispatchDetails = 15,
        IGMNo = 16,
        IGMFillingDT = 17
    }
}