using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Web.Mvc;
using UtilizandoControleReportViwer.Models;

namespace ReportViwerRelatorioEmPDF.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Relatorio()
        {
            //instanciando e populando lista de produtos
            var listaProduto = new List<ProdutoModels>()
            {
             new ProdutoModels{Codigo = 1, Nome = "Teclado", Preco = 31.50, QtdEmEstoque = 100},
             new ProdutoModels{Codigo = 2, Nome = "Mouse", Preco = 20, QtdEmEstoque = 75},
             new ProdutoModels{Codigo = 3, Nome = "Monitor", Preco = 350.99, QtdEmEstoque = 83},
             new ProdutoModels{Codigo = 4, Nome = "Fone de ouvido", Preco = 59.60, QtdEmEstoque = 18},
             new ProdutoModels{Codigo = 5, Nome = "Mousepad", Preco = 8.30, QtdEmEstoque = 33},
             new ProdutoModels{Codigo = 6, Nome = "Notebook", Preco = 2500.70, QtdEmEstoque = 20},
             new ProdutoModels{Codigo = 7, Nome = "HD SSD 120 GB", Preco = 240, QtdEmEstoque = 17},
             new ProdutoModels{Codigo = 8, Nome = "Placa de vídeo", Preco = 999, QtdEmEstoque = 42},
             new ProdutoModels{Codigo = 9, Nome = "Gabinete", Preco = 60, QtdEmEstoque = 23},
             new ProdutoModels{Codigo = 10, Nome = "Iphone 6", Preco = 3000, QtdEmEstoque = 50}
            };


            LocalReport relat = new LocalReport();   
            //caminho do arquivo rdlc
            relat.ReportPath = Server.MapPath("~/Relatorio/ListaProduto.rdlc");

            //vinculando dataset ao objeto relat
           var ds = new ReportDataSource();
           ds.Name = "dsProduto";
           ds.Value = listaProduto;
           relat.DataSources.Add(ds);


            //definindo tipo que o relatório será renderizado
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            //configurações da página ex: margin, top, left ...
            string deviceInfo =
            "<DeviceInfo>" +
            "<OutputFormat>PDF</OutputFormat>" +
            "<PageWidth>8.5in</PageWidth>" +
            "<PageHeight>11in</PageHeight>" +
            "<MarginTop>0.5in</MarginTop>" +
            "<MarginLeft>1in</MarginLeft>" +
            "<MarginRight>1in</MarginRight>" +
            "<MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";
  
            Warning[] warnings;
            string[] streams;
            byte[] bytes;
 
            //Renderizando o relatório o bytes
            bytes = relat.Render(reportType, deviceInfo, out mimeType,  out encoding,  out fileNameExtension, out streams, out warnings);
 
            //Retornondo o arquivo renderizado
            //dessa forma o arquivo será aberto na mesma aba do navegador em que foi chamado
            return File(bytes, mimeType);

            //para fazer o download, informe um terceiro parametro ao método file. 
            //O terceiro parametro será definido como nome do arquivo
            //ex:
            //return File(bytes, mimeType, "ListadeProduto");
     }


        
    }
}