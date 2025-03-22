namespace Avaliativa.Models
{
    public class Evento
    {
        public int EventoId { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public string Local { get; set; }

        public List<Participante> Participante {  get; set; }
    }
}
