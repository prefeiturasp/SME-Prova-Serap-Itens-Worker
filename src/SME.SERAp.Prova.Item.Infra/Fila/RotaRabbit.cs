﻿namespace SME.SERAp.Prova.Item.Infra.Fila
{
    public class RotaRabbit
    {
        public static string Log => "ApplicationLog";

        public const string DeadLetterSync = "serap.estudante.item.deadletter.sync";
        public const string DeadLetterTratar = "serap.estudante.item.deadletter.tratar";

        public const string AlterarTesteTratar = "serap.estudante.item.teste.alterar";
        public const string AssuntoSync = "serap.estudante.item.assunto.sync";
        public const string AssuntoTratar = "serap.estudante.item.assunto.tratar";
        public const string SubassuntoSync = "serap.estudante.item.subassunto.sync";
        public const string SubassuntoTratar = "serap.estudante.item.subassunto.tratar";

        public const string TipoItemSync = "serap.estudante.item.tipoitem.sync";
        public const string TipoItemTratar = "serap.estudante.item.tipoitem.tratar";
        public const string TipoGradeSync = "serap.estudante.item.tipograde.sync";
        public const string TipoGradeTratar = "serap.estudante.item.tipograde.tratar";
        public const string CompetenciaSync = "serap.estudante.item.competencia.sync";
        public const string CompetenciaTratar = "serap.estudante.item.competencia.tratar";
    }
}
