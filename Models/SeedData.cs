using Microsoft.EntityFrameworkCore;

namespace Avaliativa.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            // Obtém o serviço do contexto
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<Context>();

            // Aplica as migrations automaticamente
            context.Database.Migrate();

            // Se o banco estiver vazio, insere dados iniciais
            if (!context.Eventos.Any())
            {
                context.Eventos.AddRange(
                    new Evento { Nome = "Jornada de Informática", Data = new DateTime(2024, 6, 10), Local = "Auditório Prof Edson Antônio Velano" },
                    new Evento { Nome = "Workshop de C#", Data = new DateTime(2024, 7, 15), Local = "Labratório" }
                );

                context.Participantes.AddRange(
                    new Participante { Nome = "João Silva", Email = "joao@email.com", EventoId = 1 },
                    new Participante { Nome = "Maria Souza", Email = "maria@email.com", EventoId = 1 },
                    new Participante { Nome = "Carlos Oliveira", Email = "carlos@email.com", EventoId = 2 }
                );

                context.SaveChanges();
            }
        }
    }
}
