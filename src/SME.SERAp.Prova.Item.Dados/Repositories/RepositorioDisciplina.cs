using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;


namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioDisciplina : RepositorioBase<Disciplina>, IRepositorioDisciplina
    {
        public RepositorioDisciplina(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }
    }
}