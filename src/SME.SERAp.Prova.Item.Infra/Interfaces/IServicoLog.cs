using SME.SERAp.Prova.Item.Infra.Services;
using System;

namespace SME.SERAp.Prova.Item.Infra.Interfaces
{
    public interface IServicoLog
    {
        void Registrar(Exception ex);
        void Registrar(ServicoLog.LogNivel nivel, string erro, string observacoes, string stackTrace);
        void Registrar(string mensagem, Exception ex);
        void Registrar(ServicoLog.LogMensagem log);
    }
}
