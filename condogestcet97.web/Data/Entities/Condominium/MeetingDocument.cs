namespace condogestcet97.web.Data.Entities.Condominium
{
    public class MeetingDocument : Document
    {
        public override DocumentType Type { get; set; } = DocumentType.Meeting;

        public int MeetingId { get; set; }

        public Meeting? Meeting { get; set; }
    }
}
