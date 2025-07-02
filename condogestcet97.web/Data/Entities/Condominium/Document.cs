namespace condogestcet97.web.Data.Entities.Condominium
{
    public class Document : IEntity
    {
        public int Id { get; set; } 

        public string Name { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime EmissionDate { get; set; }

        public Intervention? Intervention { get; set; }

        public Meeting Assembly { get; set; }
    }
}
