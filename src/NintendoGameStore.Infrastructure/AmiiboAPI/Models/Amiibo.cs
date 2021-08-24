using System.Collections.Generic;

namespace NintendoGameStore.Infrastructure.AmiiboAPI.Models
{
    public class AmiibosJson
    {
        public List<Amiibo> Amiibo { get; set; }
    }
    public class Amiibo
    {
        public string AmiiboSeries { get; set; }
        public string Character { get; set; }
        public string GameSeries { get; set; }
        public string Head { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public Release Release { get; set; }
        public string Tail { get; set; }
        public string Type { get; set; }
    }

    public class Release
    {
        public string au { get; set; }
        public string eu { get; set; }
        public string jp { get; set; }
        public string na { get; set; }
    }
}
