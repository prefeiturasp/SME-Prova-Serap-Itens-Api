using System.ComponentModel;

namespace SME.SERAp.Prova.Item.Dominio.Enums
{
    public enum TipoArquivo
    {
        [Description("Texto_Base")]
        BaseText = 1,
        [Description("Alternativa")]
        Alternative = 2,
        [Description("Justificativa")]
        Justificative = 3,
        [Description("Enunciado")]
        Statement = 4,
        [Description("Prova")]
        Test = 5,
        [Description("Modelo_prova")]
        ModelTestHeader = 6,
        [Description("Arquivo")]
        File = 7,
        [Description("Folha_de_resposta")]
        AnswerSheetStudentNumber = 8,
        [Description("Lote")]
        AnswerSheetBatch = 9,
        [Description("Modelo_prova")]
        ModelTestFooter = 10,
        [Description("Modelo_prova")]
        ModelTestCover = 11,
        [Description("Lote_Folha_de_resposta")]
        AnswerSheetLot = 12,
        [Description("Folha_de_resposta")]
        AnswerSheetQrCode = 13,
        [Description("Analise_Item")]
        AnalysisItem = 14,
        [Description("Gabarito_Aluno")]
        AnswerSheetBatchLog = 15,
        [Description("Gabarito_Prova")]
        TestFeedback = 16,
        [Description("Resultado_Prova")]
        ExportCorrectionResult = 17,
        [Description("Dados_Folha_de_resposta")]
        ExportAnswerSheetResult = 18,
        [Description("Rel_Folha_Resposta")]
        ExportFollowUpIdentification = 19,
        [Description("Zip_Rel_Folha_Resposta")]
        ZipFollowUpIdentification = 20,
        [Description("Rel_Processsamento_Correcao")]
        ExportReportCorrection = 21,
        [Description("Rel_Desempenho_Prova")]
        ExportReportTestPerformance = 22,
        [Description("Rel_Desempenho_Item_Prova")]
        ExportReportItemPerformance = 23,
        [Description("Rel_Percentage_Item_Choice")]
        ExportReportPercentageItemChoice = 24,
        [Description("Rel_Student_Performance")]
        ExportReportStudentPerformance = 25,
        [Description("Icone_Link_Externo")]
        IconeLinkExterno = 26,
        [Description("Icone_Ferramenta_Destaque")]
        IconeFerramentaDestaque = 27,
        [Description("Icone_Ferramentas")]
        IconeFerramentas = 28,
        [Description("Video")]
        Video = 29,
        [Description("Thumbnail_Video")]
        ThumbnailVideo = 30,
        [Description("Audio")]
        Audio = 31,
        [Description("ResultadosPSP")]
        ResultadosPsp = 32     
    }
}