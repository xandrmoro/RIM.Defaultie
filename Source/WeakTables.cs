using RimWorld;
using System.Runtime.CompilerServices;

namespace Defaultie
{
    public static class WeakTables
    {
        private static ConditionalWeakTable<Building_WorkTable, WorkTableDefaults> workTableDefaults = new ConditionalWeakTable<Building_WorkTable, WorkTableDefaults>();
        public static WorkTableDefaults GetDefaults(this Building_WorkTable table) => workTableDefaults.GetOrCreateValue(table);

        public static void Relink(this Building_WorkTable table, WorkTableDefaults defaults)
        {
            workTableDefaults.Remove(table);
            workTableDefaults.Add(table, defaults);
        }

        private static ConditionalWeakTable<Bill, BillAddedFields> billLinks = new ConditionalWeakTable<Bill, BillAddedFields>();
        public static BillAddedFields GetAddedFields(this Bill bill) => billLinks.GetOrCreateValue(bill);
    }
}
