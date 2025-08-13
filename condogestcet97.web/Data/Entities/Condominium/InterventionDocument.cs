namespace condogestcet97.web.Data.Entities.Condominium
{
    public class InterventionDocument : Document
    {
        public override DocumentType Type { get; set; } = DocumentType.Intervention;

        public int InterventionId { get; set; }

        public Intervention? Intervention { get; set; }
    }
}
