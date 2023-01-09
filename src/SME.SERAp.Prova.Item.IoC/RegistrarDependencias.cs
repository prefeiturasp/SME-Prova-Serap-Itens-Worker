using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SME.SERAp.Prova.Item.Aplicacao;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dados.Cache;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dados.Interfaces.CoreSSO;
using SME.SERAp.Prova.Item.Dados.Mappings;
using SME.SERAp.Prova.Item.Dados.Repositories;
using SME.SERAp.Prova.Item.Dados.Repositories.CoreSSO;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using SME.SERAp.Prova.Item.Infra.Services;
using SME.SERAp.Prova.Item.IoC.Extensions;

namespace SME.SERAp.Prova.Item.IoC
{
    public static class RegistraDependencias
    {
        public static void Registrar(IServiceCollection services)
        {
            services.AdicionarMediatr();
            services.AdicionarValidadoresFluentValidation();
            RegistrarServicos(services);
            RegistrarRepositorios(services);
            RegistrarRepositoriosCoreSSO(services);
            RegistrarCasosDeUso(services);
            RegistraMapeamentos();
        }

        private static void RegistrarServicos(IServiceCollection services)
        {
            services.TryAddSingleton<IServicoLog, ServicoLog>();
            services.TryAddSingleton<IServicoClientApi, ServicoClientApi>();
        }

        private static void RegistrarRepositorios(IServiceCollection services)
        {
            services.AddScoped<IRepositorioCache, RepositorioCache>();
            services.AddScoped<IRepositorioAssunto, RepositorioAssunto>();
            services.AddScoped<IRepositorioSubassunto, RepositorioSubassunto>();
            services.AddScoped<IRepositorioTipoItem, RepositorioTipoItem>();
            services.AddScoped<IRepositorioAreaConhecimento, RepositorioAreaConhecimento>();
            services.AddScoped<IRepositorioDisciplina, RepositorioDisciplina>();
            services.AddScoped<IRepositorioMatriz, RepositorioMatriz>();
            services.AddScoped<IRepositorioTipoGrade, RepositorioTipoGrade>();
            services.AddScoped<IRepositorioCompetencia, RepositorioCompetencia>();
            services.AddScoped<IRepositorioMatriz, RepositorioMatriz>();
            services.AddScoped<IRepositorioHabilidade, RepositorioHabilidade>();
            services.AddScoped<IRepositorioGrupo, RepositorioGrupo>();
            services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
            services.AddScoped<IRepositorioUsuarioGrupo, RepositorioUsuarioGrupo>();
        }

        private static void RegistrarRepositoriosCoreSSO(IServiceCollection services)
        {
            services.AddScoped<IRepositorioCoreSSOGrupo, RepositorioCoreSSOGrupo>();
            services.AddScoped<IRepositorioCoreSSOUsuario, RepositorioCoreSSOUsuario>();
        }

        private static void RegistrarCasosDeUso(IServiceCollection services)
        {
            services.AddScoped<IAssuntoSyncUseCase, AssuntoSyncUseCase>();
            services.AddScoped<IAssuntoTratarUseCase, AssuntoTratarUseCase>();
            services.AddScoped<ISubassuntoSyncUseCase, SubassuntoSyncUseCase>();
            services.AddScoped<ISubassuntoTratarUseCase, SubassuntoTratarUseCase>();
            services.AddScoped<ITipoItemSyncUseCase, TipoItemSyncUseCase>();
            services.AddScoped<ITipoItemTratarUseCase, TipoItemTratarUseCase>();
            services.AddScoped<IAreaConhecimentoSyncUsecase, AreaConhecimentoSyncUsecase>();
            services.AddScoped<IAreaConhecimentoUseCase, AreaConhecimentoTratarUseCase>();
            services.AddScoped<IDisciplinaSyncUseCase, DisciplinaSyncUseCase>();
            services.AddScoped<IDisciplinaTratarUseCase, DisciplinaTratarUseCase>();
            services.AddScoped<ISyncMatrizUseCase, MatrizSyncUseCase>();
            services.AddScoped<IMatrizTratarUseCase, MatrizTratarUseCase>();
            services.AddScoped<ITipoGradeSyncUseCase, TipoGradeSyncUseCase>();
            services.AddScoped<ITipoGradeTratarUseCase, TipoGradeTratarUseCase>();
            services.AddScoped<ICompetenciaSyncUseCase, CompetenciaSyncUseCase>();
            services.AddScoped<ICompetenciaTratarUseCase, CompetenciaTratarUseCase>();
            services.AddScoped<IHabilidadeSyncUseCase, HabilidadeSyncUseCase>();
            services.AddScoped<IHabilidadeTratarUseCase, HabilidadeTratarUseCase>();
            services.AddScoped<IIniciarImportacoesUseCase, IniciarImportacoesUseCase>();
            services.AddScoped<IGrupoSyncUseCase, GrupoSyncUseCase>();
            services.AddScoped<IGrupoTratarUseCase, GrupoTratarUseCase>();
            services.AddScoped<IUsuarioSyncUseCase, UsuarioSyncUseCase>();
            services.AddScoped<IUsuarioTratarUseCase, UsuarioTratarUseCase>();
            services.AddScoped<IUsuarioGrupoInserirUseCase, UsuarioGrupoInserirUseCase>();
            services.AddScoped<IUsuarioGrupoInativarUseCase, UsuarioGrupoInativarUseCase>();
        }

        private static void RegistraMapeamentos()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new AssuntoMap());
                config.AddMap(new SubassuntoMap());
                config.AddMap(new TipoItemMap());
                config.AddMap(new AreaConhecimentoMap());
                config.AddMap(new DisciplinaMap());
                config.AddMap(new MatrizMap());
                config.AddMap(new TipoGradeMap());
                config.AddMap(new CompetenciaMap());
                config.AddMap(new HabilidadeMap());
                config.AddMap(new GrupoMap());
                config.AddMap(new UsuarioMap());
                config.AddMap(new UsuarioGrupoMap());
                config.ForDommel();
            });
        }
    }
}
