namespace NZWalks.API.Models.DTO
{
    public class WalkDto
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double LenghtInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        //Getting the Region Data.
        public RegionDto Region { get; set; }
        //Getting difficulty data.
        public DifficultyDto Difficulty { get; set; }

    }
}
