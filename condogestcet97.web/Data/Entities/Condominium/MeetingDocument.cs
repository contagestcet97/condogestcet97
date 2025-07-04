namespace condogestcet97.web.Data.Entities.Condominium
{
    public class MeetingDocument : Document
    {
        public override DocumentType Type { get; set; } = DocumentType.Meeting;

        public Meeting Meeting { get; set; }
    }
}
