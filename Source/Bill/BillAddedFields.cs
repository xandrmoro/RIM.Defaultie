using Verse;

namespace Defaultie
{
    public class BillAddedFields : IExposable
    {
        public bool Linked = false;

        public virtual void ExposeData()
        {
            Scribe_Values.Look(ref Linked, "linkedToTable");
        }
    }
}
