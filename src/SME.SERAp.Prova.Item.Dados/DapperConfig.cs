using Dapper;
using Dommel;
using SME.SERAp.Prova.Item.Dados.TypeMappers;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Dados
{
    public static class DapperConfig
    {
        public static void RegistrarMapeamentos()
        {
            // ── Dapper puro (QueryAsync, ExecuteAsync, etc.) ─────────────────
            SqlMapper.SetTypeMap(typeof(AreaConhecimento), new ColumnAttributeTypeMapper<AreaConhecimento>());
            SqlMapper.SetTypeMap(typeof(Assunto), new ColumnAttributeTypeMapper<Assunto>());
            SqlMapper.SetTypeMap(typeof(Competencia), new ColumnAttributeTypeMapper<Competencia>());
            SqlMapper.SetTypeMap(typeof(Dificuldade), new ColumnAttributeTypeMapper<Dificuldade>());
            SqlMapper.SetTypeMap(typeof(Disciplina), new ColumnAttributeTypeMapper<Disciplina>());
            SqlMapper.SetTypeMap(typeof(Grupo), new ColumnAttributeTypeMapper<Grupo>());
            SqlMapper.SetTypeMap(typeof(Habilidade), new ColumnAttributeTypeMapper<Habilidade>());
            SqlMapper.SetTypeMap(typeof(Matriz), new ColumnAttributeTypeMapper<Matriz>());
            SqlMapper.SetTypeMap(typeof(QuantidadeAlternativa), new ColumnAttributeTypeMapper<QuantidadeAlternativa>());
            SqlMapper.SetTypeMap(typeof(Subassunto), new ColumnAttributeTypeMapper<Subassunto>());
            SqlMapper.SetTypeMap(typeof(TipoGrade), new ColumnAttributeTypeMapper<TipoGrade>());
            SqlMapper.SetTypeMap(typeof(Usuario), new ColumnAttributeTypeMapper<Usuario>());
            SqlMapper.SetTypeMap(typeof(UsuarioGrupo), new ColumnAttributeTypeMapper<UsuarioGrupo>());

            // ── Dommel (GetAsync, InsertAsync, UpdateAsync, DeleteAsync) ─────
            DommelMapper.SetColumnNameResolver(new ColumnAttributeColumnNameResolver());
            DommelMapper.SetTableNameResolver(new ColumnAttributeTableNameResolver());
            DommelMapper.SetKeyPropertyResolver(new ColumnAttributeKeyPropertyResolver());
        }
    }
}